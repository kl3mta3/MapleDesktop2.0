using CefSharp;
using CefSharp.Handler;
using CefSharp.Wpf;
using MapleDesktop2._0.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
