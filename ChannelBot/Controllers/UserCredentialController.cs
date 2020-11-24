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

namespace ChannelBot.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        public async Task<List<UserCredentialResponseViewModel>> GetAllUserCredential()
        {
            var responce = await _contentService.GetAllUserCredentials();
            return _mapperProfile.Map<List<UserCredentialResponseViewModel>>(responce);
        }
    }
}