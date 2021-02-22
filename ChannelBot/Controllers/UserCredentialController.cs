using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChannelBot.BLL.Abstractions;
using ChannelBot.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChannelBot.DAL.ViewModel.Response;
using Microsoft.AspNetCore.Authorization;
using ChannelBot.Authorization.Bll;

namespace ChannelBot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserCredentialController : ControllerBase
    {
        private readonly IUserCredentialService _contentService;
        private readonly IMapper _mapperProfile;
        public UserCredentialController(IUserCredentialService contentService, IMapper mapperProfile)
        {
            _contentService = contentService;
            _mapperProfile = mapperProfile;
        }

        [Authorize(Policy = "AdminRole")]
        [HttpGet]
        public async Task<List<UserCredentialResponseViewModel>> GetAllUserCredential()
        {
            var responce = await _contentService.GetAllUserCredentials();
            return _mapperProfile.Map<List<UserCredentialResponseViewModel>>(responce);
        }

        [Authorize(Policy = "AdminRole")]
        [HttpPost]
        public async Task<int> ChangeUserCredential([FromQuery] int categoryId, string login, string password)
        {
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            var responce = await _contentService.ChangeUserCredential(authorizedUserModel.UserId, categoryId, login, password);
            return 1;
        }

    }
}