using Microsoft.EntityFrameworkCore;
using EsettMvcIntegration.Data;
using EsettMvcIntegration.Services;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
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
