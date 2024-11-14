// using API.Extensions;

using Microsoft.EntityFrameworkCore;

using Waterlily.Api.Middleware;
using Waterlily.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register application services here (before calling builder.Build())
builder.Services.AddApplicationServices(builder.Configuration);

// Register controllers
builder.Services.AddControllers();

// Register Swagger (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Middleware (optional)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
            .WithOrigins("http://localhost:5200", "https://localhost:5201");
    });
});

// Build the app after all services have been registered
var app = builder.Build();

// Use Middleware (optional)
app.UseMiddleware<ExceptionMiddleware>();

// Set up Swagger (optional)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Set up routing and controllers
app.MapControllers();

app.Run();
