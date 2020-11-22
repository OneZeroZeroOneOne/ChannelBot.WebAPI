using AutoMapper;
using ChannelBot.Authorization.Bll;
using ChannelBot.BLL.Abstractions;
using ChannelBot.BLL.Options;
using ChannelBot.BLL.Services;
using ChannelBot.DAL;
using ChannelBot.DAL.Contexts;
using ChannelBot.DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ChannelBot.Utilities.Middlewares;
using System;

namespace ChannelBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("ALO");
            services.AddControllers();

            string datebaseconnectionstring = Environment.GetEnvironmentVariable("datebaseconnectionstring");
            Console.WriteLine(datebaseconnectionstring);
            services.AddTransient(x =>
            {
                return new MainContext(datebaseconnectionstring);
            });

            MainContext context = new MainContext(datebaseconnectionstring);

            JwtOption jwtOption = context.JwtOption.FirstOrDefault();

            AuthOptions authOptions = new AuthOptions(jwtOption.Key, jwtOption.Issuer, jwtOption.Audience);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = authOptions.ISSUER,

                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = authOptions.AUDIENCE,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,

                            // установка ключа безопасности
                            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                        };
                    });


            services.AddTransient<ICategoryService, CategoryService>();

            services.AddTransient<IGroupService, GroupService>();

            services.AddTransient<ISourceService, SourceService>();

            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("AdminRole", policy =>
                    policy.Requirements.Add(new RoleEntryRequirement(1)));
            });

            services.AddSingleton<IAuthorizationHandler, RoleEntryHandler>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.InjectJavascript("https://ajax.googleapis.com/ajax/libs/jquery/1.7/jquery.min.js");
                c.InjectJavascript("https://unpkg.com/browse/webextension-polyfill@0.6.0/dist/browser-polyfill.min.js", type: "text/html");
                c.InjectJavascript("https://raw.githack.com/OneZeroZeroOneOne/StaticFiles/master/Login.js");
            });


            app.UseRouting();
            app.UseMiddleware<JwtMiddleware>();
            app.UseAuthorization();

            app.UseCors(x => x.AllowAnyOrigin());

            app.UseCors(x => x.AllowAnyHeader());


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
