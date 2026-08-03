using JELMusic.Application.Abstractions.Dispatching;
using Microsoft.Extensions.DependencyInjection;

namespace JELMusic.Framework.Dispatching;

public sealed class ApplicationDispatcher : IApplicationDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public ApplicationDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> SendCommandAsync<TCommand, TResult>(
        TCommand command,
        CancellationToken cancellationToken = default)
        where TCommand : ICommand<TResult>
    {
        ArgumentNullException.ThrowIfNull(command);

        var handler = _serviceProvider
            .GetRequiredService<ICommandHandler<TCommand, TResult>>();

        return await handler.HandleAsync(
            command,
            cancellationToken);
    }

    public async Task<TResult?> SendQueryAsync<TQuery, TResult>(
        TQuery query,
        CancellationToken cancellationToken = default)
        where TQuery : IQuery<TResult>
    {
        ArgumentNullException.ThrowIfNull(query);

        var handler = _serviceProvider
            .GetRequiredService<IQueryHandler<TQuery, TResult>>();

        return await handler.HandleAsync(
            query,
            cancellationToken);
    }
}