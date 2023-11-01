using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using NotesIdentity;
using NotesIdentity.Data;
using NotesIdentity.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(config =>
{
    config.AddPolicy("AllowServices", builder =>
    {
        builder
        .AllowAnyOrigin()
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
        rabbitConfig.Host(/*"notesrabbit*/ "localhost", "/", config =>
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

IdentityModelEventSource.ShowPII = true;

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
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None;
});

builder.Services.AddIdentityServer(/*x => x.IssuerUri = "http://notesidentity"*/)    // THE MOST IMPORTANT PART
    .AddAspNetIdentity<AppUser>()
    .AddInMemoryClients(IdentityServerConfiguration.AddClients())
    .AddInMemoryApiScopes(IdentityServerConfiguration.AddScopes())
    .AddInMemoryApiResources(IdentityServerConfiguration.AddApiResources())
    .AddInMemoryIdentityResources(IdentityServerConfiguration.AddIdentityResources())
    .AddDeveloperSigningCredential();

var app = builder.Build();

//var fordwardedHeaderOptions = new ForwardedHeadersOptions
//{
//    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
//};
//fordwardedHeaderOptions.KnownNetworks.Clear();
//fordwardedHeaderOptions.KnownProxies.Clear();

//app.UseForwardedHeaders(fordwardedHeaderOptions);

app.UseStaticFiles();

app.UseCors("AllowServices");
app.MapControllers();
app.UseIdentityServer();

app.Run();
