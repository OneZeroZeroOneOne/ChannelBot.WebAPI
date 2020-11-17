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

namespace ChannelBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {

        private IGroupService _groupService;
        private IMapper _mapperProfile;
        public GroupController(IGroupService groupService, IMapper mapperProfile)
        {
            _groupService = groupService;
            _mapperProfile = mapperProfile;
        }

        [Authorize]
        [HttpGet]
        async public Task<List<GroupResponseViewModel>> GetAllGroup()
        {
            var responce = await _groupService.GetAllGroups();
            return _mapperProfile.Map<List<GroupResponseViewModel>>(responce);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        async public Task<GroupResponseViewModel> GetGroup([FromRoute] int id)
        {
            var responce = await _groupService.GetGroup(id);
            return _mapperProfile.Map<GroupResponseViewModel>(responce);
        }

        [Authorize]
        [HttpPost]
        async public Task CreateGroup([FromQuery] int categoryId)
        {
            await _groupService.CreateGroup(categoryId);

        }

        [Authorize]
        [HttpPost]
        [Route("AddSource")]
        async public Task AddSource([FromQuery] int groupId, [FromQuery] int sourceId)
        {
            await _groupService.AddSource(groupId, sourceId);
        }

        [Authorize]
        [HttpGet]
        [Route("GroupSource")]
        async public Task<List<SourceResponseViewModel>> GroupSource([FromQuery] int groupId)
        {
            var responce = await _groupService.GroupSource(groupId);
            return _mapperProfile.Map<List<SourceResponseViewModel>>(responce);
        }

        [Authorize]
        [HttpDelete]
        [Route("{groupId}")]
        async public Task DeleteGroup([FromRoute] int groupId)
        {
            await _groupService.DeleteGroup(groupId);
        }
    }

}