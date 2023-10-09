using Application;
using Infrastructure;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowServices", builder =>
    builder
    .WithOrigins("http://localhost:3000", "http://localhost:5101", "https://localhost:7280")
    .AllowAnyHeader()
    .AllowAnyMethod()
    );
});

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
    {
        config.Authority = "https://localhost:7280/";
        config.Audience = "NotesAppApiResources";
        config.RequireHttpsMetadata = false;
    });

builder.Services.AddMassTransit(massTransitConfig =>
{
    var assembly = typeof(Program).Assembly;
    massTransitConfig.AddConsumers(assembly);
    massTransitConfig.UsingRabbitMq((context, RabbitConfig) =>
    {
        RabbitConfig.Host("localhost", "/", config =>
        {
            config.Username("guest");
            config.Password("guest");
        });
        RabbitConfig.ConfigureEndpoints(context);
    });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();
app.UseCors("AllowServices");
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
