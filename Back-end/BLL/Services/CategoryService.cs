using BLL.Interfaces;
using BLL.DTO;
using BLL.MapperConfigurations;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using DLL.Entities;
using DLL;

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
        public void CreateCategory(CategoryDTO categoryDto)
        {
            _unitOfWork.Categories.Create(_mapper.Map<Category>(categoryDto));
            _unitOfWork.Save();
        }

        public void ChangeCategory(CategoryDTO categoryDto)
        {
            _unitOfWork.Categories.Update(_mapper.Map<Category>(categoryDto));
            _unitOfWork.Save();
        }


        public void DeleteCategory(int id)
        {
            _unitOfWork.Categories.Delete(id);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Save();
            _unitOfWork.Dispose();
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryDTO>>(_unitOfWork.Categories.GetAll());
        }

        public CategoryDTO GetCategory(int id)
        {
            return _mapper.Map<CategoryDTO>(_unitOfWork.Categories.Get(id));
        }

        public CategoryDTO GetCategoryByName(string name)
        {
            return _mapper.Map<CategoryDTO>(_unitOfWork.Categories.GetAll().FirstOrDefault(category => category.Name == name));
        }
    }
}
