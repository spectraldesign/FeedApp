using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Database;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FeedAppDbContext>(o =>
        o.UseNpgsql(configuration.GetConnectionString("ConnectionString")));

        services.AddScoped<IFeedAppDbContext>(provider => provider.GetService<FeedAppDbContext>());

        return services;
    }
}