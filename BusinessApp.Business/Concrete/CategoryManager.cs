using BusinessApp.CarpetWash.Business.Abstract;
using BusinessApp.CarpetWash.DataAccess.Abstract;
using BusinessApp.CarpetWash.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public async Task CreateAsync(Category category)
        {
            try
            {
                await _categoryDal.CreateAsync(category);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task DeleteAsync(int categoryId)
        {
            try
            {
                await _categoryDal.DeleteAsync(new Category { Id = categoryId });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> Exist(int id)
        {
            var result =  await _categoryDal.GetWithInludesAsync(x => x.Id == id);

            if(result != null) 
            {
                return true;
            }

            return false;
        }

        public async Task<Category> FindCategoryByIdAsync(int? categoryId)
        {
            try
            {
                if (categoryId != null)
                {
                    return await _categoryDal.GetWithInludesAsync(id => id.Id == categoryId);
                }

                else { return null; }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ICollection<Category>> GetAllCategoriesAsync()
        {
            try
            {
                return await _categoryDal.GetListWithIncludesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task UpdateAsync(Category category)
        {
            try
            {
                await _categoryDal.UpdateAsync(category);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
