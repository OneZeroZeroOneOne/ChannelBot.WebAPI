using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChannelBot.BLL.Abstractions;
using ChannelBot.DAL.Models;
using ChannelBot.DAL.ViewModel.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChannelBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourceController : ControllerBase
    {
        private ISourceService _sourceService;
        private IMapper _mapperProfile;
        public SourceController(ISourceService sourceService, IMapper mapperProfile)
        {
            _sourceService = sourceService;
            _mapperProfile = mapperProfile;
        }


        [Authorize]
        [HttpGet]
        [Route("{id}")]
        async public Task<SourceResponseViewModel> GetSource([FromRoute] int id)
        {
            var responce = await _sourceService.GetSource(id);
            return _mapperProfile.Map<SourceResponseViewModel>(responce);
        }


        [Authorize]
        [HttpGet]
        async public Task<List<SourceResponseViewModel>> GetAllSource()
        {
            var responce = await _sourceService.GetAllSource();
            return _mapperProfile.Map<List<SourceResponseViewModel>>(responce);
        }


        [Authorize]
        [HttpPost]
        async public Task CreateSource([FromQuery] string Url, [FromQuery] int platformId)
        {
            await _sourceService.CreateSource(Uri.UnescapeDataString(Url), platformId);
        }


        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        async public Task DeleteSource([FromRoute] int id)
        {
            await _sourceService.DeleteSource(id);
        }
    }
}