using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChannelBot.DAL.Models;
using ChannelBot.BLL.Abstractions;
using AutoMapper;
using ChannelBot.DAL.ViewModel.Response;
using Microsoft.AspNetCore.Authorization;

namespace ChannelBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        private IMapper _mapperProfile;
        public CategoryController(ICategoryService categoryService, IMapper mapperProfile)
        {
            _categoryService = categoryService;
            _mapperProfile = mapperProfile;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<CategoryResponseViewModel>> GetCategories()
        {
            var responce = await _categoryService.GetAllCategories();
            return _mapperProfile.Map<List<CategoryResponseViewModel>>(responce);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<CategoryResponseViewModel> GetCategory([FromRoute]int id)
        {
            var responce = await _categoryService.GetCategory(id);
            return _mapperProfile.Map<CategoryResponseViewModel>(responce);
        }

        [Authorize]
        [HttpPost]
        public async Task CreateCategory([FromQuery] string title)
        {
            await _categoryService.CreateCategory(title);
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteCategory([FromRoute] int id)
        {
            await _categoryService.DeleteCategory(id);
        }
    }
}