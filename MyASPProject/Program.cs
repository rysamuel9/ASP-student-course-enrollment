using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyASPProject.Data;
using MyASPProject.Models;
using MyASPProject.Services;
using MyASPProject.Services.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//menambahkan pengaturan Identity
builder.Services.AddIdentity<CustomIdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 10;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
}).AddDefaultUI().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<RestaurantDbContext>();

builder.Services.ConfigureApplicationCookie(opt => opt.LoginPath = "/Account/Login");

builder.Services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddScoped<IPengguna, PenggunaServices>();
builder.Services.AddScoped<IRestaurantData, SqlRestaurantData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();