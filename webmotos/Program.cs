using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using webmotos.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.




builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "notificacion";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Requiere HTTPS
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.LoginPath = "/Login/Login";
        options.LogoutPath = "/Login/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.SlidingExpiration = true;
        /* options.ExpireTimeSpan = TimeSpan.FromMinutes(5); */
    });

builder.Services.AddDbContext<WebmotosContext>(opt =>
    opt.UseMySql(builder.Configuration.GetConnectionString("webmotosDB"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("webmotosDB"))));
    
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");


app.Run();
