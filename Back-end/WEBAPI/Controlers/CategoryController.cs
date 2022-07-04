using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public string Get(int id)
        {
            var category = _categoryService.GetCategory(id).Name;
            return category;
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
                return BadRequest();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public IActionResult Put(CategoryInfoModel category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (_categoryService.GetCategoryByName(category.Name) is null || _categoryService.GetCategoryByName(category.Name).Id == category.Id)
            {
                _categoryService.ChangeCategory(_mapper.Map<CategoryDTO>(category));
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<CategoryController>/5
        [Authorize(Roles = "admin")]
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _categoryService.DeleteCategory(id);
            return Ok();
        }
    }
}
