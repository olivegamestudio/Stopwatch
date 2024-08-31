using System.Runtime.InteropServices.JavaScript;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Stopwatch;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    readonly StopwatchDataContext _context;
    readonly IHostBuilder _builder;
    readonly IHost _host;
    readonly DispatcherTimer _timer = new();

    public MainWindow()
    {
        InitializeComponent();
        
        _builder = Host.CreateDefaultBuilder();

        _builder.ConfigureLogging((context, logging) => { });

        _builder.ConfigureServices((builder, services) =>
        {
            services
                .AddSingleton<StopwatchDataContext>()
                .AddSingleton<IStopwatchTimer, StopwatchTimer>();
        });

        _host = _builder.Build();

        _timer.Interval = TimeSpan.FromMilliseconds(500);
        _timer.Tick += OnTick;

        DataContext = _context = _host.Services.GetRequiredService<StopwatchDataContext>();
    }

    void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        _context.Started += OnStarted;
        _context.Stopped += OnStopped;
    }

    void OnStopped(object? sender, EventArgs e)
    {
        _timer.Stop();
    }

    void OnStarted(object? sender, EventArgs e)
    {
        _timer.Start();
    }

    async void OnTick(object? sender, EventArgs e)
    {
        await _context.Update();
    }
}
