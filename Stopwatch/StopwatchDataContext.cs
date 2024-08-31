using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Stopwatch;

/// <summary>
/// Represents the data context for the stopwatch, providing commands to start, stop, and reset the timer.
/// </summary>
public partial class StopwatchDataContext : ObservableObject
{
    readonly IStopwatchTimer _timer;

    public bool IsRunning => _timer.IsRunning;

    public event EventHandler Started = delegate { };

    public event EventHandler Stopped = delegate { };

    /// <summary>
    /// Initializes a new instance of the <see cref="StopwatchDataContext"/> class.
    /// </summary>
    /// <param name="timer">The timer implementation to be used by the stopwatch.</param>
    public StopwatchDataContext(IStopwatchTimer timer)
    {
        _timer = timer;
        _timer.Started += OnStarted;
        _timer.Stopped += OnStopped;
    }

    /// <summary>
    /// Handles the Started event of the stopwatch timer.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    void OnStarted(object? sender, EventArgs e)
    {
        Started.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Handles the Stopped event of the stopwatch timer.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    void OnStopped(object? sender, EventArgs e)
    {
        Stopped.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Gets or sets the current time of the stopwatch.
    /// </summary>
    [ObservableProperty]
    string _time;

    /// <summary>
    /// Updates the current time of the stopwatch.
    /// </summary>
    /// <returns>A task that represents the asynchronous update operation.</returns>
    public Task Update()
    {
        Time = _timer.Time.ToString("hh\\:mm\\:ss");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Starts the stopwatch timer.
    /// </summary>
    [RelayCommand]
    async void Start()
    {
        await _timer.Start();
    }

    /// <summary>
    /// Stops the stopwatch timer.
    /// </summary>
    [RelayCommand]
    async void Stop()
    {
        await _timer.Stop();
    }

    /// <summary>
    /// Resets the stopwatch timer.
    /// </summary>
    [RelayCommand]
    async void Restart()
    {
        await _timer.Restart();
    }
}
