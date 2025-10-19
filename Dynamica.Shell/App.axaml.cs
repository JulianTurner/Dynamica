using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using Splat;

namespace Dynamica.Shell;

public partial class App : Application
{
    public static IServiceProvider? Services { get; private set; }

    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
            return;

        // 1) MS.DI Container aufbauen
        var sc = new ServiceCollection();

        // Konfiguration & Logging
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        sc.AddSingleton<IConfiguration>(config);
        sc.AddLogging(b =>
        {
            b.AddConfiguration(config.GetSection("Logging"));
            b.AddConsole();
            b.AddDebug();
        });

        // ReactiveUI-Bus
        sc.AddSingleton<IMessageBus>(MessageBus.Current);

        // 2) ViewModels
        sc.AddTransient<ViewModels.MainWindowViewModel>();
        sc.AddTransient<ViewModels.StatusBarViewModel>();
        sc.AddTransient<ViewModels.ActivityBarViewModel>();

        // 3) Views **als IViewFor<T>** registrieren (so werden sie via DI erstellt)
        sc.AddTransient<IViewFor<ViewModels.StatusBarViewModel>,
                        Views.StatusBarView>();
        sc.AddTransient<IViewFor<ViewModels.ActivityBarViewModel>,
                        Views.ActivityBarView>();

        // 4) Splat-Resolver auf MS.DI umbiegen
        var resolver = Locator.CurrentMutable;
        resolver.InitializeSplat();                         // Basisdienste
        resolver.InitializeReactiveUI();                    // ReactiveUI-Services

        var sp = sc.BuildServiceProvider();
        Services = sp;
        
        // 5) Register ViewLocator to use DI container for view resolution
        Locator.CurrentMutable.RegisterLazySingleton<IViewFor<ViewModels.StatusBarViewModel>>(
            () => sp.GetRequiredService<IViewFor<ViewModels.StatusBarViewModel>>());
        Locator.CurrentMutable.RegisterLazySingleton<IViewFor<ViewModels.ActivityBarViewModel>>(
            () => sp.GetRequiredService<IViewFor<ViewModels.ActivityBarViewModel>>());

        // 6) MainWindow + ViewModel
        var mainVm = sp.GetRequiredService<ViewModels.MainWindowViewModel>();

        desktop.MainWindow = new Views.MainWindow
        {
            ViewModel = mainVm // bei ReactiveWindow<T> bevorzugt
        };

        base.OnFrameworkInitializationCompleted();
    }
}
