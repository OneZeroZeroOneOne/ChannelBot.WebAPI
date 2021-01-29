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
using ChannelBot.DAL.Models;
using ChannelBot.DAL.ViewModel.In;

namespace ChannelBot.Controllers
{
    [Route("[controller]")]
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
        public async Task CreateContent([FromBody] CreateContentInModel content)
        {
            await _contentService.CreateContent(content.id, Uri.UnescapeDataString(content.mediaUrl), Uri.UnescapeDataString(content.description), content.sourceId);

        }
    }
}