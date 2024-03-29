using LeaveManagement.Application.Contracts;
using LeaveManagement.Data;
using LeaveManagement.Application.Configurations;
using LeaveManagement.Web.Services;
using LeaveManagement.Application.Repositories___Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using LeaveManagement.Web.Services.Identity;
using LeaveManagement.Web.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<Employee>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddClaimsPrincipalFactory<CustomClaimService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IEmailSender>(option => new EmailSender("localhost", 25, "no-reply@leavemanagement.com"));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
builder.Services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));  

builder.Services.AddControllersWithViews();

//builder.Services.AddMvc(options =>
//{
//    options.EnableEndpointRouting = false;
//    //options.Filters.Add(typeof(ValidateAntiForgeryTokenFilter)); // Optional: If you have an anti-forgery token filter
//})
//    .AddRazorRuntimeCompilation()
//    .AddApplicationPart(typeof(DemoLoginController).Assembly); // Add this line

var app = builder.Build();

app.UseSerilogRequestLogging();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
