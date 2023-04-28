using CefSharp;
using CefSharp.Wpf;
using MapleDesktop2._0;
using System.Windows;
using System.Windows.Controls;

namespace MapleDeaktop2._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public partial class App : Application
    {
            internal  ChromiumWebBrowser browser = new ChromiumWebBrowser();

        protected override void OnStartup(StartupEventArgs e)
        {

            var settings = new CefSettings();
            settings.CefCommandLineArgs["autoplay-policy"] = "no-user-gesture-required";
            Cef.Initialize(settings, true, browserProcessHandler: null);
           // browser.FrameLoadEnd += Browser_FrameLoadEnd;
            //browser.FrameLoadStart += Browser_FrameLoadStart;
            //WebWindow.SetBrowser(browser);
            //Grid.Children.Add(browser);
            base.OnStartup(e);

        }
        //internal void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        //{
        //    WebWindow.DebugWrite("Attempting to Skip Ad");
        //    Dispatcher.Invoke(() =>
        //    {
        //        string script = @"
        //            var video = document.getElementsByTagName('video')[0];
        //            video.oncanplay = function () {
        //                var ad = document.getElementsByClassName('ad-container')[0];
        //                ad.remove();
        //            }
        //        ";
        //        e.Frame.ExecuteJavaScriptAsync(script);
        //    });
        //}

        //internal void Browser_FrameLoadStart(object sender, FrameLoadStartEventArgs e)
        //{
        //    WebWindow.DebugWrite("Attempting to Skip Ad");
        //    Dispatcher.Invoke(() =>
        //    {
        //        string script = @"
        //            var video = document.getElementsByTagName('video')[0];
        //            video.oncanplay = function () {
        //                var ad = document.getElementsByClassName('ad-container')[0];
        //                ad.remove();
        //            }
        //        ";
        //        e.Frame.ExecuteJavaScriptAsync(script);
        //    });
        //}

    }


}
