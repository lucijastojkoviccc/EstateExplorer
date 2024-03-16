using EstateExplorer;
using EstateExplorer.Data;
using EstateExplorer.Core;
using EstateExplorer.Helpers;
using EstateExplorer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Quartz;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using EstateExplorer.Core.Repositories;
using EstateExplorer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
 .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddAuthentication()
   .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


#region Authorization

AddAuthorizationPolicies();
#endregion

AddScoped();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHelpers();

builder.Services.AddCors(options => {
    options.AddPolicy(name: "Pufla",
                      builder => {
                          builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
});
// Add the Quartz.NET services
builder.Services.AddQuartz(q =>
{
    // Use a Scoped container to create jobs. I'll touch on this later
   
    q.UseMicrosoftDependencyInjectionJobFactory();

    // Create a "key" for the job
    var jobKey = new JobKey("HelloWorldJob");

    // Register the job with the DI container
    q.AddJob<QuartzJob>(opts => opts.WithIdentity(jobKey));

    // Create a trigger for the job
    q.AddTrigger(opts => opts
        .ForJob(jobKey) // link to the HelloWorldJob
        .WithIdentity("HelloWorldJob-trigger") // give the trigger a unique name
        .WithCronSchedule("0 0 8 * * ? *"));//0 0 8 * * ? * //0/5 * * * * ?
});

// Add the Quartz.NET hosted service
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("Pufla");

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapFallbackToFile("index.html");


app.Run();

void AddAuthorizationPolicies()
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
    });
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(EstateExplorer.Core.Constants.Policies.RequireAdmin, policy => policy.RequireRole(EstateExplorer.Core.Constants.Roles.Admin));
        options.AddPolicy(EstateExplorer.Core.Constants.Policies.RequireAdministrativniRadnik, policy => policy.RequireRole(EstateExplorer.Core.Constants.Roles.AdministrativniRadnik));
        options.AddPolicy(EstateExplorer.Core.Constants.Policies.RequireInvestitor, policy => policy.RequireRole(EstateExplorer.Core.Constants.Roles.Investitor));
        options.AddPolicy(EstateExplorer.Core.Constants.Policies.RequireKupac, policy => policy.RequireRole(EstateExplorer.Core.Constants.Roles.Kupac));
    });
}
void AddScoped()
{
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IRoleRepository, RoleRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
}

