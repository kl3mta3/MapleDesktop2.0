using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MapleDesktop2._0
{
    /// <summary>
    /// Interaction logic for PlaylistConsoleForm.xaml
    /// </summary>
    public partial class PlaylistConsoleForm : Window
    {
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


    }
}
