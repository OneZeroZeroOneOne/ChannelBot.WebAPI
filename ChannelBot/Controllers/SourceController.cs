using AutoMapper;
using ChannelBot.Authorization.Bll;
using ChannelBot.BLL.Abstractions;
using ChannelBot.DAL.Models;
using ChannelBot.DAL.ViewModel.In;
using ChannelBot.DAL.ViewModel.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChannelBot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SourceController : ControllerBase
    {
        private readonly ISourceService _sourceService;
        private readonly IMapper _mapperProfile;
        public SourceController(ISourceService sourceService, IMapper mapperProfile)
        {
            _sourceService = sourceService;
            _mapperProfile = mapperProfile;
        }


        [Authorize(Policy = "AdminRole")]
        [HttpGet]
        [Route("{id}")]
        public async Task<SourceResponseViewModel> GetSource([FromRoute] int id)
        {
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            var responce = await _sourceService.GetSource(id, authorizedUserModel.UserId);
            return _mapperProfile.Map<SourceResponseViewModel>(responce);
        }

        [Authorize(Policy = "AdminRole")]
        [HttpGet]
        public async Task<List<SourceResponseViewModel>> GetAllSource()
        {
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            var responce = await _sourceService.GetAllSource(authorizedUserModel.UserId);
            return _mapperProfile.Map<List<SourceResponseViewModel>>(responce);
        }


        [Authorize(Policy = "AdminRole")]
        [HttpPost]
        public async Task<int> CreateSource([FromQuery] string mediaUrl, int platformId)
        {
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            return await _sourceService.CreateSource(Uri.UnescapeDataString(mediaUrl), platformId, authorizedUserModel.UserId);
        }


        [Authorize(Policy = "AdminRole")]
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteSource([FromRoute] int id)
        {
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            await _sourceService.DeleteSource(id, authorizedUserModel.UserId);
        }
    }
}