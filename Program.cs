global using FastEndpoints;

using FastEndpoints.Swagger;
using FastEndpoints.Security;

var builder = WebApplication.CreateBuilder();
builder.Services
   .AddFastEndpoints()
   .SwaggerDocument();

var app = builder.Build();
app.UseFastEndpoints()
   .UseSwaggerGen();

app.Run();
