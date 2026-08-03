using JELMusic.Application.Abstractions.Dispatching;
using Microsoft.Extensions.DependencyInjection;

namespace JELMusic.Infrastructure.Dispatching;

public sealed class ApplicationDispatcher : IApplicationDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public ApplicationDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider
            ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public Task<TResult> SendAsync<TResult>(
        ICommand<TResult> command,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> SendAsync<TResult>(
        IQuery<TResult> query,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}