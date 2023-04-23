using System;
using System.Windows;
using System.Windows.Input;
using NAudio.Wave;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Threading;
using System.Drawing;
using static MapleDesktop2._0.MusicSystem;
using System.Windows.Controls;
using System.ComponentModel;

namespace MapleDesktop2._0
{
    /// <summary>
    /// Interaction logic for MusicForm.xaml
    /// </summary>
    public partial class MusicForm : Window
    {
        internal static DispatcherTimer timer1 = new DispatcherTimer();
        internal static DebugConsoleForm currentDebugConsole = new DebugConsoleForm();
        public MusicForm()
        {
            InitializeComponent();

            
        }


        internal void BeginPlayback(string filename)
        {


            WriteToDebugConsole($"Audio BeginPlayback Triggered at {filename}");

            MainWindow.wavePlayer = null;
            SetPlayStatus("-Now Playing-");
            //Debug.Assert(MainWindow.wavePlayer == null);
            MainWindow.wavePlayer = CreateWavePlayer();
            WriteToDebugConsole("Wave Player Created");
            try
            {
                MainWindow.audioFile = new AudioFileReader(filename);
                WriteToDebugConsole("File Read");

            }
            catch
            {
                WriteToDebugConsole("Error Finding File Redownloading");
                Song song = MainWindow.music.SongFromUrl(MainWindow.music.currentTrack.url);
                MainWindow.audioFile = new AudioFileReader(song.path);
                WriteToDebugConsole("File Read");

            }
            WriteToDebugConsole($"Audio file  {MainWindow.audioFile.FileName}");
            SetProgressBarMaxValue((int)MainWindow.audioFile.TotalTime.TotalSeconds);
            WriteToDebugConsole($"Audio file Length  {MainWindow.audioFile.Length}");
            WriteToDebugConsole($"Audio file Length as int {(int)MainWindow.audioFile.Length}");
            WriteToDebugConsole($"Audio file TotalTime {MainWindow.audioFile.TotalTime}");
            WriteToDebugConsole($"Audio file TotalSeconds {MainWindow.audioFile.TotalTime.TotalSeconds}");
            WriteToDebugConsole($"Audio file TotalSeconds as int {(int)MainWindow.audioFile.TotalTime.TotalSeconds}");

            MainWindow.audioFile.Volume = (float)volumeSlider3.Value / 100;



            SetProgressBarCurrentValue((int)MainWindow.audioFile.CurrentTime.TotalSeconds);
            WriteToDebugConsole("SetProgressBarCurrentValue TO " + MainWindow.audioFile.CurrentTime.TotalSeconds.ToString());
            WriteToDebugConsole("SetProgressBarMaxValue TO " + MainWindow.audioFile.TotalTime.TotalSeconds.ToString());
            MainWindow.wavePlayer.Init(MainWindow.audioFile);

            WriteToDebugConsole($"Volume lvl {MainWindow.audioFile.Volume}");
            MainWindow.wavePlayer.PlaybackStopped += OnPlaybackStopped;
            MainWindow.music.playingTrack = true;
            lbl_ProgressBarTrackLength.Content = FormatTimeSpan(MainWindow.audioFile.TotalTime);
            WriteToDebugConsole("Starting Play");
            SetPlayStatus("Now Playing:");


            MainWindow.wavePlayer.Play();
            WriteToDebugConsole("Audio Playback Started");
            //EnableButtons(true);

            timer1.Tick += OnTimerTick;
            timer1.Interval = new TimeSpan(0, 0, 1);
            timer1.Start();
             // timer for updating current time label

            WriteToDebugConsole("Tick Timer on");
        }





        void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {

            //Debug.Assert(!InvokeRequired, "PlaybackStopped on wrong thread");
            //Helpers helper = new Helpers();
            WriteToDebugConsole("Tick Timer stopped");
            MainWindow.music.trackPaused = false;

            WriteToDebugConsole("Playback Stopped");
            MainWindow.music.playingTrack = false;
            //helper.DisplayCurrentSongInfo("-", "-", "-", "-");
            timer1.Stop();
            if (!MainWindow.music.userPressedStop)
            {

                ClearSongInfoDisplay();
                SetPlayStatus("");
            }
            else
            {

                SetPlayStatus("-Stopped-");
            }

            CleanUp();
            //EnableButtons(false);
            lbl_CurrentTrackPosition.Content = "00:00";

            MainWindow.music.currentTrack = null;
            MainWindow.music.userPressedStop = false;




            if (MainWindow.music.playlistCount > 1 && !MainWindow.music.userPressedStop && !MainWindow.music.userPressedLast)
            {
                currentDebugConsole.WriteToDebugConsole($"Next Track Triggered After Stop");
                MainWindow.music.PlayNextTrack(sender, e);


                MainWindow.music.userPressedLast = false;
                MainWindow.music.userPressedNext = false;
                return;
            }
            if (MainWindow.music.userPressedLast)
            {

                currentDebugConsole.WriteToDebugConsole($"Last Track Triggered ");
                MainWindow.music.PlayLastTrack(sender, e);


                MainWindow.music.userPressedLast = false;
                MainWindow.music.userPressedNext = false;
                return;
            }
            if (MainWindow.music.userPressedLast)
            {

                currentDebugConsole.WriteToDebugConsole($"Last Track Triggered ");
                MainWindow.music.PlayNextTrack(sender, e);

                MainWindow.music.userPressedLast = false;
                MainWindow.music.userPressedNext = false;
                return;
            }
            currentDebugConsole.WriteToDebugConsole($"Reached last of OnStop ");
            MainWindow.music.userPressedLast = false;
            MainWindow.music.userPressedNext = false;
            //MainWindow.music.userPressedStop = false;
            //MainWindow.music.userpressedSeek = false;
        }


        internal void ClearSongInfoDisplay()
        {

            lbl_PlayingName.Content = "";


            lbl_PlayingArtist.Content = "";

           //lbl_PlayingLink.Content = "";
            txb_PlayingLink.Text = "";
            lbl_PlayingStatus.Content = "";
            lbl_PlayTrackPlaylistID.Content = "";

        }

        internal IWavePlayer CreateWavePlayer()
        {
            switch (comboBoxOutputDriver.SelectedIndex)
            {
                case 2:
                    return new WaveOutEvent();
                case 1:
                    return new WaveOut(WaveCallbackInfo.FunctionCallback());
                default:
                    return new WaveOut();
            }
        }

        internal void SetPlayStatus(string message)
        {
            lbl_PlayingStatus.Content = message;
        }
        internal void WriteToDebugConsole(string message)
        {
            //rtb_DebugMusic.Text = message + Environment.NewLine + rtb_DebugMusic.Text;


            currentDebugConsole.WriteToDebugConsole(message);


        }

        private void btn_MusicDebugConsoleToggle_Click(object sender, RoutedEventArgs e)
        {
            toggleDebugConsole();
        }

        public void toggleDebugConsole()
        {
            MainWindow.debugConsoleOpen = !MainWindow.debugConsoleOpen;
            if (MainWindow.debugConsoleOpen)
            {
                DebugConsoleForm current = new DebugConsoleForm();
                currentDebugConsole = current;
                currentDebugConsole.Show();

            }
            else
            {
                currentDebugConsole.Hide();

            }

        }


        private void ckb_SaveVideo_CheckedChanged(object sender, EventArgs e)
        {

            if (ckb_SaveVideo.IsChecked==false)
            {
                MainWindow.saveVideo = false;
                //WriteToMapleConsole("Saving Video Disabled");
            }
            else
            {
                MainWindow.saveVideo = true;
                //WriteToMapleConsole("Saving Video Enabled");
            }
        }

        private void CleanUp()
        {
            if (MainWindow.audioFile != null)
            {
                MainWindow.audioFile.Dispose();
                MainWindow.audioFile = null;
            }
            if (MainWindow.wavePlayer != null)
            {
                MainWindow.wavePlayer.Dispose();
                MainWindow.wavePlayer = null;
            }
        }

        public void DisplayCurrentSongInfo(string _title, string _artist, string _url, string _id)
        {
            lbl_PlayingName.Content = _title;

            lbl_PlayingArtist.Content = _artist;

           // lbl_PlayingLink.Content = _url;
            txb_PlayingLink.Text = _url;
            lbl_PlayTrackPlaylistID.Content = _id;
        }

        internal void SetProgressBarCurrentValue(double _value)
        {
            if (_value <= tbr_TrackProgressBar.Maximum)
            {
                double roundedValue = Math.Round(_value);
                // WriteToMapleConsole("TrackProgressBar.Value" + roundedValue.ToString());
                tbr_TrackProgressBar.Value = (int)roundedValue;
            }


        }

        internal void SetProgressBarMaxValue(int _value)
        {
            // WriteToMapleConsole("TrackProgressBar.Maximum" + _value.ToString());
            tbr_TrackProgressBar.Maximum = _value;

        }

        private static string FormatTimeSpan(TimeSpan ts)
        {
            return string.Format("{0:D2}:{1:D2}", (int)ts.TotalMinutes, ts.Seconds);
        }

        public void SetProgressBarCurrentProgress(string _progress)
        {
            if (_progress != "0:00")
            {

                lbl_CurrentTrackPosition.Content = _progress;
            }
            else
            {

                lbl_CurrentTrackPosition.Content = "0:00/0:00";
            }
        }

        public void SetProgressBarCurrentTrackLength(string _length)
        {
            if (_length != "0:00")
            {
                lbl_ProgressBarTrackLength.Content = _length;
            }
            else
            {
                lbl_ProgressBarTrackLength.Content = "0:00";

            }

        }

        void OnTimerTick(object sender, EventArgs e)
        {

            string total = FormatTimeSpan(MainWindow.audioFile.TotalTime);
            string current = FormatTimeSpan(MainWindow.audioFile.CurrentTime);
            string combined = $"{current}/{total}";

            //WriteToMapleConsole($"{test}");
            MainWindow.music.currentTrackCurrentTime = (int)MainWindow.audioFile.CurrentTime.TotalSeconds;


            SetProgressBarCurrentProgress(combined);
            if (!MainWindow.music.userpressedSeek && MainWindow.music.playingTrack)
            {

                if (MainWindow.audioFile != null)
                {
                    try
                    {

                        SetProgressBarCurrentValue((int)MainWindow.audioFile.CurrentTime.TotalSeconds);
                        MainWindow.music.progressBarCurrentTime = tbr_TrackProgressBar.Value;


                    }
                    catch (Exception ex)
                    {

                        currentDebugConsole.WriteToDebugConsole($"Error updating Progressbar value  Reason:{ex}");

                    }


                }


            }

        }

        void PlaybackPanel_Disposed(object sender, EventArgs e)
        {
            CleanUp();
        }

        private void PopulateOutputDriverCombo()
        {
            comboBoxOutputDriver.Items.Add("WaveOut Window Callbacks");
            comboBoxOutputDriver.Items.Add("WaveOut Function Callbacks");
            comboBoxOutputDriver.Items.Add("WaveOut Event Callbacks");
            comboBoxOutputDriver.SelectedIndex = 0;
        }

        private void StopAll()
        {

            if (MainWindow.music.playingTrack)
            {
                MainWindow.music.userPressedStop = true;
                MainWindow.wavePlayer.Stop();
                MainWindow.music.ClearAudioPlaylist();
            }

        }

        private void ckb_SaveVideo_Checked(object sender, RoutedEventArgs e)
        {
            

            
           
                MainWindow.saveVideo = true;
                currentDebugConsole.WriteToDebugConsole("Saving Video Enabled");
         


        }

        private void txb_SongInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (txb_SongInput.Text != null)
                {
                    string searchrequest = txb_SongInput.Text;



                   MainWindow.music.PlaySearchRequest(searchrequest, sender, e);

                    txb_SongInput.Text = "";
                }

            }
        }

        private void rbn_PlayAudio_Unchecked(object sender, RoutedEventArgs e)
        {

               MainWindow.playAudio = false;
                MainWindow.playVideo = true;
                //WriteToMapleConsole("Playing Video Files");
            
        }

        private void btn_Play_Click(object sender, RoutedEventArgs e)
        {
            if (txb_SongInput.Text != null)
            {
                string searchrequest = txb_SongInput.Text;

                 WriteToDebugConsole($" starting search with query {searchrequest}");

                MainWindow.music.HandleRequest(searchrequest, sender, e);
                txb_SongInput.Text = "";
            }
        }

        private void ckb_SaveMusic_Checked(object sender, RoutedEventArgs e)
        {

            MainWindow.saveMusic = true;
                currentDebugConsole.WriteToDebugConsole("Saving Music true");
            
           
        }

        private void ckb_SaveMusic_Unchecked(object sender, RoutedEventArgs e)
        {
           
                MainWindow.saveMusic = false;
                currentDebugConsole.WriteToDebugConsole("Saving Music false");
            
        }

        private void ckb_SaveVideo_Unchecked(object sender, RoutedEventArgs e)
        {

            MainWindow.saveVideo = false;
            currentDebugConsole.WriteToDebugConsole("Saving Video Disabled");
        }

        private void tbr_TrackProgressBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MainWindow.music.userpressedSeek = true;
            }
            catch (Exception ex)
            {
               currentDebugConsole.WriteToDebugConsole(ex.Message);
            }
        }

        private void tbr_TrackProgressBar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MainWindow.music.progressBarCurrentTime = (int)tbr_TrackProgressBar.Value;
                MainWindow.music.userpressedSeek = false;
                MainWindow.audioFile.CurrentTime = TimeSpan.FromSeconds(MainWindow.music.progressBarCurrentTime);

            }
            catch (Exception ex)
            {
                currentDebugConsole.WriteToDebugConsole(ex.Message);
            }
        }

        private void OnButtonPauseClick(object sender, RoutedEventArgs e)
        {
            PausePlayer();
        }

        private void PausePlayer()
        {

            if (!MainWindow.music.trackPaused && MainWindow.music.playingTrack)
            {

                MainWindow.music.trackPaused = true;

                SetPlayStatus("-Paused-");
                MainWindow.wavePlayer.Pause();


            }

        }

        private void OnButtonResumeClick(object sender, RoutedEventArgs e)
        {
            if (MainWindow.music.trackPaused && MainWindow.music.playingTrack)
            {

                MainWindow.music.trackPaused = false;
                MainWindow.music.userPressedStop = false;

                //lbl_PlayingStatus.Text = "-Paused-";
                SetPlayStatus("-Now Playing-");
                MainWindow.wavePlayer.Play();
                timer1.Start();

            }
            else if (MainWindow.music.trackPaused && MainWindow.music.userPressedStop)
            {
                MainWindow.music.userPressedStop = false;
                MainWindow.music.trackPaused = false;
                MainWindow.music.PlayNextTrack(sender, e);

            }
        }

        private void btn_PlayingLast_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MainWindow.music.playlistCount >= 2)
                {
                    MainWindow.music.userPressedLast = true;
                    MainWindow.wavePlayer.Stop();
                    WriteToDebugConsole("Skip Audio Pressed");
                }
               
            }
            catch (Exception ex)
            {

                currentDebugConsole.WriteToDebugConsole(ex.Message);
            }
        }

        private void btn_Skip_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MainWindow.music.playlistCount >= 2)
                {
                    MainWindow.wavePlayer.Stop();
                    MainWindow.music.userPressedNext = true;
                    WriteToDebugConsole("Skip Audio Pressed");
                }


            }
            catch (Exception ex)
            {
                currentDebugConsole.WriteToDebugConsole(ex.Message);



            }
        }

        private void OnButtonStopClick(object sender, RoutedEventArgs e)
        {
            StopPlayer();
        }

        private void StopPlayer()
        {

            if (MainWindow.music.playlistCount >= MainWindow.music.currentTrackPlaylistId)
            {
                if (MainWindow.music.playingTrack)
                { //pause 
                  //switch to next track.
                    MainWindow.music.userPressedStop = true;
                    MainWindow.music.trackPaused = true;
                    SetPlayStatus("-Stopped-");
                    timer1.Stop();
                    MainWindow.wavePlayer.Pause();

                }

            }
            else if (MainWindow.music.playlistCount <= MainWindow.music.currentTrackPlaylistId)
            {
                if (MainWindow.music.playingTrack)
                {
                    MainWindow.music.userPressedStop = true;
                    MainWindow.wavePlayer.Stop();

                    MainWindow.music.ClearAudioPlaylist();
                }

            }


        }

        private void btn_StopAll_Click(object sender, RoutedEventArgs e)
        {
            

                if (MainWindow.music.playingTrack)
                {
                    MainWindow.music.userPressedStop = true;
                    MainWindow.wavePlayer.Stop();
                    MainWindow.music.ClearAudioPlaylist();
                }

            
        }

        private void OnVolumeSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MainWindow.music.playingTrack)
            {
                MainWindow.audioFile.Volume = (float)volumeSlider3.Value / 100;
            }
        }

        private void lbl_PlayingLink_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string link = MainWindow.music.currentTrack.url;
            try
            {
                Process proc = new Process();
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.FileName = link;
                proc.Start();


            }
            catch (Exception ex)
            {
                WriteToDebugConsole(ex.Message);
            }
        }

        private void linkLabel1_Click(object sender, RoutedEventArgs e)
        {
            string link = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Maple";

            try
            {

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    Arguments = link,
                    FileName = "explorer.exe"

                };

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                WriteToDebugConsole(ex.Message);
            }
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            lbl_PlayingLink.Cursor = Cursors.Hand;
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            lbl_PlayingLink.Cursor = Cursors.Arrow;
        }

        private void TextBlock_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string link = MainWindow.music.currentTrack.url;
            try
            {
                Process proc = new Process();
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.FileName = link;
                proc.Start();


            }
            catch (Exception ex)
            {
                WriteToDebugConsole(ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.musicFormOpen = false;
            if (MainWindow.music.playingTrack)
            {
                MainWindow.music.userPressedStop = true;
                MainWindow.wavePlayer.Stop();
                MainWindow.music.ClearAudioPlaylist();
            }
           
        }
    }
}
