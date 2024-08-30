using _2024AMS.Models;
using _2024AMS.Pages.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Okta.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Get access to the application's configuration in the appsettings.json file.
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddDbContext<_2024AMSContext>(options => options.UseSqlServer(configuration["ConnectionStrings:2024AMSConnectionString"]));

builder.Services.AddTransient<IEmailService, EmailServiceMailKit>();

builder.Services.AddSession(options =>
{
    // Set the length of the session timeout.
    options.IdleTimeout = TimeSpan.FromMinutes(120);
});

// Okta: Set up authentication.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OktaDefaults.MvcAuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.AccessDeniedPath = PathString.FromUriComponent("/Common/DenyAccess");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
    options.LoginPath = new PathString("/");
    options.LogoutPath = new PathString("/");
})
.AddOktaMvc(new OktaMvcOptions
{
    ClientId = configuration["Okta:ClientId"],
    ClientSecret = configuration["Okta:ClientSecret"],
    OktaDomain = configuration["Okta:OktaDomain"]
});

builder.Services.AddAuthorization(options =>
{
    // A = Administrator, F = FacStaff, T = Technician
    options.AddPolicy("DashboardPolicy", policy => policy.RequireRole("A", "F", "T"));
    options.AddPolicy("DeleteAssetPolicy", policy => policy.RequireRole("A"));
    options.AddPolicy("DeleteAssetPolicy2", policy => policy.RequireRole("A"));
    options.AddPolicy("DeleteAssetPolicy3", policy => policy.RequireRole("A"));
    options.AddPolicy("AssetsPolicy", policy => policy.RequireRole("A", "T"));
    options.AddPolicy("AssetCategoriesPolicy", policy => policy.RequireRole("A"));
    options.AddPolicy("MaintainUserAssetsPolicy", policy => policy.RequireRole("A"));
    options.AddPolicy("AddUserAssetPolicy", policy => policy.RequireRole("A"));
    options.AddPolicy("ModifyUserAssetPolicy", policy => policy.RequireRole("A"));
    options.AddPolicy("DeleteUserAssetPolicy", policy => policy.RequireRole("A"));
    options.AddPolicy("ManufacturersPolicy", policy => policy.RequireRole("A"));
    options.AddPolicy("OperatingSystemsPolicy", policy => policy.RequireRole("A"));
    options.AddPolicy("ReportsPolicy", policy => policy.RequireRole("A"));
    options.AddPolicy("UserAssetsPolicy", policy => policy.RequireRole("A", "F"));
    options.AddPolicy("UsersPolicy", policy => policy.RequireRole("A"));

});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AllowAnonymousToFolder("/Home");
    options.Conventions.AuthorizeFolder("/Common");
    options.Conventions.AuthorizeFolder("/Dashboard", "DashboardPolicy");
    options.Conventions.AuthorizeFolder("/Assets", "AssetsPolicy");
    options.Conventions.AuthorizePage("/Assets/DeleteAsset", "DeleteAssetPolicy");
    options.Conventions.AuthorizePage("/Assets/DeleteAsset?intAssetID", "DeleteAssetPolicy2");
    options.Conventions.AuthorizePage("/Assets/DeleteAsset?", "DeleteAssetPolicy3");
    options.Conventions.AuthorizeFolder("/AssetCategories", "AssetCategoriesPolicy");
    options.Conventions.AuthorizePage("/UserAssets/MaintainUserAssets", "MaintainUserAssetsPolicy");
    options.Conventions.AuthorizePage("/UserAssets/AddUserAsset", "AddUserAssetPolicy");
    options.Conventions.AuthorizePage("/UserAssets/ModifyUserAsset?", "ModifyUserAssetPolicy");
    options.Conventions.AuthorizePage("/UserAssets/DeleteUserAsset?", "DeleteUserAssetPolicy");
    options.Conventions.AuthorizeFolder("/Manufacturers", "ManufacturersPolicy");
    options.Conventions.AuthorizeFolder("/OperatingSystems", "OperatingSystemsPolicy");
    options.Conventions.AuthorizeFolder("/Reports", "ReportsPolicy");
    options.Conventions.AuthorizeFolder("/UserAssets", "UserAssetsPolicy");
    options.Conventions.AuthorizeFolder("/Users", "UsersPolicy");

});

// Okta: This is required for Okta.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Make the specified subdirectory the path base of the application so that
// the application can run under the apps.franklincollege.edu subdomain.
// Do not start any redirects in the PageModel classes with forward slashes.
app.UsePathBase("/2024AMS");
app.Use((context, next) =>
{
    context.Request.PathBase = "/2024AMS";
    return next();
});

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication(); // Okta: Enable Okta authentication.

app.UseAuthorization(); // Okta: Enable Okta authorization.

// Okta: This is required for Okta.
#pragma warning disable ASP0014
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Default}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();