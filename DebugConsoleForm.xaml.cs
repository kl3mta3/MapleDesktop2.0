using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            e.Cancel = true;
           
            MainWindow.debugConsoleOpen = false;
        }

        private void btn_ClearConsole_Click(object sender, RoutedEventArgs e)
        {
            rtb_DebugWindow.Document.Blocks.Clear();
        }
    }
}
