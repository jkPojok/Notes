using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowServices", builder =>
    builder
    .WithOrigins("http://localhost:3000")
    .WithOrigins("http://localhost:5101")
    .AllowAnyHeader()
    .AllowAnyMethod());
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowServices");

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.MapControllers();

app.Run();
