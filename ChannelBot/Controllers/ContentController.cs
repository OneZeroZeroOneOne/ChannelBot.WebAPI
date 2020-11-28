using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChannelBot.DAL.ViewModel.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChannelBot.BLL.Abstractions;

namespace ChannelBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IContentService _contentService;
        private readonly IMapper _mapperProfile;
        public ContentController(IContentService contentService, IMapper mapperProfile)
        {
            _contentService = contentService;
            _mapperProfile = mapperProfile;
        }

        [Authorize(Policy = "AdminRole")]
        [HttpGet]
        public async Task<List<ContentResponseViewModel>> GetAllContent()
        {
            var responce = await _contentService.GetAllContent();
            return _mapperProfile.Map<List<ContentResponseViewModel>>(responce);
        }

        [Authorize(Policy = "AdminRole")]
        [HttpGet]
        [Route("{contentId}")]
        public async Task<ContentResponseViewModel> GetContent(int contentId)
        {
            var responce = await _contentService.GetContent(contentId);
            return _mapperProfile.Map<ContentResponseViewModel>(responce);
        }

        [Authorize(Policy = "AdminRole")]
        [HttpPost]
        public async Task CreateContent([FromQuery] long id, [FromQuery] string mediaUrl, [FromQuery] string description, [FromQuery] int sourceId)
        {
            await _contentService.CreateContent(id, Uri.UnescapeDataString(mediaUrl), Uri.UnescapeDataString(description), sourceId);

        }
    }
}