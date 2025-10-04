global using FastEndpoints;
using Configs;
using FastEndpoints.Swagger;
using FastEndpointsBasicAuthEf.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication()
    .AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("Basic", _ => { });

builder.Services.RegisterServices();
builder.Services.AddAuthorization();
builder.Services.AddFastEndpoints();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddSwaggerConfig();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();
app.UseSwaggerGen();

app.Run();
