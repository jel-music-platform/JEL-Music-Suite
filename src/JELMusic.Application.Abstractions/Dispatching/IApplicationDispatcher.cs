namespace JELMusic.Application.Abstractions.Dispatching;

public interface IApplicationDispatcher
{
    Task<TResult> SendCommandAsync<TCommand, TResult>(
        TCommand command,
        CancellationToken cancellationToken = default)
        where TCommand : ICommand<TResult>;

    Task<TResult?> SendQueryAsync<TQuery, TResult>(
        TQuery query,
        CancellationToken cancellationToken = default)
        where TQuery : IQuery<TResult>;
}