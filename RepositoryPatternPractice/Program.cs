using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RepositoryPatternPractice.Data;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Optimization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddIdentityCore<ClaimsIdentity>(new Claim());
//builder.Services.AddDefaultIdentity<ClaimsIdentity>(new ClaimsIdentity());
//builder.Services.AddTransient<ClaimsIdentity>(new Claim());

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login";
        options.Cookie.Name = "RepositoryProject";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";

    });


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*");
                          policy.AllowAnyHeader();
                          
                      });
    
});

//builder.Services.AddControllersWithViews(options => {
//    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
//})
//    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
//    .AddJsonOptions(options => {
//        options.JsonSerializerOptions. = Formatting.Indented;
//    });
//builder.Services.AddIdentityCore();
//builder.Services.AddMvc().AddJsonOptions(
//        options => options.JsonSerializerOptions..ReferenceLoopHandling =
//        Newtonsoft.Json.ReferenceLoopHandling.Ignore
//    );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.UseCookiePolicy();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
 
app.MapRazorPages();
app.UseCors(MyAllowSpecificOrigins);
app.Run();
