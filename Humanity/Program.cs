//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Humanity.Data;
//using Microsoft.AspNetCore.CookiePolicy;

//var builder = WebApplication.CreateBuilder(args);
//var configuration = builder.Configuration;

//builder.Services.AddAuthentication().AddGoogle(googleOptions =>
//{
//    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
//    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
//});

//var connectionString = builder.Configuration.GetConnectionString("HumanityContextConnection") ?? throw new InvalidOperationException("Connection string 'HumanityContextConnection' not found.");
//builder.Services.AddDbContext<HumanityContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<HumanityContext>();
//builder.Services.AddControllersWithViews();

//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    options.MinimumSameSitePolicy = SameSiteMode.None; // or SameSiteMode.Unspecified
//    options.HttpOnly = HttpOnlyPolicy.None;
//    options.Secure = CookieSecurePolicy.None; // Only set to CookieSecurePolicy.Always if your site is served over HTTPS
//});


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.UseAuthentication();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=UserControlller}/{action=Home}/{id?}");
//app.MapRazorPages();

//app.Run();

using Humanity.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/UserControlller/adminlogin";
		options.LogoutPath = "/UserControlller/Logout";
		// Configure other cookie options as needed
	})
	.AddGoogle(googleOptions =>
	{
		googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
		googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
	});

var connectionString = builder.Configuration.GetConnectionString("HumanityContextConnection") ?? throw new InvalidOperationException("Connection string 'HumanityContextConnection' not found.");
builder.Services.AddDbContext<HumanityContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<HumanityContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Before UseAuthorization
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=UserControlller}/{action=Home}/{id?}");
app.MapRazorPages();

app.Run();
