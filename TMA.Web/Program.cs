using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using TMA.Api.Common;
using TMA.Infrastructure;
using TMA.Middleware;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Optional: log to the console
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day) // Log to a text file, with a new file created daily
    .CreateLogger();

// Use Serilog for logging
builder.Logging.ClearProviders(); // Remove other loggers
builder.Logging.AddSerilog();

// Bind AppConfig
var appconfig = new AppConfig();
builder.Configuration.Bind("AnalyticsAppsettings", appconfig);
builder.Services.AddSingleton(appconfig);

builder.Services.AddCors(options =>
{
       options.AddPolicy("AllowLocalhost4200",
        builder => builder
            .WithOrigins("http://localhost:4200") 
            .AllowAnyHeader()                    
            .AllowAnyMethod()                     
            .AllowCredentials());  
    
});


// Add services to the container.

builder.Services.AddControllersWithViews();


// Set DB Context
Infrastructure.SetDBContext(builder.Services,configuration);

// Dependency Injection
Infrastructure.ConfigureServices(builder.Services);

// Configure JWT Authentication

string issuer = builder.Configuration.GetValue<string>("Jwt:Issuer") ?? "DefaultIssuer";
string audience = builder.Configuration.GetValue<string>("Jwt:Audience") ?? "DefaultAudience";
string key = builder.Configuration.GetValue<string>("Jwt:Key") ?? "DefaultSecretKey"; // Ensure this is a valid key

// Ensure your key is long enough for HMACSHA256; typically at least 16 characters
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

// Add Authorization policies for roles (if required)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireManagerRole", policy => policy.RequireRole("Manager"));
    options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
});

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1", Description = "API documentation for My API" });
});

var app = builder.Build();
app.UseCors("AllowLocalhost4200");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Register GlobalExceptionMiddleware first to catch any unhandled exceptions
app.UseMiddleware<GlobalExceptionMiddleware>();

// Register AuthorizationHeaderMiddleware to manipulate the Authorization header
app.UseMiddleware<AuthorizationHeaderMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve Swagger UI (HTML, JS, CSS, etc.),
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
