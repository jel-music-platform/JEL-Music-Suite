namespace JELMusic.Application.Abstractions.Dispatching;

public interface IApplicationDispatcher
{
    Task<TResult> SendAsync<TResult>(
        ICommand<TResult> command,
        CancellationToken cancellationToken = default);

    Task<TResult> SendAsync<TResult>(
        IQuery<TResult> query,
        CancellationToken cancellationToken = default);
}