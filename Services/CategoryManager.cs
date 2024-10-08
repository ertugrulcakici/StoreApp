using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _manager;

        public CategoryManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public IEnumerable<Category> GetAllCategories(bool trackChanges)
        {
            return _manager.CategoryRepository.FindAll(trackChanges);
        }

        public Category GetCategory(int id, bool trackChanges)
        {
            Category? category = _manager.CategoryRepository.FindByCondition(c => c.CategoryId == id, trackChanges);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            return category;
        }
    }
}