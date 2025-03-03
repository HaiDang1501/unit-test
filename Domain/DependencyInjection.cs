using Domain.Followers;
using Domain.Persistence;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

        services.AddScoped<IFollowerRepository, FollowerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        //services.AddScoped<FollowerService>();
        return services;
    }
}