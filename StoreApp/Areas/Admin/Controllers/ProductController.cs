using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public ProductController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var model = _serviceManager.ProductService.GetAllProducts(false);
            return View(model);
        }

        public IActionResult Create()
        {
            // ViewBag.Categories = _manager.CategoryService.GetAllCategories(false);
            ViewBag.Categories = GetCategoriesSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion ProductDto, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                // file operation
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                ProductDto.ImageUrl = String.Concat("/images/", file.FileName);
                _serviceManager.ProductService.CreateProduct(ProductDto);
                return RedirectToAction("Index");
            }
            return View(ProductDto);
        }

        public SelectList GetCategoriesSelectList()
        {
            return new SelectList(
                _serviceManager.CategoryService.GetAllCategories(false),
                "CategoryId",
                "CategoryName", "1"
            );
        }

        public IActionResult Update([FromRoute(Name = "id")] int id)
        {
            ViewBag.Categories = GetCategoriesSelectList();

            var model = _serviceManager.ProductService.GetOneProductForUpdate(id, false);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] ProductDtoForUpdate productDto, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                productDto.ImageUrl = String.Concat("/images/", file.FileName);

                _serviceManager.ProductService.UpdateOneProduct(productDto);
                return RedirectToAction("Index");
            }

            return View(productDto);
        }

        [HttpGet]
        public IActionResult Delete([FromRoute(Name = "id")] int id)
        {
            _serviceManager.ProductService.DeleteOneProduct(id);
            return RedirectToAction("Index");
        }

    }
}