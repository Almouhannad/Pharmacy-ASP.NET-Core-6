using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.DomainServices;
using Pharmacy.Application.NotificationsServices;
using Pharmacy.Core.Entities.General.Users;
using Pharmacy.Core.Interfaces.IRepositories;
using Pharmacy.Core.Interfaces.IRepositories.Logs;
using Pharmacy.Core.Interfaces.IServices;
using Pharmacy.Core.Interfaces.IServices.Notifications;
using Pharmacy.Core.Interfaces.IUnitOfWork;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Repositories;
using Pharmacy.Infrastructure.Repositories.Logs;
using Pharmacy.Infrastructure.UnitOfWork;
using Pharmacy.Web.Data;

var builder = WebApplication.CreateBuilder(args);

#region Add DbContext

builder.Services.AddDbContext<PharmacyDbContext>
    (
        options =>
        {
            options.UseSqlServer
                (
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Pharmacy.Infrastructure")
                );
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        }
    );
#endregion

#region Add DI Mappings

#region Base repository and service
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseService<,,,>), typeof(BaseService<,,,>));
#endregion

#region Notifications repository ans service
builder.Services.AddScoped(typeof(IAddNewCaseLogRepository), typeof(AddNewCaseLogRepository));
builder.Services.AddScoped(typeof(IAddNewCaseNotificationService), typeof(AddNewCaseNotificationService));

#endregion

#region Service pool
builder.Services.AddScoped(typeof(IServicePool), typeof(ServicePool));

#endregion

#region Unit of work
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
#endregion


#endregion

#region Add identity

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<PharmacyDbContext>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });

#endregion


// Add services to the container.


builder.Services.AddControllersWithViews();

var app = builder.Build();

#region Seed roles and admin
await Seed.SeedUsersAndRolesAsync(app);
#endregion

#region Seed database
await Seed.SeedDatabase(app);
#endregion



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

#region Handle routing problems
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Response.Redirect("/Errors/NotFound");
    }
    else if (context.Response.StatusCode == 405)
    {
        context.Response.Redirect("/Errors/InternalServerError");
    }
});
#endregion


// Very important
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");





app.Run();
