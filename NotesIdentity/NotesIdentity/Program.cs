using Microsoft.AspNetCore.Identity;
using NotesIdentity.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<AppUser, IdentityRole>(config =>
{
    config.Password.RequiredLength = 4;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireDigit = false;
    config.Password.RequireLowercase = false;
    config.Password.RequireUppercase = false;
    config.Password.RequiredUniqueChars = 2;
})
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<AppUser>()
    .AddInMemoryApiResources()
    .AddInMemoryIdentityResources()
    .AddInMemoryApiScopes()
    .AddInMemoryClients()
    .AddDeveloperSigningCredential();

var app = builder.Build();



app.Run();
