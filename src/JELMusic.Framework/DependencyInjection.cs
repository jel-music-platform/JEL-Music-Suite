using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Framework.Dispatching;
using Microsoft.Extensions.DependencyInjection;

namespace JELMusic.Framework;

public static class DependencyInjection
{
    public static IServiceCollection AddJELMusicFramework(
        this IServiceCollection services)
    {
        services.AddScoped<IApplicationDispatcher, ApplicationDispatcher>();

        return services;
    }
}