using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        void CreateCategory(CategoryDTO categoryDto);
        void ChangeCategory(CategoryDTO categoryDto);
        void DeleteCategory(int id);
        CategoryDTO GetCategory(int id);
        CategoryDTO GetCategoryByName(string name);
        IEnumerable<CategoryDTO> GetCategories();
        void Dispose();
    }
}
