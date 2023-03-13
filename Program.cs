using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:SportsStoreConnection"]));

builder.Services.AddDbContext<AppIdentityDbContext>(opts =>
opts.UseSqlServer(builder.Configuration["ConnectionStrings:IdentityConnection"]));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();    

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
builder.Services.AddScoped<IOrderRepository, EfOrderRepository>();

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddServerSideBlazor(); //create the services that blazor uses

var app = builder.Build();

if(app.Environment.IsProduction())
{
    app.UseExceptionHandler("/error");
}
app.UseRequestLocalization(opts =>
{
    opts.AddSupportedCultures("en-US")
    .AddSupportedUICultures("en-US")
    .SetDefaultCulture("en-US");    
});

app.UseStaticFiles();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("catpage","{category}/Page{productPage:int}", new { Controller = "Home", action = "Index" });
app.MapControllerRoute("page", "Page{productPage:int}", new { Controller = "Home", action = "Index", productPage = 1 });
app.MapControllerRoute("category", "{category}", new { Controller = "Home", action = "Index", productPage = 1 });
app.MapControllerRoute("pagination", "Products/Page{productPage}", new { Controller = "Home", action = "Index", productPage = 1});
app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);
IdentitySeedData.EnsurePopulated(app);

app.MapRazorPages();
app.MapBlazorHub();//registers the blazor middleware components
app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");//ensure that blazor routing sistem work with the application

app.Run();
