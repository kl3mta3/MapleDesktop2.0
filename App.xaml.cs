using CefSharp;
using CefSharp.Wpf;
using System.Windows;

namespace MapleDeaktop2._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {

            var settings = new CefSettings();
            settings.CefCommandLineArgs["autoplay-policy"] = "no-user-gesture-required";
            Cef.Initialize(settings, true, browserProcessHandler: null);

            base.OnStartup(e);

        }

    }


}
