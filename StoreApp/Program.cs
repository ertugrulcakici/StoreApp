using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;
using StoreApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // Viewlerin olduğu bir uygulama oluşturuyoruz.
builder.Services.AddRazorPages(); // Razor Pages'leri ekliyoruz.


builder.Services.AddDbContext<RepositoryContext>(
    options => options.UseSqlite(
        builder.Configuration.GetConnectionString("sqlite-connection"),
        b => b.MigrationsAssembly("StoreApp")
        )
); // Sqlite veritabanı bağlantısını yapıyoruz. Bunu DI Container'a ekliyoruz.

builder.Services.AddDistributedMemoryCache(); // Session'ları kullanabilmek için bu middleware'yi ekliyoruz.
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".StoreApp.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(10);
}); // Session'ları kullanabilmek için bu middleware'yi ekliyoruz.
//builder.Services.AddHttpContextAccessor(); // HttpContext'e erişebilmek için bu middleware'yi ekliyoruz.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // HttpContext'e erişebilmek için bu middleware'yi ekliyoruz. // yukarıdakini kullanmadık çünkü sadece bir tane üretilmesini istiyoruz.

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();

builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IOrderService, OrderManager>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//builder.Services.AddSingleton<Cart>(); // Cart'ı DI Container'a ekliyoruz. // Singleton yaptığımız zaman farklı tarayıcılarda bile olsa, aynı cart nesnesi kullanılıyor. Bu direk server-side'da çalışan app üzerinde tanımlanıyor
builder.Services.AddScoped<Cart>(c =>SessionCart.GetCart(c)); // Cart'ı DI Container'a ekliyoruz.

builder.Services.AddAutoMapper(typeof(Program)); // AutoMapper'ı ekliyoruz.

var app = builder.Build();

app.UseSession(); // Session'ları kullanabilmek için bu middleware'yi ekliyoruz.

app.UseStaticFiles(); // wwwroot klasöründeki dosyaları kullanabilmek için bu middleware'yi ekliyoruz.
app.UseHttpsRedirection(); // Https yönlendirmelerini yapabilmek için bu middleware'yi ekliyoruz.
app.UseRouting(); // Routing işlemlerini yapabilmek için bu middleware'yi ekliyoruz.
app.UseEndpoints(endpoints =>
{
    // bunlar sıra sıra çalıştığı için önce admin sonra default çalışacak.
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();

});
app.Run();
