using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModelShop.Data;
using ModelShop.Data.Contracts;
using ModelShop.Data.Implementation;
using ModelShop.Helpers;
using ModelShop.Models;
using ModelShop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add database context
builder.Services.AddDbContext<ModelShopContext>((options) =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ModelShopContext")));

// Add repositories
builder.Services.AddScoped<IModel3DRepository, Model3DRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IModelCategoryRepository, ModelCategoryRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICart_Model3DRepository, Cart_Model3DRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrder_Model3DRepository, Order_Model3DRepository>();

// Other services
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Add PhotoService
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<IPhotoService, PhotoService>();

// Add FileService
builder.Services.AddScoped<IFileService, FileService>();

// Identity
builder.Services.AddIdentity<Client,  IdentityRole>().
    AddEntityFrameworkStores<ModelShopContext>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ModelShopContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);

    await DbInitializer.SeedClientsAndRolesAsync(app);
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
