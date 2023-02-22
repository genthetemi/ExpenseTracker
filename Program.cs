using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DI
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

//Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHRqVVhkVFpHaVZdX2NLfUN1T2dadVt0ZDU7a15RRnVfQFxgSHZSd0VjWX5ccQ==;Mgo+DSMBPh8sVXJ0S0J+XE9AflRBQmpWfFN0RnNadVpzflBCcDwsT3RfQF5jS39Qd01iWH9ecHNQQA==;ORg4AjUWIQA/Gnt2VVhkQlFacldJXnxAYVF2R2BJfFR0d19DZEwxOX1dQl9gSXxSdURrWH5ecHVVR2Y=;MTEyMjc4NkAzMjMwMmUzNDJlMzBoQlRHR0M3amNnQzRRMlBSUkptVGJuYUlGa3d5L0M3STIzR0dUV1JncEs0PQ==;MTEyMjc4N0AzMjMwMmUzNDJlMzBMSzAyNTRkZ3YzSEUzYmd6V0FlYVBFTWE1VC9zSWJ1eEFjaW8yTHFWSWJnPQ==;NRAiBiAaIQQuGjN/V0Z+WE9EaFtKVmBWd0x0RWFab116cFZMZVhBJAtUQF1hSn5Rd0ZiUX5fcHRXRGhd;MTEyMjc4OUAzMjMwMmUzNDJlMzBPeTVJTk53a3R2MXJnK1NwWEFReVNnSVJDMzh1RDc1eitVdjhyTDNqS05NPQ==;MTEyMjc5MEAzMjMwMmUzNDJlMzBlSHJYeVYzUytDbktGSDVwMlRkNkFkQ2tSLzR6ZENoaVJkTjhhK3pURTVVPQ==;Mgo+DSMBMAY9C3t2VVhkQlFacldJXnxAYVF2R2BJfFR0d19DZEwxOX1dQl9gSXxSdURrWH5ecHdVQ2Y=;MTEyMjc5MkAzMjMwMmUzNDJlMzBHTzNjMENJa3dqTHJhdW1MdUcvWmtDK1lrbGdsVkV2WFhHblhiOUhZNC93PQ==;MTEyMjc5M0AzMjMwMmUzNDJlMzBaV1J4T1JFUVNsQlFoTnl4dkkvenAySUY4bVFUODFad1d3VXJZbCtVQ0NZPQ==;MTEyMjc5NEAzMjMwMmUzNDJlMzBPeTVJTk53a3R2MXJnK1NwWEFReVNnSVJDMzh1RDc1eitVdjhyTDNqS05NPQ==");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();


app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
