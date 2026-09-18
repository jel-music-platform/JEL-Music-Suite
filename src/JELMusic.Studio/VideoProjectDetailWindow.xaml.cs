using System.Windows;
using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.Queries.GetVideoProjectById;

namespace JELMusic.Studio;

public partial class VideoProjectDetailWindow : Window
{
    public VideoProjectDetailWindow(
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
                MusicalProjectIdTextBlock.Text =
                    result.MusicalProjectId.ToString();
                ConceptTextBlock.Text = result.Concept;
                DurationTextBlock.Text = result.Duration.ToString();
                StatusTextBlock.Text = result.Status.ToString();
                CreatedAtTextBlock.Text =
                    result.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al cargar el proyecto de vídeo",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                DialogResult = false;
            }
        };
    }

    private void Close_Click(
        object sender,
        RoutedEventArgs e)
    {
        DialogResult = false;
    }
}
