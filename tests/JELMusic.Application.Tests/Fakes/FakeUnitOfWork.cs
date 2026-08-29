using JELMusic.Domain.Repositories;

namespace JELMusic.Application.Tests.Fakes;

public sealed class FakeUnitOfWork : IUnitOfWork
{
    public bool Saved { get; private set; }

    public CancellationToken LastCancellationToken { get; private set; }

    public Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        LastCancellationToken = cancellationToken;
        Saved = true;
        return Task.FromResult(1);
    }
}