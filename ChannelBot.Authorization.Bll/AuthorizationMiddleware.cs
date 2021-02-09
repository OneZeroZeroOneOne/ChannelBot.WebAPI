using ChannelBot.DAL.Contexts;
using ChannelBot.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ChannelBot.Authorization.Bll
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MainContext _db;

        public JwtMiddleware(RequestDelegate next, MainContext db)
        {
            _db = db;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = "123 .";
            token += context.Request.Headers["da"].FirstOrDefault()?.Split(" ").Last();
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync(token);
            /*if (token != null)
            {
                ParseToken(context, token);
                await _next(context);
            }
            else
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"unauthorized");
            }*/
        }

        private void ParseToken(HttpContext context, string token)
        {
            try
            {
                JwtOption jwtOption = _db.JwtOption.FirstOrDefault();

                if (jwtOption == null) throw new Exception("JwtOption was null");

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(jwtOption.Key);

                SecurityToken SignatureValidator(string encodedToken, TokenValidationParameters parameters)
                {
                    var jwt = new JwtSecurityToken(encodedToken);

                    var hmac = new HMACSHA256(Encoding.ASCII.GetBytes(jwtOption.Key));

                    var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(hmac.Key), SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);

                    var signKey = signingCredentials.Key as SymmetricSecurityKey;

                    var encodedData = jwt.EncodedHeader + "." + jwt.EncodedPayload;
                    var compiledSignature = Encode(encodedData, signKey.Key);

                    if (compiledSignature != jwt.RawSignature)
                    {
                        throw new Exception("Token signature validation failed.");
                    }

                    return jwt;
                }

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireSignedTokens = false, //погугли
                    ClockSkew = TimeSpan.Zero,
                    SignatureValidator = SignatureValidator,
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                context.User = new GenericPrincipal(new AuthorizedUserModel
                {
                    RoleId = int.Parse(jwtToken.Claims.First(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value),
                    UserId = int.Parse(jwtToken.Claims.First(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value),
                }, new []{""});
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string Encode(string input, byte[] key)
        {
            HMACSHA256 sha = new HMACSHA256(key);
            byte[] byteArray = Encoding.UTF8.GetBytes(input);
            MemoryStream stream = new MemoryStream(byteArray);
            byte[] hashValue = sha.ComputeHash(stream);
            return Base64UrlEncoder.Encode(hashValue);
        }
    }
}