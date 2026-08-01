using JELMusic.Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace JELMusic.Infrastructure.Tests.Infrastructure;

public sealed class SqliteTestDatabase : IDisposable
{
    private readonly SqliteConnection _connection;

    public CoreDbContext Context { get; }

    public SqliteTestDatabase()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<CoreDbContext>()
            .UseSqlite(_connection)
            .Options;

        Context = new CoreDbContext(options);

        Context.Database.Migrate();
    }

    public void Dispose()
    {
        Context.Dispose();
        _connection.Dispose();
    }
}