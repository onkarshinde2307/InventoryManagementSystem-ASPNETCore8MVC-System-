using InventoryManagementSystem.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using OfficeOpenXml;

// Set EPPlus license globally (must be first)
//ExcelPackage.LicenseContext = LicenseContext.NonCommercial;    (Here is Error but i will resolve it)


var builder = WebApplication.CreateBuilder(args);

// Enable MVC
builder.Services.AddControllersWithViews();

// Enable DbConnection class (DI)
builder.Services.AddScoped<DbConnection>();

// Required for login session features
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// Enable Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

var app = builder.Build();

// Error handling 
//when error occurs in production environment then redirect to error page 
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Enable Session
app.UseSession();

// Default Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
