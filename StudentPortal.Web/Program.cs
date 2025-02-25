using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using Serilog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;

var builder = WebApplication.CreateBuilder(args);

// Set up logging
builder.Logging
	.ClearProviders() // Optional: Clear default providers
	.AddConsole() // Log to the console
	.AddDebug() // Log to the Debug window
	.SetMinimumLevel(LogLevel.Debug); // Set the minimum log level

// Set up Serilog to log to a file
Log.Logger = new LoggerConfiguration()
	.WriteTo.Console() // Logs to the console
	.WriteTo.File(@"E:\C#DotNetW3-codes_E-Drive\StudentPortalWeb_Re\StudentPortal.Web\Logs\MyLogs_Re-.log", rollingInterval: RollingInterval.Hour)
	.MinimumLevel.Information()  // Only logs Information level and above
	.CreateLogger();

// Add Serilog to the logging pipeline
builder.Host.UseSerilog(); // This replaces the default logging provider

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StudentPortal")));

builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.

//if (app.Environment.IsDevelopment())
//{
//	// In Development, use the Developer Exception Page to show detailed error information
//	app.UseDeveloperExceptionPage();
//}
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// Redirect to the error page if an exception occurs
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();  // Adds HTTP Strict Transport Security (HSTS) for production security
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Students}/{action=List}/{id?}");

app.Run();
