using Domain.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace Domain.IntegrationTests;


public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("qet-db")
        .WithPassword("postgres")
        .WithUsername("postgres")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(
            services =>
            {
                var descriptor = services
                    .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationDbContext>(
                    options =>
                    {
                        options
                            .UseNpgsql(_dbContainer.GetConnectionString());
                    });
            });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        _dbContainer.ExecScriptAsync("data.sql");
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }

}