global using FastEndpoints;

using FastEndpoints.Swagger;
using FastEndpointsBasicAuthEf.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services
   .AddFastEndpoints()
   .SwaggerDocument();

var app = builder.Build();
app.UseFastEndpoints()
   .UseSwaggerGen();

app.Run();
