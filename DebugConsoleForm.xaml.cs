using System;
using System.ComponentModel;
using System.Windows;


namespace MapleDesktop2._0
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class DebugConsoleForm : Window
    {
        public DebugConsoleForm()
        {
            InitializeComponent();
        }

        //private void rtb_DebugWindow_TextChanged(object sender, EventArgs e)
        //{


        //}



        public void WriteToDebugConsole(string message)
        {
            //rtb_DebugWindow.Text = rtb_DebugWindow.Text + Environment.NewLine + message;

            rtb_DebugWindow.AppendText(Environment.NewLine + message);
            rtb_DebugWindow.ScrollToEnd();
        }





        private void Window_Closing(object sender, CancelEventArgs e)
        {
            // e.Cancel = true;

            MainWindow.debugConsoleOpen = false;
        }

        private void btn_ClearConsole_Click(object sender, RoutedEventArgs e)
        {
            rtb_DebugWindow.Document.Blocks.Clear();
        }
    }
}
