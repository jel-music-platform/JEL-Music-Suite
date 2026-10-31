using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.Queries.GetVideoProjectById;
using JELMusic.Application.Queries.ListVideoScenes;
using JELMusic.Application.VideoProjects.CreateVideoScene;
using JELMusic.Application.VideoProjects.UpdateVideoSceneTiming;

namespace JELMusic.Studio;

public partial class VideoProjectWorkspaceWindow : Window
{
    private readonly IApplicationDispatcher _dispatcher;
    private readonly Guid _videoProjectId;

    private TimeSpan _projectDuration;

    private IReadOnlyList<VideoSceneListItem> _scenes =
        Array.Empty<VideoSceneListItem>();

    private Guid? _selectedSceneId;

    public VideoProjectWorkspaceWindow(
        IApplicationDispatcher dispatcher,
        Guid videoProjectId)
    {
        InitializeComponent();

        _dispatcher = dispatcher;
        _videoProjectId = videoProjectId;

        SaveSceneTimingButton.IsEnabled = false;

        TimelineTrack.SizeChanged += (_, _) =>
        {
            DrawTimelineRuler(_projectDuration);
            DrawScenes(_scenes);
        };

        Loaded += async (_, _) =>
        {
            try
            {
                var result = await _dispatcher.SendQueryAsync<
                    GetVideoProjectByIdQuery,
                    GetVideoProjectByIdResult>(
                        new GetVideoProjectByIdQuery(_videoProjectId));

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
                DurationTextBlock.Text =
                    result.Duration.ToString(@"mm\:ss");

                _projectDuration = result.Duration;

                BuildTimeline(_projectDuration);

                await ReloadScenesAsync();
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

    private async Task ReloadScenesAsync()
    {
        var result = await _dispatcher.SendQueryAsync<
            ListVideoScenesQuery,
            ListVideoScenesResult>(
                new ListVideoScenesQuery(_videoProjectId));

        if (result is null)
            return;

        _scenes = result.Scenes;

        DrawScenes(_scenes);
        LoadSelectedScene();
    }

    private void BuildTimeline(TimeSpan duration)
    {
        TimelineRuler.Children.Clear();

        TimelineDurationTextBlock.Text =
            duration.ToString(@"mm\:ss");

        TimelineSelectedTimeTextBlock.Text = "00:00";

        DrawTimelineRuler(duration);
    }

    private void DrawTimelineRuler(TimeSpan duration)
    {
        TimelineRuler.Children.Clear();

        if (duration.TotalSeconds <= 0 ||
            TimelineRuler.ActualWidth <= 0)
        {
            return;
        }

        var totalSeconds = duration.TotalSeconds;
        var width = TimelineRuler.ActualWidth;

        var interval = totalSeconds <= 60 ? 10 : 30;

        for (var seconds = 0.0;
             seconds <= totalSeconds;
             seconds += interval)
        {
            var position = seconds / totalSeconds * width;

            var marker = new Border
            {
                Width = 1,
                Height = 8,
                Background = new SolidColorBrush(
                    Color.FromRgb(100, 100, 100))
            };

            Canvas.SetLeft(marker, position);
            Canvas.SetTop(marker, 0);
            TimelineRuler.Children.Add(marker);

            var label = new TextBlock
            {
                Text = TimeSpan.FromSeconds(seconds)
                    .ToString(@"mm\:ss"),
                FontSize = 10,
                Foreground = new SolidColorBrush(
                    Color.FromRgb(120, 120, 120))
            };

            Canvas.SetLeft(label, position + 4);
            Canvas.SetTop(label, 8);
            TimelineRuler.Children.Add(label);
        }
    }

    private void DrawScenes(
        IReadOnlyList<VideoSceneListItem> scenes)
    {
        TimelineScenesCanvas.Children.Clear();

        if (_projectDuration.TotalSeconds <= 0 ||
            TimelineScenesCanvas.ActualWidth <= 0)
        {
            return;
        }

        var totalSeconds = _projectDuration.TotalSeconds;
        var width = TimelineScenesCanvas.ActualWidth;

        foreach (var scene in scenes)
        {
            var startRatio =
                scene.StartTime.TotalSeconds / totalSeconds;

            var durationRatio =
                scene.Duration.TotalSeconds / totalSeconds;

            var left = Math.Clamp(
                startRatio * width,
                0,
                width);

            var sceneWidth = Math.Max(
                2,
                durationRatio * width);

            sceneWidth = Math.Min(
                sceneWidth,
                Math.Max(2, width - left));

            var isSelected =
                _selectedSceneId == scene.VideoSceneId;

            var sceneBorder = new Border
            {
                Width = sceneWidth,
                Height = 26,
                Background = new SolidColorBrush(
                    isSelected
                        ? Color.FromRgb(190, 90, 30)
                        : Color.FromRgb(55, 55, 55)),
                BorderBrush = new SolidColorBrush(
                    isSelected
                        ? Color.FromRgb(255, 170, 80)
                        : Color.FromRgb(90, 90, 90)),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(3),
                Padding = new Thickness(6, 0, 6, 0),
                VerticalAlignment = VerticalAlignment.Center,
                ToolTip =
                    $"{scene.Name}  " +
                    $"{scene.StartTime:mm\\:ss} - " +
                    $"{scene.EndTime:mm\\:ss}"
            };

            var sceneText = new TextBlock
            {
                Text = scene.Name,
                FontSize = 11,
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center,
                TextTrimming = TextTrimming.CharacterEllipsis
            };

            sceneBorder.Child = sceneText;
            sceneBorder.Cursor = Cursors.Hand;

            sceneBorder.MouseLeftButtonDown += (_, e) =>
            {
                _selectedSceneId = scene.VideoSceneId;

                DrawScenes(_scenes);
                LoadSelectedScene();
                e.Handled = true;
            };

            Canvas.SetLeft(sceneBorder, left);
            Canvas.SetTop(sceneBorder, 4);
            TimelineScenesCanvas.Children.Add(sceneBorder);
        }
    }

    private void LoadSelectedScene()
    {
        var scene = _scenes.FirstOrDefault(
            item => item.VideoSceneId == _selectedSceneId);

        if (scene is null)
        {
            SelectedSceneNameTextBlock.Text =
                "Ninguna escena seleccionada";
            SceneStartTimeTextBox.Text = "00:00";
            SceneEndTimeTextBox.Text = "00:00";
            SceneDurationTextBlock.Text = "00:00";

            SaveSceneTimingButton.IsEnabled = false;
            return;
        }

        SelectedSceneNameTextBlock.Text = scene.Name;

        SceneStartTimeTextBox.Text =
            scene.StartTime.ToString(@"mm\:ss");

        SceneEndTimeTextBox.Text =
            scene.EndTime.ToString(@"mm\:ss");

        SceneDurationTextBlock.Text =
            scene.Duration.ToString(@"mm\:ss");

        SaveSceneTimingButton.IsEnabled = true;
   }

    private void SceneTimingTextBox_TextChanged(
        object sender,
        TextChangedEventArgs e)
  {
       UpdateSceneDurationPreview();
  }
    private void UpdateSceneDurationPreview()
  {
            // Evita ejecutar el cálculo mientras WPF inicializa los controles.
        if (SceneStartTimeTextBox is null ||
            SceneEndTimeTextBox is null ||
            SceneDurationTextBlock is null)
    {
            return;
    }

        if (!TimeSpan.TryParseExact(
                SceneStartTimeTextBox.Text,
                @"mm\:ss",
                CultureInfo.InvariantCulture,
                out var startTime) ||
            !TimeSpan.TryParseExact(
                SceneEndTimeTextBox.Text,
                @"mm\:ss",
                CultureInfo.InvariantCulture,
                out var endTime))
       {
            SceneDurationTextBlock.Text = "--:--";
            return;
       }

        if (endTime <= startTime)
       {
            SceneDurationTextBlock.Text = "--:--";
            return;
       }

        SceneDurationTextBlock.Text =
            (endTime - startTime).ToString(@"mm\:ss");
    }
              private async void CreateSceneButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        var name = NewSceneNameTextBox.Text.Trim();
        var description = NewSceneDescriptionTextBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(name))
       {
            MessageBox.Show(
                "Introduce un nombre para la escena.",
                "Nueva escena",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            NewSceneNameTextBox.Focus();
            return;
        }

        if (!TimeSpan.TryParseExact(
                NewSceneStartTimeTextBox.Text.Trim(),
                @"mm\:ss",
                CultureInfo.InvariantCulture,
                TimeSpanStyles.None,
                out var startTime))
        {
            MessageBox.Show(
                "El inicio debe tener el formato mm:ss.",
                "Nueva escena",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            NewSceneStartTimeTextBox.Focus();
            return;
        }

        if (!TimeSpan.TryParseExact(
                NewSceneEndTimeTextBox.Text.Trim(),
                @"mm\:ss",
                CultureInfo.InvariantCulture,
                TimeSpanStyles.None,
                out var endTime))
        {
            MessageBox.Show(
                "El final debe tener el formato mm:ss.",
                "Nueva escena",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            NewSceneEndTimeTextBox.Focus();
            return;
        }

        if (startTime < TimeSpan.Zero || endTime <= startTime)
        {
            MessageBox.Show(
                "El final debe ser posterior al inicio.",
                "Nueva escena",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return;
        }

        if (endTime > _projectDuration)
        {
            MessageBox.Show(
                "El final de la escena supera la duración del videoclip.",
                "Nueva escena",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return;
        }

        CreateSceneButton.IsEnabled = false;

        try
        {
            var command = new CreateVideoSceneCommand(
                _videoProjectId,
                name,
                startTime,
                endTime,
                description);

            var createdSceneId =
                await _dispatcher.SendCommandAsync<
                    CreateVideoSceneCommand,
                    Guid>(command);

            _selectedSceneId = createdSceneId;

            await ReloadScenesAsync();

            NewSceneNameTextBox.Clear();
            NewSceneDescriptionTextBox.Clear();

            MessageBox.Show(
                "La escena se ha creado correctamente.",
                "Nueva escena",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error al crear la escena",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
        finally
        {
            CreateSceneButton.IsEnabled = true;
        }
    }
    private async void SaveSceneTimingButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        if (_selectedSceneId is not Guid sceneId)
        {
            MessageBox.Show(
                "Selecciona una escena antes de guardar.",
                "Escena no seleccionada",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return;
        }

        if (!TimeSpan.TryParseExact(
                SceneStartTimeTextBox.Text,
                @"mm\:ss",
                CultureInfo.InvariantCulture,
                out var startTime) ||
            !TimeSpan.TryParseExact(
                SceneEndTimeTextBox.Text,
                @"mm\:ss",
                CultureInfo.InvariantCulture,
                out var endTime))
        {
            MessageBox.Show(
                "Introduce los tiempos con el formato mm:ss, por ejemplo 00:15.",
                "Formato de tiempo incorrecto",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return;
        }

        if (startTime < TimeSpan.Zero ||
            endTime <= startTime ||
            endTime > _projectDuration)
        {
            MessageBox.Show(
                "Comprueba los tiempos: el inicio debe ser anterior al final y el final no puede superar la duración del videoclip.",
                "Tiempos no válidos",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return;
        }

        try
        {
            SaveSceneTimingButton.IsEnabled = false;

            var command = new UpdateVideoSceneTimingCommand(
                sceneId,
                startTime,
                endTime);

            await _dispatcher.SendCommandAsync<
                UpdateVideoSceneTimingCommand,
                Guid>(command);

            await ReloadScenesAsync();

            DrawScenes(_scenes);

            MessageBox.Show(
                "Los tiempos de la escena se han guardado correctamente.",
                "Cambios guardados",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error al guardar los tiempos",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
        finally
        {
            SaveSceneTimingButton.IsEnabled =
                _selectedSceneId.HasValue;
        }
    }
    private void TimelineTrack_MouseLeftButtonDown(
        object sender,
        MouseButtonEventArgs e)
    {
        if (sender is not Border track)
            return;

        if (_projectDuration.TotalSeconds <= 0 ||
            track.ActualWidth <= 0)
        {
            return;
        }

        var position = e.GetPosition(track).X;

        position = Math.Clamp(
            position,
            0,
            track.ActualWidth);

        var percentage = position / track.ActualWidth;

        var selectedTime = TimeSpan.FromSeconds(
            percentage * _projectDuration.TotalSeconds);

        var playheadPosition = Math.Clamp(
            position - Playhead.Width / 2,
            0,
            Math.Max(0, track.ActualWidth - Playhead.Width));

        Playhead.Margin = new Thickness(
            playheadPosition,
            0,
            0,
            0);

        TimelineSelectedTimeTextBlock.Text =
            selectedTime.ToString(@"mm\:ss");

        e.Handled = true;
    }
}
