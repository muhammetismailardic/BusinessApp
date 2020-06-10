using BusinessApp.CarpetWash.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.Business.Abstract
{
    public interface ICategoryService
    {
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int categoryId);
        Task<ICollection<Category>> GetAllCategoriesAsync();
        Task<Category> FindCategoryByIdAsync(int? categoryId);
        Task<bool> Exist(int id);
    }
}
