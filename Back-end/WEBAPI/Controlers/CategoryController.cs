using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEBAPI.Models;

namespace WEBAPI.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private IMapper _mapper;
        private ICategoryService _categoryService;
        public CategoryController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            this._categoryService = categoryService;
        }

        [HttpGet]
        public IEnumerable<CategoryInfoModel> Get()
        {
            return _mapper.Map<IEnumerable<CategoryInfoModel>>(_categoryService.GetCategories());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var category =(await _categoryService.GetCategoryAsync(id));
            if (category is null)
            {
                return new NotFoundResult();
            }
            return new JsonResult(category.Name);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Post(CategoryCreationModel category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (_categoryService.GetCategoryByName(category.Name) is null)
            {
                _categoryService.CreateCategory(_mapper.Map<CategoryDTO>(category));
                return Ok();
            }
            else
            {
                return new ConflictResult();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> PutAsync(CategoryInfoModel category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (_categoryService.GetCategoryByName(category.Name) is null || _categoryService.GetCategoryByName(category.Name).Id == category.Id)
            {
                await _categoryService.ChangeCategory(_mapper.Map<CategoryDTO>(category));
                return Ok();
            }
            else
            {
                return new ForbidResult();
            }
        }

        // DELETE api/<CategoryController>/5
        [Authorize(Roles = "admin")]
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var category = await _categoryService.GetCategoryAsync(id);
            if (category is null)
            {
                return new NotFoundResult();
            }
            await _categoryService.DeleteCategoryAsync(id);
            return Ok();
        }
    }
}
