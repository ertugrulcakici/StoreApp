using Entities.Models;
using StoreApp.Infrastructure.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Models
{
    public class SessionCart : Cart // Bu sınıfı, session u direk model üzerinden yönetmek için oluşturduk.
    {
        [JsonIgnore] // bu property'nin json'a dönüştürülmesini engelliyoruz. Sessiona SessionCart eklediğimiz için her nesne eklenirken tekrar tekrar eklenmesini engelliyoruz.
        public ISession Session { get; set; }
        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }
        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson<SessionCart>("Cart", this);
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session.SetJson<SessionCart>("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
