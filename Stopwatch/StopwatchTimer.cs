using System.Windows.Threading;

namespace Stopwatch;

/// <summary>
/// Represents a stopwatch timer with start, stop, and restart functionalities.
/// </summary>
public class StopwatchTimer : IStopwatchTimer
{
    readonly System.Diagnostics.Stopwatch _stopwatch = new();

    /// <summary>
    /// Gets the elapsed time of the stopwatch timer.
    /// </summary>
    public TimeSpan Time => _stopwatch.Elapsed;

    /// <summary>
    /// Gets a value indicating whether the stopwatch timer is currently running.
    /// </summary>
    public bool IsRunning { get; set; }

    /// <summary>
    /// Occurs when the stopwatch timer has started.
    /// </summary>
    public event EventHandler Started = delegate { };

    /// <summary>
    /// Occurs when the stopwatch timer has stopped.
    /// </summary>
    public event EventHandler Stopped = delegate { };

    /// <summary>
    /// Starts the stopwatch timer.
    /// </summary>
    /// <returns>A task that represents the asynchronous start operation.</returns>
    public Task Start()
    {
        _stopwatch.Start();
        Started.Invoke(this, EventArgs.Empty);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Restarts the stopwatch timer.
    /// </summary>
    /// <returns>A task that represents the asynchronous restart operation.</returns>
    public Task Restart()
    {
        _stopwatch.Restart();
        Started.Invoke(this, EventArgs.Empty);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Stops the stopwatch timer.
    /// </summary>
    /// <returns>A task that represents the asynchronous stop operation.</returns>
    public Task Stop()
    {
        _stopwatch.Stop();
        Stopped.Invoke(this, EventArgs.Empty);
        return Task.CompletedTask;
    }
}
