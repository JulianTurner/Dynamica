using Avalonia.ReactiveUI;

namespace Dynamica.Shell.Views;

public partial class StatusBarView
    : ReactiveUserControl<ViewModels.StatusBarViewModel>
{
    public StatusBarView()
    {
        InitializeComponent();

    }

}