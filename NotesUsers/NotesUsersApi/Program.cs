using Application;
using Infrastructure;
using MassTransit;

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
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
