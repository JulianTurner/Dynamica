using Avalonia.ReactiveUI;

namespace Dynamica.Shell.Views;

public partial class ActivityBarView :  ReactiveUserControl<ViewModels.ActivityBarViewModel>
{
    public ActivityBarView()
    {
        InitializeComponent();
    }
}