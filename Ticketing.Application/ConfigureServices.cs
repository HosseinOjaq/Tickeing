using Ticketing.Application.Services;
using Ticketing.Application.Common.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Ticketing.Application;

public static class ConfigureServices
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITicketService, TicketService>();

        return services;
    }
}