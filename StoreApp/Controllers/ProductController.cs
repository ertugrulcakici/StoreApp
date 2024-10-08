using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {

        private readonly IServiceManager _manager;
        // private readonly RepositoryContext _context;

        // public ProductController(RepositoryContext context)
        // {
        //     _context = context;
        // }

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            // var _context = new RepositoryContext(new DbContextOptionsBuilder<RepositoryContext>().UseSqlite("Data Source=D:\\learning\\MVC\\ProductDB.db").Options);
            // return _context.Products.ToList();
            // Eğer DI olmazsa üstteki gibi bir kullanım yapılabilir.
            // var model = _context.Products.ToList();
            List<Product> model = _manager.ProductService.GetAllProducts(false).ToList();
            return View(model);
        }


        public IActionResult Get(int id)
        {
            // var model = _context.Products.Find(id);
            Product model = _manager.ProductService.GetOneProduct(id, false);
            return View(model);
        }
    }
}