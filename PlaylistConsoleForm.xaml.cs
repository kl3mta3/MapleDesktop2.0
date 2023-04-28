using System;
using System.Windows;

namespace MapleDesktop2._0
{
    /// <summary>
    /// Interaction logic for PlaylistConsoleForm.xaml
    /// </summary>
    public partial class PlaylistConsoleForm : Window
    {

        internal static int playlistTrack = 0;
        public PlaylistConsoleForm()
        {
            InitializeComponent();
        }


        private void btn_ClearPlaylist_Click(object sender, EventArgs e)
        {
            ClearPlaylistConsole();
            MainWindow.music.ClearAudioPlaylist();
        }


        public void WriteToPlaylistConsole(string message)
        {


            rtb_PlaylistConsole.AppendText(Environment.NewLine + message);


        }
        public void ClearPlaylistConsole()
        {

            rtb_PlaylistConsole.Document.Blocks.Clear();


        }

        private void btn_GoToTrack_Click(object sender, RoutedEventArgs e)
        {


            if (txb_playlistTrackSelect.Text != "")
            {
                MainWindow.music.selectedPlaylistTrack = Int32.Parse(txb_playlistTrackSelect.Text);
                MainWindow.currentMusicForm.WriteToDebugConsole("txb_playlist.text" + txb_playlistTrackSelect.Text);
                MainWindow.currentMusicForm.WriteToDebugConsole("selectedPlaylistTrack" + MainWindow.music.selectedPlaylistTrack.ToString());
                MainWindow.music.userpressedGo = true;
                MainWindow.wavePlayer.Stop();
                // MainWindow.music.PlayPlaylistTrack(sender, e);
            }

        }
    }
}
