using Google.Apis.YouTube.v3.Data;
using NAudio.Wave;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Shapes;
using YoutubeExplode;

namespace MapleDesktop2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        internal static string openAiToken = Properties.Settings.Default.OpenAiToken;
        // internal static string youtubeApiKey = Properties.Settings.Default.YouTubeApiKey;

        internal static bool inInput = false;
        internal static bool saveMusic = false;
        internal static bool saveVideo = false;
        internal static bool playAudio = true;
        internal static bool playVideo = false;

        internal static string systemPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        internal static string musicSavePath = Path.Combine(systemPath, @"Maple\Saved Music");
        internal static string videoSavePath = Path.Combine(systemPath, @"Maple\Saved Videos");


        internal static IWavePlayer wavePlayer;
        internal static WaveOutEvent outputDevice;
        internal static AudioFileReader audioFile;
        internal static VideoPlayer videoPlayer = new VideoPlayer();
        internal static YoutubeClient youtube = new YoutubeClient();

        internal static MusicSystem music = new MusicSystem();
        internal static MusicForm musicForm = new MusicForm();
        internal static AiForm aiForm = new AiForm();
        internal static WebWindow webWindow = new WebWindow();
        internal static LinksForm LinksForm = new LinksForm();
        internal static DebugConsoleForm debugConsole = new DebugConsoleForm();
        internal static bool aiFormOpen = false;
        internal static bool musicFormOpen = false;
        internal static bool linksFormOpen = false;
        internal static bool playlistConsoleOpen = false;
        internal static bool webWindowOpen = false;
        internal static bool currentSetLinksFormOpen = false;
        internal static bool currentSetAppsFormOpen = false;
        internal static MusicForm currentMusicForm = new MusicForm();
        internal static AiForm currentAiForm = new AiForm();
        internal static LinksForm currentLinksForm = new LinksForm();
        internal static bool debugConsoleOpen = false;

        public MainWindow()
        {
            InitializeComponent();

            if (!Directory.Exists(musicSavePath))
            {
                Directory.CreateDirectory(musicSavePath);

            }
            if (!Directory.Exists(videoSavePath))
            {
                Directory.CreateDirectory(videoSavePath);

            }

           

        }
        internal static void WriteToDebugConsole(string value)
        {
            currentMusicForm.WriteToDebugConsole(value);
        }

        private void btn_ChatForm_Click(object sender, RoutedEventArgs e)
        {
            aiFormOpen = !aiFormOpen;

            if (aiFormOpen)
            {
                AiForm current = new AiForm();
                currentAiForm = current;
                currentAiForm.Show();
            }
            else
            {

                currentAiForm.Hide();
            }
        }


        private void btn_MusicForm_Click(object sender, RoutedEventArgs e)
        {
            musicFormOpen = !musicFormOpen;

            if (musicFormOpen)
            {
                MusicForm current = new MusicForm();
                currentMusicForm = current;
                currentMusicForm.Show();
            }
            else
            {

                currentMusicForm.Hide();
            }
        }



        private void btn_LinkToDiscord_Click(object sender, RoutedEventArgs e)
        {


            string link = " https://discord.com/invite/PPaRjqnG";
            try
            {
                Process proc = new Process();
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.FileName = link;
                proc.Start();
            }
            catch (Exception)
            {

            }
        }





        private void btn_LinksForm_Click(object sender, RoutedEventArgs e)
        {
            linksFormOpen = !linksFormOpen;

            if (linksFormOpen)
            {
                LinksForm current = new LinksForm();
                currentLinksForm = current;
                currentLinksForm.ConfigureSystem();
                currentLinksForm.Show();
            }
            else
            {

                currentLinksForm.Hide();
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
