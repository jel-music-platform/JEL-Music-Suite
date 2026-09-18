using System.Windows;
using JELMusic.Application.Abstractions.Dispatching;
using Microsoft.Extensions.DependencyInjection;

namespace JELMusic.Studio;

public partial class MainWindow : Window
{
    private readonly IServiceProvider _serviceProvider;

    public MainWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;
    }

    private void NewVideoProject_Click(
        object sender,
        RoutedEventArgs e)
    {
        var dispatcher = _serviceProvider
            .GetRequiredService<IApplicationDispatcher>();

        var window = new NewVideoProjectWindow(dispatcher)
        {
            Owner = this
        };

        window.ShowDialog();
    }
}