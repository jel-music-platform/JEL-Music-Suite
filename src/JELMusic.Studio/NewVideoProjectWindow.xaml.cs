using System.Windows;
using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.Queries.ListMusicalProjects;
using JELMusic.Application.VideoProjects.CreateVideoProject;

namespace JELMusic.Studio;

public partial class NewVideoProjectWindow : Window
{
    private readonly IApplicationDispatcher _dispatcher;

    public NewVideoProjectWindow(
        IApplicationDispatcher dispatcher)
    {
        InitializeComponent();

        _dispatcher = dispatcher;

        Loaded += NewVideoProjectWindow_Loaded;
    }

    private async void NewVideoProjectWindow_Loaded(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            var result = await _dispatcher.SendQueryAsync<
                ListMusicalProjectsQuery,
                ListMusicalProjectsResult>(
                    new ListMusicalProjectsQuery());

            if (result is null)
                return;

            MusicalProjectComboBox.ItemsSource = result.Projects;
            MusicalProjectComboBox.DisplayMemberPath = nameof(
                MusicalProjectListItem.Name);
            MusicalProjectComboBox.SelectedValuePath = nameof(
                MusicalProjectListItem.ProjectId);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error al cargar proyectos musicales",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private async void CreateVideoProject_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            if (MusicalProjectComboBox.SelectedValue is not Guid musicalProjectId ||
                musicalProjectId == Guid.Empty)
            {
                MessageBox.Show(
                    "Selecciona un proyecto musical.",
                    "Crear proyecto de vídeo",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show(
                    "Introduce un nombre para el proyecto de vídeo.",
                    "Crear proyecto de vídeo",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                NameTextBox.Focus();
                return;
            }

            if (!TimeSpan.TryParse(
                    DurationTextBox.Text,
                    out var duration) ||
                duration < TimeSpan.Zero)
            {
                MessageBox.Show(
                    "Introduce una duración válida, por ejemplo 00:04:28.",
                    "Crear proyecto de vídeo",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                DurationTextBox.Focus();
                return;
            }

            var projectId = await _dispatcher.SendCommandAsync<
                CreateVideoProjectCommand,
                Guid>(
                    new CreateVideoProjectCommand(
                        musicalProjectId,
                        NameTextBox.Text.Trim(),
                        ConceptTextBox.Text.Trim(),
                        duration));

            MessageBox.Show(
                $"Proyecto de vídeo creado correctamente.\n\nId: {projectId}",
                "JEL-Music Studio",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            DialogResult = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error al crear proyecto de vídeo",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void Cancel_Click(
        object sender,
        RoutedEventArgs e)
    {
        DialogResult = false;
    }
}