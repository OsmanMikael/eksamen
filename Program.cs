using Eksamen.Models;
using Microsoft.EntityFrameworkCore;
using Eksamen.DAL;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Identity;
using eksamen.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ProductDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ProductDbContextConnection' not found.");

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ProductDbContext>(options => {
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:ProductDbContextConnection"]);
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ProductDbContext>();

// builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
// {
//     //Passwoed settings
//     options.Password.RequireDigit = true;
//     options.Password.RequiredLength = 8;
//     options.Password.RequireNonAlphanumeric = true;
//     options.Password.RequireUppercase = true;
//     options.Password.RequireLowercase = true;
//     options.Password.RequiredUniqueChars = 6;

//     //Lockout settings
//     options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
//     options.Lockout.MaxFailedAccessAttempts = 5;
//     options.Lockout.AllowedForNewUsers = true;

//     //User settings
//     options.User.RequireUniqueEmail = true;
// })
//     .AddEntityFrameworkStores<ProductDbContext>().AddDefaultUI()
//     .AddDefaultTokenProviders(); 

builder.Services.AddScoped<IItemRepository, ItemRepository>();

builder.Services.AddRazorPages();
builder.Services.AddSession();

/*builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    options.Cookie.IsEssential = true;
}); */

var loggerConfiguration = new LoggerConfiguration()
    .MinimumLevel.Information() // levels: Trace< Information < Warning < Erorr < Fatal
    .WriteTo.File($"Logs/app_{DateTime.Now:yyyyMMdd_HHmmss}.log");

loggerConfiguration.Filter.ByExcluding(e => e.Properties.TryGetValue("SourceContext", out var value) &&
                            e.Level == LogEventLevel.Information &&
                            e.MessageTemplate.Text.Contains("Executed DbCommand"));

var logger = loggerConfiguration.CreateLogger();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    DBInit.Seed(app);
}

app.UseStaticFiles();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthentication();

app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();

