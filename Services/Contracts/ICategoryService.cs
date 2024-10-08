using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Services.Contracts
{
    public interface ICategoryService
    {
        Category GetCategory(int id, bool trackChanges);
        IEnumerable<Category> GetAllCategories(bool trackChanges);
    }
}