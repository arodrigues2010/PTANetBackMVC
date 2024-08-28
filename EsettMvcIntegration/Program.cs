using Microsoft.EntityFrameworkCore;
using EsettMvcIntegration.Data;
using EsettMvcIntegration.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on a specific port (e.g., 5001)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5001); // Se ejecuta Swagger 
});

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure HttpClient for FeeDataService
builder.Services.AddHttpClient<FeeDataService>(client =>
{
    client.BaseAddress = new Uri("https://api.opendata.esett.com/");
});

// Register scoped services
builder.Services.AddScoped<FeeDataRepository>();
builder.Services.AddScoped<FeeDataService>();

// Register controllers with views
builder.Services.AddControllersWithViews();

// Add Swagger generation to the services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Documentation",
        Version = "v1",
        Description = "API for managing fees data.",
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve Swagger UI
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Map routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
