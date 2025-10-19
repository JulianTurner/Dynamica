using Microsoft.Extensions.Logging;
using ReactiveUI;

namespace Dynamica.Shell.ViewModels;

public sealed class StatusBarViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly ILogger<StatusBarViewModel> _logger;

    public ViewModelActivator Activator { get; } = new();

    private string _message = string.Empty;
    public string Message
    {
        get => _message;
        set => this.RaiseAndSetIfChanged(ref _message, value);
    }

    public StatusBarViewModel(ILogger<StatusBarViewModel> logger)
    {
        _logger = logger;
    }
}
