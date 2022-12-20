using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Tripscribe.Api;
using Tripscribe.Api.Authentication;
using tripscribe.Api.Filters;
using tripscribe.Dal.Contexts;
using tripscribe.Dal.Interfaces;
using tripscribe.Services.Services;
using AuthenticationService = tripscribe.Services.Services.AuthenticationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GeneralExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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

builder.Services.AddAuthentication(string.Empty).AddScheme<AuthenticationSchemeOptions, AccessAuthenticationFilter>(string.Empty, options => {});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(string.Empty, policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });
});

builder.Services.AddScoped<ITripscribeDatabase, TripscribeContext>(_ => new TripscribeContext(EnvironmentVariables.DbConnectionString));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IJourneyService, JourneyService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IStopService, StopService>();
builder.Services.AddScoped<IAuthenticateService, AuthenticationService>();
builder.Services.AddScoped<IAuthorizedAccountProvider, AuthorizedAccountProvider>();
builder.Services.AddAutoMapper(config => config.AllowNullCollections = true, typeof(Program).Assembly, typeof(AccountService).Assembly);
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.MapHealthChecks("/health");

app.Run();

public partial class Program { };
