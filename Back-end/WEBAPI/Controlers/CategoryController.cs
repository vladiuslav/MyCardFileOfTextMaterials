using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using WEBAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        // GET: api/<CategoryController>
        [HttpGet]
        public IEnumerable<CategoryInfoModel> Get()
        {
            return _mapper.Map<IEnumerable<CategoryInfoModel>>(_categoryService.GetCategories());
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public CategoryInfoModel Get(int id)
        {
            return _mapper.Map<CategoryInfoModel>(_categoryService.GetCategory(id));
        }

        // POST api/<CategoryController>
        [Authorize(Roles = "admin")]
        [HttpPost]
        public void Post(CategoryCreationModel category)
        {
            _categoryService.CreateCategory(_mapper.Map<CategoryDTO>(category));
        }

        // PUT api/<CategoryController>
        [Authorize(Roles = "admin")]
        [HttpPut]
        public void Put(CategoryCreationModel category)
        {
            _categoryService.ChangeCategory(_mapper.Map<CategoryDTO>(category));
        }

        // DELETE api/<CategoryController>/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _categoryService.DeleteCategory(id);
        }
    }
}
