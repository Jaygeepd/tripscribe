using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using tripscribe.Dal.Contexts;
using tripscribe.Dal.Interfaces;

namespace Tripscribe.Api.Integration.Test.Base;

public class IntegrationClassFixture : IDisposable
{
    public readonly WebApplicationFactory<Program> Host;

    public IntegrationClassFixture()
    {
        Host = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(e =>
                {
                    e.AddDbContext<TripscribeContext>(options => options
                            .EnableSensitiveDataLogging()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString()),
                        ServiceLifetime.Singleton,
                        ServiceLifetime.Singleton);
                    e.AddTransient<ITripscribeDatabase, TripscribeContext>();
                });
            });
        DatabaseSeed.SeedDatabase(Host.Services.GetService<TripscribeContext>());
    }

    public void Dispose()
    {
        Host?.Dispose();
    }
}