using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NotesIdentity;
using NotesIdentity.Data;
using NotesIdentity.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(config =>
{
    config.AddPolicy("AllowServices", builder =>
    {
        builder
        .WithOrigins("http://localhost:3000", "https://localhost:7278")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddMassTransit(massTransitConfig =>
{
    var assembly = typeof(Program).Assembly;
    massTransitConfig.AddConsumers(assembly);
    massTransitConfig.UsingRabbitMq((context, rabbitConfig) =>
    {
        rabbitConfig.Host("localhost", "/", config =>
        {
            config.Username("guest");
            config.Password("guest");
        });
        rabbitConfig.ConfigureEndpoints(context);
    });
});

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddIdentity<AppUser, IdentityRole>(config =>
{
    config.Password.RequiredLength = 4;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireDigit = false;
    config.Password.RequireLowercase = false;
    config.Password.RequireUppercase = false;
    config.Password.RequiredUniqueChars = 2;
})
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/auth/login";
    options.LogoutPath = "/auth/logout";
});

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<AppUser>()
    .AddInMemoryClients(IdentityServerConfiguration.AddClients())
    .AddInMemoryApiScopes(IdentityServerConfiguration.AddScopes())
    .AddInMemoryApiResources(IdentityServerConfiguration.AddApiResources())
    .AddInMemoryIdentityResources(IdentityServerConfiguration.AddIdentityResources())
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Views/Styles")),
    RequestPath = "/views/styles"
});

app.UseRouting();
app.UseCors("AllowServices");
app.UseIdentityServer();
app.MapControllers();

app.Run();
