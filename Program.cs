using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DI
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

//Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("@32302e342e30Rb5k8XV5ifM0qdygGxmLPNwqMU8jilQP8 iGXu5w1kk=;Mgo DSMBaFt/QHRqVVhkVFpGaV5CQmFJfFBmQmldeVRxcUUmHVdTRHRcQl9jSn9ackNiXH5dcnI=;Mgo DSMBMAY9C3t2VVhkQlFacldJX3xIfkx0RWFab1l6cFNMYVlBJAtUQF1hSn5RdkZiUHtYcXFURWZa;Mgo DSMBPh8sVXJ0S0J XE9AflRAQmJJYVF2R2BJeFR0cl9HZUwxOX1dQl9gSXxTdURqXXlfdXVRRmY=;@32302e342e30f9WPn3GHxwlfZn/TZy9pgD9en iPWc5LmkKwDLIF91g=;NRAiBiAaIQQuGjN/V0Z WE9EaFtKVmFWf1NpR2NbfE53flFAal1UVBYiSV9jS31TdEVgWHZadnVQR2RVVw==;NT8mJyc2IWhhY31nfWN9Z2toYXxiY3xhY2Fgc2RpZ2VpYmZzAx5oNDY9Jzs2JzY OmQTND4yOj99MDw ;ORg4AjUWIQA/Gnt2VVhkQlFacldJX3xIfkx0RWFab1l6cFNMYVlBJAtUQF1hSn5RdkZiUHtYcXFUQ2Ja;@32302e342e30BL5NiQ6BLPc5F4K/XsjkigT QoD9az2yRpZG7dIrqe8=;@32302e342e30SQjl11qT2qHVx1SoJGG6T57Yan8/k6Mzdl1 uHBt1kA=;@32302e342e30XBcVPksGxEAflLWfm3YvRqSblYCSvCTlRV3N0wk1cts=;@32302e342e30BLkF9px/jvvll/TJy4bzCBoonstjkwgY2y/Dlqq9lAU=;@32302e342e30Rb5k8XV5ifM0qdygGxmLPNwqMU8jilQP8 iGXu5w1kk=");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
