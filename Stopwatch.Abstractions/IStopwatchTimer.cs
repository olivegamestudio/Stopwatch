namespace Stopwatch;

/// <summary>
/// Defines the interface for a stopwatch timer with start, stop, and restart functionalities.
/// </summary>
public interface IStopwatchTimer
{
    /// <summary>
    /// Gets the elapsed time of the stopwatch timer.
    /// </summary>
    TimeSpan Time { get; }

    /// <summary>
    /// Gets a value indicating whether the stopwatch timer is currently running.
    /// </summary>
    bool IsRunning { get; }

    /// <summary>
    /// Occurs when the stopwatch timer has started.
    /// </summary>
    event EventHandler Started;

    /// <summary>
    /// Occurs when the stopwatch timer has stopped.
    /// </summary>
    event EventHandler Stopped;

    /// <summary>
    /// Starts the stopwatch timer.
    /// </summary>
    /// <returns>A task that represents the asynchronous start operation.</returns>
    Task Start();

    /// <summary>
    /// Restarts the stopwatch timer.
    /// </summary>
    /// <returns>A task that represents the asynchronous restart operation.</returns>
    Task Restart();

    /// <summary>
    /// Stops the stopwatch timer.
    /// </summary>
    /// <returns>A task that represents the asynchronous stop operation.</returns>
    Task Stop();
}
