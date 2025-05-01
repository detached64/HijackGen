using System.Globalization;
using System.Threading;
using System.Windows;

namespace HijackGen.GUI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
#if false
            var culture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            base.OnStartup(e);
#endif
        }
    }
}
