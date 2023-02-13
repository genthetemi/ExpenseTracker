using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DI
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

//Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHRqVVhkVFpHaVZdX2NLfUN1T2dadVt0ZDU7a15RRnVfQFxgSXdXfkVrUH9ZdQ==;Mgo+DSMBPh8sVXJ0S0J+XE9AflRBQmpWfFN0RnNadVpzflBCcDwsT3RfQF5jS39QdkxnUX9WeXZVQw==;ORg4AjUWIQA/Gnt2VVhkQlFacldJXnxAYVF2R2BJfFR0d19DZEwxOX1dQl9gSXxSdUVqXXdeeH1WQ2U=;MTEyMDU4OUAzMjMwMmUzNDJlMzBVYjB0WFZ1cmlRTGdSWVlZR0pWM2FRMHIyaFhySkxkdUF4SkRvRSs4c1lRPQ==;MTEyMDU5MEAzMjMwMmUzNDJlMzBGb0ZkVzJPaG5wK09sREJmMXdBMXpRTk5vbWl3eXlmank1Vi81TUFTNFpRPQ==;NRAiBiAaIQQuGjN/V0Z+WE9EaFtKVmBWd0x0RWFab116cFZMZVhBJAtUQF1hSn5Rd0ZjUHtWcH1VR2Nd;MTEyMDU5MkAzMjMwMmUzNDJlMzBBc29KQlhWM3BSVWV3ZlZlaFR3UTVjSEUwV0FXcGpXb0h5VEE5bStJUnZ3PQ==;MTEyMDU5M0AzMjMwMmUzNDJlMzBpb3FwRHEzdVZ6djZkU0h3WG1GYzNMODBoQUhtZVUwUHJtcnAzVlltOG9zPQ==;Mgo+DSMBMAY9C3t2VVhkQlFacldJXnxAYVF2R2BJfFR0d19DZEwxOX1dQl9gSXxSdUVqXXdeeXRSRGY=;MTEyMDU5NUAzMjMwMmUzNDJlMzBLQVVRclIvK09mWVRpbGxtTEdHOFA5Nys0ajhCTjNSQ1ExcWJQRDFuSkJrPQ==;MTEyMDU5NkAzMjMwMmUzNDJlMzBQelhELzZ3TFBZNXQrSTNmODZScjdBNkx6MStpRlpBenlQUnNwYjE1YXpjPQ==;MTEyMDU5N0AzMjMwMmUzNDJlMzBBc29KQlhWM3BSVWV3ZlZlaFR3UTVjSEUwV0FXcGpXb0h5VEE5bStJUnZ3PQ==");

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
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
