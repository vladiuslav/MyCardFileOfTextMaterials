using BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategory(CategoryDTO categoryDto);
        Task ChangeCategory(CategoryDTO categoryDto);
        Task DeleteCategoryAsync(int id);
        Task<CategoryDTO> GetCategoryAsync(int id);
        CategoryDTO GetCategoryByName(string name);
        IEnumerable<CategoryDTO> GetCategories();
        Task Dispose();
    }
}
