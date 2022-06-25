using BLL.Interfaces;
using BLL.DTO;
using BLL.MapperConfigurations;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using DLL.Entities;
using DLL;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private EFUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public CategoryService(string connectionString)
        {
            this._unitOfWork = new EFUnitOfWork(connectionString);

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<ConfigurationProfile>();
            });
            this._mapper = new Mapper(config);
        }
        public async Task CreateCategory(CategoryDTO categoryDto)
        {
            _unitOfWork.Categories.Create(_mapper.Map<Category>(categoryDto));
            await _unitOfWork.SaveAsync();
        }

        public async Task ChangeCategory(CategoryDTO categoryDto)
        {
            _unitOfWork.Categories.Update(_mapper.Map<Category>(categoryDto));
            await _unitOfWork.SaveAsync();
        }


        public async Task DeleteCategory(CategoryDTO category)
        {
            _unitOfWork.Categories.Delete(_mapper.Map<Category>(category));
            await _unitOfWork.SaveAsync();
        }

        public async void Dispose()
        {
            await _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryDTO>>( await _unitOfWork.Categories.GetAllAsync());
        }

        public async Task<CategoryDTO> GetCategory(int id)
        {
            return _mapper.Map<CategoryDTO>(await _unitOfWork.Categories.GetByIdAsync(id));
        }

        public async Task<CategoryDTO> GetCategoryByName(string name)
        {
            return _mapper.Map<CategoryDTO>(await _unitOfWork.Categories.FindAsync(category => category.Name == name));
        }
    }
}
