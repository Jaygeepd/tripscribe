using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using tripscribe.Api.testDI;
using tripscribe.Dal.Contexts;
using tripscribe.Dal.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IDoStuff, DoStuff2>(); // Interface implementation to handle 
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Tripscribe API",
        Description = "An ASP.NET Core Web API for the Tripscribe backend",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});
builder.Services.AddFluentValidation(s =>
    s.RegisterValidatorsFromAssemblyContaining<Program>()
);

builder.Services.AddScoped<ITripscribeDatabase, TripscribeContext>(_ => new TripscribeContext("Server=localhost;Port=5432;Database=tripscribe;User Id=postgres;Password=password;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
