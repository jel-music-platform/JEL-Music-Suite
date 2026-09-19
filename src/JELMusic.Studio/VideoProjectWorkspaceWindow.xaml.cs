using System.Windows;
using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.Queries.GetVideoProjectById;

namespace JELMusic.Studio;

public partial class VideoProjectWorkspaceWindow : Window
{
    public VideoProjectWorkspaceWindow(
        IApplicationDispatcher dispatcher,
        Guid videoProjectId)
    {
        InitializeComponent();

        Loaded += async (_, _) =>
        {
            try
            {
                var result = await dispatcher.SendQueryAsync<
                    GetVideoProjectByIdQuery,
                    GetVideoProjectByIdResult>(
                        new GetVideoProjectByIdQuery(videoProjectId));

                if (result is null)
                {
                    MessageBox.Show(
                        "No se ha encontrado el proyecto de vídeo.",
                        "Proyecto no encontrado",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    DialogResult = false;
                    return;
                }

                NameTextBlock.Text = result.Name;
                StatusTextBlock.Text = result.Status.ToString();
                ConceptTextBlock.Text = result.Concept;
                DurationTextBlock.Text = result.Duration.ToString(@"mm\:ss");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al cargar el espacio de trabajo",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                DialogResult = false;
            }
        };
    }
}