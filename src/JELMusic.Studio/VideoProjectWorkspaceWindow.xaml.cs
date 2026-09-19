using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using JELMusic.Application.Abstractions.Dispatching;
using JELMusic.Application.Queries.GetVideoProjectById;

namespace JELMusic.Studio;

public partial class VideoProjectWorkspaceWindow : Window
{
    private TimeSpan _projectDuration;

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
                DurationTextBlock.Text =
                    result.Duration.ToString(@"mm\:ss");

                _projectDuration = result.Duration;

                BuildTimeline(_projectDuration);
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

    private void BuildTimeline(TimeSpan duration)
    {
        TimelineRuler.Children.Clear();

        TimelineDurationTextBlock.Text =
            duration.ToString(@"mm\:ss");

        TimelineSelectedTimeTextBlock.Text =
            "00:00";

        TimelineRuler.SizeChanged += (_, _) =>
        {
            DrawTimelineRuler(duration);
        };
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

        var interval = totalSeconds <= 60
            ? 10
            : 30;

        for (var seconds = 0.0;
             seconds <= totalSeconds;
             seconds += interval)
        {
            var position =
                seconds / totalSeconds * width;

            var marker = new Border
            {
                Width = 1,
                Height = 8,
                Background = new SolidColorBrush(
                    Color.FromRgb(100, 100, 100)),
                HorizontalAlignment =
                    HorizontalAlignment.Left
            };

            Canvas.SetLeft(marker, position);
            Canvas.SetTop(marker, 0);

            TimelineRuler.Children.Add(marker);

            var label = new TextBlock
            {
                Text = TimeSpan
                    .FromSeconds(seconds)
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

    var percentage =
        position / track.ActualWidth;

    var selectedTime =
        TimeSpan.FromSeconds(
            percentage *
            _projectDuration.TotalSeconds);

    var playheadPosition = Math.Clamp(
        position - Playhead.Width / 2,
        0,
        Math.Max(
            0,
            track.ActualWidth - Playhead.Width));

    Playhead.Margin = new Thickness(
        playheadPosition,
        0,
        0,
        0);

    TimelineSelectedTimeTextBlock.Text =
        selectedTime.ToString(@"mm\:ss");

    e.Handled = true;
}

        var position = e.GetPosition(track).X;

        position = Math.Clamp(
            position,
            0,
            track.ActualWidth);

        var percentage =
            position / track.ActualWidth;

        var selectedTime =
            TimeSpan.FromSeconds(
                percentage *
                _projectDuration.TotalSeconds);


        var playheadPosition = Math.Clamp(
            position - Playhead.Width / 2,
            0,
            Math.Max(
            0,
            track.ActualWidth - Playhead.Width));

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
