using CefSharp;
using CefSharp.Handler;
using CefSharp.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using static MapleDesktop2._0.MusicSystem;

namespace MapleDesktop2._0
{
    /// <summary>
    /// Interaction logic for WebWindow.xaml
    /// </summary>
    public partial class WebWindow : Window
    {

        internal static ChromiumWebBrowser temp = new ChromiumWebBrowser();
        public WebWindow()
        {
            InitializeComponent();
            webBrowser.BrowserSettings = temp.BrowserSettings;

        }
        public static  void SetBrowser(ChromiumWebBrowser browser)
        {
           
            temp = browser;
           

        }
        public static void DebugWrite(string value)
        {

            MainWindow.currentMusicForm.WriteToDebugConsole(value);


        }
        //internal static string url1 = "https://www.google.com";

        //internal static CefSharp.Wpf.ChromiumWebBrowser webBrowser = new CefSharp.Wpf.ChromiumWebBrowser();


        internal void PlayVideo(string url)
        {
            MainWindow.music.playingVideo = true;
            MainWindow.webWindowOpen = true;
            MainWindow.currentMusicForm.WriteToDebugConsole("Playing video: " + url);

            string builtURL = ($"{url}autoplay=1");
            webBrowser.Address = url;


        }



        internal void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            MainWindow.currentMusicForm.WriteToDebugConsole("attempting to remove ad");
            try
            {
                Dispatcher.Invoke(() =>
                {
                    string script = @"
                    var video = document.getElementsByTagName('video')[0];
                    video.oncanplay = function () {
                        var ad = document.getElementsByClassName('ad-container')[0];
                        ad.remove();
                    }
                ";
                    e.Frame.ExecuteJavaScriptAsync(script);
                });
            }
            catch (Exception ex)
            {

                MainWindow.currentMusicForm.WriteToDebugConsole(ex.Message);
            }
            
        }




        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            MainWindow.webWindowOpen = false;
            MainWindow.music.playingVideo = false;
            MainWindow.currentMusicForm.ResetWebWindow();
            MainWindow.currentMusicForm.WriteToDebugConsole("MainWindow.webWindowOpen = false; ");



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (webBrowser.CanGoBack)
            {
                webBrowser.Back();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (webBrowser.CanGoForward)
            {
                webBrowser.Forward();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            webBrowser.Address = txb_URLBox.Text;
        }

        private void btn_PlayNextTrack_Click(object sender, RoutedEventArgs e)
        {

            int currentTrackId = MainWindow.music.currentTrack.playlistId;
            MainWindow.currentMusicForm.WriteToDebugConsole("currentTrackId " + currentTrackId);
            int nextTrackId = currentTrackId + 1;
            MainWindow.currentMusicForm.WriteToDebugConsole("nextTrackId " + nextTrackId);
            MainWindow.currentMusicForm.PostPlaylistCount();
            Song song = MainWindow.music.SearchForPlaylistTrack(nextTrackId - 1);
            MainWindow.music.currentTrack = song;
            MainWindow.music.currentTrackPlaylistId = song.playlistId;
            MainWindow.music.playingVideo = true;
            webBrowser.Address = song.url;

        }

        private void btn_PlaylastTrack_Click(object sender, RoutedEventArgs e)
        {

            int currentTrackId = MainWindow.music.currentTrack.playlistId;
            MainWindow.currentMusicForm.WriteToDebugConsole("currentTrackId " + currentTrackId);
            int nextTrackId = currentTrackId - 1;
            MainWindow.currentMusicForm.WriteToDebugConsole("nextTrackId " + nextTrackId);
            MainWindow.currentMusicForm.PostPlaylistCount();
            Song song = MainWindow.music.SearchForPlaylistTrack(nextTrackId - 2);
            MainWindow.music.currentTrack = song;
            MainWindow.music.currentTrackPlaylistId = song.playlistId;
            MainWindow.music.playingVideo = true;
            webBrowser.Address = song.url;
        }
    }


    

}

