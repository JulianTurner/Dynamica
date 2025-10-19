using Avalonia.ReactiveUI;
using Dynamica.Shell.ViewModels;

namespace Dynamica.Shell.Views;

public partial class MainWindow :  ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }
}