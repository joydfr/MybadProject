using Microsoft.EntityFrameworkCore;
using MvcMyBad.Data;
using MvcMyBad.Models;
using MvcMyBad.Services;
using MvcMyBad.Interfaces;
using Microsoft.OpenApi.Models;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Charger les variables d'environnement du fichier .env
DotNetEnv.Env.Load();

// Construire la chaîne de connexion dynamiquement
var connectionString = (builder.Configuration.GetConnectionString("DefaultConnection") ?? "")
    .Replace("${DB_HOST}", Environment.GetEnvironmentVariable("DB_HOST") ?? "")
    .Replace("${DB_NAME}", Environment.GetEnvironmentVariable("DB_NAME") ?? "")
    .Replace("${DB_USER}", Environment.GetEnvironmentVariable("DB_USER") ?? "")
    .Replace("${DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "");

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MybadDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "MyBad API",
            Version = "v1",
            Description = "Une API pour gérer vos événements de badminton !",
            Contact = new OpenApiContact
            {
                Name = "Mybad Support",
                Email = "Mybad@example.com",
                Url = new Uri("https://www.Mybad.com"),
            },
        }
    );
    c.EnableAnnotations();
});

builder.Services.AddScoped<UserService>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBad API V1");
    c.RoutePrefix = "swagger";
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}").WithStaticAssets();
app.Run();
