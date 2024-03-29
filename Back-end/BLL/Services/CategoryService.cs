﻿using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.MapperConfigurations;
using DLL;
using DLL.Entities;
using DLL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// Implementaion of intarface category service for working with categories.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        /// <summary>
        /// Constructor for creating mapper and for unit of work. 
        /// </summary>
        /// <param name="connectionString">String for connecting to data base</param>
        public CategoryService(string connectionString)
        {
            this._unitOfWork = new EFUnitOfWork(connectionString);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ConfigurationProfile>();
            });
            this._mapper = new Mapper(config);
        }
        /// <summary>
        /// Constructor with parameter of unit of work, used for testing.
        /// </summary>
        public CategoryService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ConfigurationProfile>();
            });
            this._mapper = new Mapper(config);
        }
        public async Task CreateCategory(CategoryDTO categoryDto)
        {
            await _unitOfWork.Categories.Create(_mapper.Map<Category>(categoryDto));
            await _unitOfWork.SaveAsync();
        }

        public async Task ChangeCategory(CategoryDTO categoryDto)
        {
            _unitOfWork.Categories.Update(_mapper.Map<Category>(categoryDto));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _unitOfWork.Categories.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task Dispose()
        {
            await _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryDTO>>(_unitOfWork.Categories.GetAll());
        }

        public async Task<CategoryDTO> GetCategoryAsync(int id)
        {
            return _mapper.Map<CategoryDTO>(await _unitOfWork.Categories.GetAsync(id));
        }

        public CategoryDTO GetCategoryByName(string name)
        {
            return _mapper.Map<CategoryDTO>(_unitOfWork.Categories.GetAll().FirstOrDefault(category => category.Name == name));
        }
    }
}
