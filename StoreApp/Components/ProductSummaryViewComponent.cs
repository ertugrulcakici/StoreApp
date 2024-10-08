using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services.Contracts;

namespace StoreApp.Components
{
    public class ProductSummaryViewComponent : ViewComponent
    {
        private readonly IServiceManager _serviceManager;

        public ProductSummaryViewComponent(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public String Invoke()
        {
            var productsCount = _serviceManager.ProductService.GetAllProducts(false).Count();
            return productsCount.ToString();
        }

        // public IViewComponentResult Invoke()
        // {
        //     var productsCount = _manager.ProductService.GetAllProducts(false).Count();
        //     return View("Default", productsCount);
        // }
    }
}