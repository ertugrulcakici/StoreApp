using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;
using StoreApp.Infrastructure.Extensions;

namespace StoreApp.Pages
{
    public class CartModel : PageModel
    {
        private readonly IServiceManager _manager;

        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";
        public CartModel(IServiceManager manager,Cart cartService)
        {
            _manager = manager;
            Cart = cartService;
        }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(int productId, string returnUrl)
        {
            Product? product = _manager.ProductService.GetOneProduct(productId, false);
            if (product is not null)
            {
                Cart.AddItem(product, 1);
                // Cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
                // Cart.AddItem(product, 1);
                // HttpContext.Session.Set("Cart", Cart);
            }
            //return Page();
             return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(int productId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(l => l.Product.ProductId.Equals(productId)).Product);
            return Page();
            // return RedirectToPage(new { returnUrl });
        }
    }
}