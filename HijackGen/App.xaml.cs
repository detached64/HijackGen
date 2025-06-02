using HijackGen.Services;
using HijackGen.ViewModels;
using HijackGen.Views;
using System.Windows;

namespace HijackGen
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
#if false
            CultureInfo culture = new("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
#endif
            base.OnStartup(e);
            DialogService dialogService = new();
            MainViewModel mainVM = new(dialogService);
            MainView main = new()
            {
                DataContext = mainVM
            };
            main.Show();
        }
    }
}
