using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repositories.Contracts;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOrderRepository _orderRepsitory;
        // ---
        public IProductRepository ProductRepository => _productRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public IOrderRepository OrderRepository => _orderRepsitory;


        public RepositoryManager(IProductRepository productRepository, RepositoryContext context, ICategoryRepository categoryRepository, IOrderRepository orderRepsitory)
        {
            _context = context;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _orderRepsitory = orderRepsitory;
        }


        public void Save()
        {
            _context.SaveChanges();
        }
    }
}