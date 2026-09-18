using JELMusic.Application;
using JELMusic.Framework;
using JELMusic.Infrastructure;
using JELMusic.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JELMusic.Studio;

public partial class App : System.Windows.Application
{
    private ServiceProvider? _serviceProvider;

    protected override async void OnStartup(
        System.Windows.StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();

        services.AddApplication();
        services.AddJELMusicFramework();

        services.AddInfrastructure(
            "Data Source=jelmusic-core.db");

        _serviceProvider = services.BuildServiceProvider();

        using (var scope = _serviceProvider.CreateScope())
        {
            var db = scope.ServiceProvider
                .GetRequiredService<CoreDbContext>();

            await db.Database.MigrateAsync();
        }

        var mainWindow = new MainWindow(_serviceProvider);
        mainWindow.Show();
    }

    protected override void OnExit(
        System.Windows.ExitEventArgs e)
    {
        _serviceProvider?.Dispose();

        base.OnExit(e);
    }
}