using HijackGen.Services;
using HijackGen.ViewModels;
using HijackGen.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace HijackGen;

public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public App()
    {
        ServiceProvider = ConfigureService();
    }

    private static ServiceProvider ConfigureService()
    {
        ServiceCollection services = new();
        // Views
        services.AddSingleton<MainView>();
        services.AddTransient<GenerationView>();
        services.AddTransient<AboutView>();
        // ViewModels
        services.AddSingleton<MainViewModel>();
        services.AddTransient<GenerationViewModel>();
        services.AddTransient<AboutViewModel>();
        // Services
        services.AddSingleton<IShowDialogService, ShowDialogService>();
        services.AddSingleton<ISettingsService, SettingsService>();
        return services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        MainView main = ServiceProvider.GetRequiredService<MainView>();
        main.DataContext = ServiceProvider.GetRequiredService<MainViewModel>();
        main.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        ISettingsService settingsService = ServiceProvider.GetRequiredService<ISettingsService>();
        settingsService.SaveSettings();
        base.OnExit(e);
    }
}
