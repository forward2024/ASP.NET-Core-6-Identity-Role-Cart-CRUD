 using Forward.Data;
using Forward.Repository;
using Forward.Repository.RUnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Forward.Data.DbInitalizer;
using Forward.Models.model;
using Microsoft.AspNetCore.Identity.UI.Services;
using Forward.Models;


#region var
var builder = WebApplication.CreateBuilder(args);

var connect = builder.Configuration.GetConnectionString("Connect");


#endregion

#region ConnectDb
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connect);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

#endregion

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IDbInitalizer, DbInitalizer>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.MapRazorPages();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
dataSedding();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");


app.Run();


void dataSedding()
{
    using(var scope = app.Services.CreateScope())
    {
        var DbInitalizer = scope.ServiceProvider.GetRequiredService<IDbInitalizer>();
        DbInitalizer.Initalizer();
    }
}