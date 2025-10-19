using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ReactiveUI;

namespace Dynamica.Shell.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<MainWindowViewModel> _logger;
    private readonly IMessageBus _bus;
    public ActivityBarViewModel ActivityBarVM { get; }
    public StatusBarViewModel StatusBarVM { get; }

    public MainWindowViewModel(
        IConfiguration configuration,
        ILogger<MainWindowViewModel> logger,
        IMessageBus bus,
        ActivityBarViewModel activityBarVM,
        StatusBarViewModel statusBarVM
    )
    {
        ActivityBarVM = activityBarVM;
        StatusBarVM = statusBarVM;
        _bus = bus;
        _configuration = configuration;
        _logger = logger;
        Greeting = _configuration.GetValue<string>("Settings:Test") ?? "Yo";

        

    }

    public string Greeting { get; } = "Welcome to Avalonia!";
}