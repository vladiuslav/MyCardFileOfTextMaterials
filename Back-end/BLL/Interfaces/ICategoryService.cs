using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategory(CategoryDTO categoryDto);
        Task ChangeCategory(CategoryDTO categoryDto);
        Task DeleteCategory(CategoryDTO category);
        Task<CategoryDTO> GetCategory(int id);
        Task<CategoryDTO> GetCategoryByName(string name);
        Task<IEnumerable<CategoryDTO>> GetCategories();
        void Dispose();
    }
}
