using Ticketing.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ticketing.Application.Common.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Ticketing.Infrastructure.Persistence.Repositories;

namespace Ticketing.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<TicketingDbContext>(
            options => options.UseSqlite(configuration
            .GetConnectionString("TicketingDbContext")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();

        return services;
    }
}