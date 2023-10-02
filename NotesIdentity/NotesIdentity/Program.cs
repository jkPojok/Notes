using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotesIdentity;
using NotesIdentity.Data;
using NotesIdentity.Entities;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllersWithViews();

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
    .AddInMemoryApiResources(IdentityServerConfiguration.AddApiResources())
    .AddInMemoryIdentityResources(IdentityServerConfiguration.AddIdentityResources())
    .AddInMemoryApiScopes(IdentityServerConfiguration.AddScopes())
    .AddInMemoryClients(IdentityServerConfiguration.AddClients())
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseIdentityServer();
app.MapControllers();

app.Run();
