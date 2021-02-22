using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChannelBot.BLL.Abstractions;
using ChannelBot.DAL.Models;
using Microsoft.AspNetCore.Routing;
using ChannelBot.DAL.ViewModel.Response;
using Microsoft.AspNetCore.Authorization;
using ChannelBot.Authorization.Bll;

namespace ChannelBot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {

        private readonly IGroupService _groupService;
        private readonly IMapper _mapperProfile;
        public GroupController(IGroupService groupService, IMapper mapperProfile)
        {
            _groupService = groupService;
            _mapperProfile = mapperProfile;
        }

        [Authorize(Policy = "AdminRole")]
        [HttpGet]
        public async Task<List<GroupResponseViewModel>> GetAllGroup()
        {
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            var responce = await _groupService.GetAllGroups(authorizedUserModel.UserId);
            return _mapperProfile.Map<List<GroupResponseViewModel>>(responce);
        }

        [Authorize(Policy = "AdminRole")]
        [HttpGet]
        [Route("{id}")]
        public async Task<GroupResponseViewModel> GetGroup([FromRoute] int id)
        {
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            var responce = await _groupService.GetGroup(id, authorizedUserModel.UserId);
            return _mapperProfile.Map<GroupResponseViewModel>(responce);
        }

        [Authorize(Policy = "AdminRole")]
        [HttpPost]
        public async Task CreateGroup([FromQuery] int categoryId, [FromQuery] int serialNumber)
        {
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            await _groupService.CreateGroup(categoryId, serialNumber, authorizedUserModel.UserId);

        }

        [Authorize(Policy = "AdminRole")]
        [HttpPost]
        [Route("AddSource")]
        public async Task AddSource([FromQuery] int groupId, [FromQuery] int sourceId)
        {
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            await _groupService.AddSource(groupId, sourceId, authorizedUserModel.UserId);
        }

        [Authorize(Policy = "AdminRole")]
        [HttpGet]
        [Route("GroupSource")]
        public async Task<List<SourceResponseViewModel>> GroupSource([FromQuery] int groupId)
        {
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            var responce = await _groupService.GroupSource(groupId, authorizedUserModel.UserId);
            return _mapperProfile.Map<List<SourceResponseViewModel>>(responce);
        }

        [Authorize(Policy = "AdminRole")]
        [HttpDelete]
        [Route("{groupId}")]
        public async Task DeleteGroup([FromRoute] int groupId)
        {
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            await _groupService.DeleteGroup(groupId, authorizedUserModel.UserId);
        }
    }

}