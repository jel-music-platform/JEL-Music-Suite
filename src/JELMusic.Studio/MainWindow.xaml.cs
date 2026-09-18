using System.Windows;
using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.Queries.GetVideoProjectById;
using JELMusic.Application.Queries.ListVideoProjects;
using Microsoft.Extensions.DependencyInjection;

namespace JELMusic.Studio;

public partial class MainWindow : Window
{
    private readonly IServiceProvider _serviceProvider;

    public MainWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;

        Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            var dispatcher = _serviceProvider
                .GetRequiredService<IApplicationDispatcher>();

            var result = await dispatcher.SendQueryAsync<
                ListVideoProjectsQuery,
                ListVideoProjectsResult>(
                    new ListVideoProjectsQuery());

            if (result is null)
                return;

            VideoProjectsListBox.ItemsSource = result.Projects;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error al cargar proyectos de vídeo",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private async void NewVideoProject_Click(
        object sender,
        RoutedEventArgs e)
    {
        var dispatcher = _serviceProvider
            .GetRequiredService<IApplicationDispatcher>();

        var window = new NewVideoProjectWindow(dispatcher)
        {
            Owner = this
        };

        var result = window.ShowDialog();

        if (result == true)
        {
            await LoadVideoProjectsAsync();
        }
    }

    private async Task LoadVideoProjectsAsync()
    {
        try
        {
            var dispatcher = _serviceProvider
                .GetRequiredService<IApplicationDispatcher>();

            var result = await dispatcher.SendQueryAsync<
                ListVideoProjectsQuery,
                ListVideoProjectsResult>(
                    new ListVideoProjectsQuery());

            if (result is null)
                return;

            VideoProjectsListBox.ItemsSource = result.Projects;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error al cargar proyectos de vídeo",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private async void OpenVideoProject_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            if (sender is not FrameworkElement element ||
                element.DataContext is not VideoProjectListItem project)
            {
                MessageBox.Show(
                    "No se ha podido identificar el proyecto de vídeo.",
                    "Abrir proyecto",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            var dispatcher = _serviceProvider
                .GetRequiredService<IApplicationDispatcher>();

            var result = await dispatcher.SendQueryAsync<
                GetVideoProjectByIdQuery,
                GetVideoProjectByIdResult>(
                    new GetVideoProjectByIdQuery(
                        project.VideoProjectId));

            if (result is null)
            {
                MessageBox.Show(
                    "No se ha encontrado el proyecto de vídeo.",
                    "Abrir proyecto",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            var window = new VideoProjectDetailWindow(
                dispatcher,
                result.VideoProjectId)
            {
                Owner = this
            };

            window.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error al abrir el proyecto de vídeo",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
