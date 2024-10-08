using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")] // Arealarla çalıştığımız için bu attribute'ü ekliyoruz.
    public class OrderController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public OrderController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public IActionResult Index()
        {
            var orders = _serviceManager.OrderService.Orders;
            return View(orders);
        }

        [HttpPost]
        public IActionResult Complete([FromForm] int id)
        {
            _serviceManager.OrderService.Complete(id);
            return RedirectToAction("Index");
        }
    }
}
