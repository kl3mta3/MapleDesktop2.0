using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

using Label = System.Windows.Controls.Label;

namespace MapleDesktop2._0
{
    /// <summary>
    /// Interaction logic for LinksForm.xaml
    /// </summary>
    public partial class LinksForm : Window

    {

        internal static Label quickLinkNameToChange = new Label();
        internal static Button quickLinkButtonToChange = new Button();
        internal static int currentquickLinkId = 0;


        internal static Label quickAppNameToChange = new Label();
        internal static Button quickAppButtonToChange = new Button();
        internal static bool systemConfigRan = false;

        internal static SetAppsForm currentSetAppsForm = new SetAppsForm();
        internal static SetLinksForm currentSetLinksForm = new SetLinksForm();


        public LinksForm()
        {
            InitializeComponent();
            ConfigureSystem();
        }



        internal void ConfigureSystem()
        {
            

                if (Properties.Settings.Default.QuickLink1Name != "Void")
                {
                    lbl_QuickLink1Name.Content = Properties.Settings.Default.QuickLink1Name;
                    //lbl_QuickLink1.Content = Properties.Settings.Default.QuickLink1;
                    btn_QuickLink1.Content = Properties.Settings.Default.QuickLink1Name;

                }
                else
                {
                    lbl_QuickLink1Name.Content = "";
                    //lbl_QuickLink1.Content = "";
                    btn_QuickLink1.Content = "Add --->";

                }
                if (Properties.Settings.Default.QuickLink2Name != "Void")
                {
                    lbl_QuickLink2Name.Content = Properties.Settings.Default.QuickLink2Name;
                    //lbl_QuickLink2.Content = Properties.Settings.Default.QuickLink2;
                    btn_QuickLink2.Content = Properties.Settings.Default.QuickLink2Name;

                }
                else
                {
                    lbl_QuickLink2Name.Content = "";
                    //lbl_QuickLink2.Content = "";
                    btn_QuickLink2.Content = "Add --->";

                }
                if (Properties.Settings.Default.QuickLink3Name != "Void")
                {
                    lbl_QuickLink3Name.Content = Properties.Settings.Default.QuickLink3Name;
                    //lbl_QuickLink3.Content = Properties.Settings.Default.QuickLink3;
                    btn_QuickLink3.Content = Properties.Settings.Default.QuickLink3Name;

                }
                else
                {
                    lbl_QuickLink3Name.Content = "";
                    //lbl_QuickLink3.Content = "";
                    btn_QuickLink3.Content = "Add --->";

                }
                if (Properties.Settings.Default.QuickLink4Name != "Void")
                {
                    lbl_QuickLink4Name.Content = Properties.Settings.Default.QuickLink4Name;
                    //lbl_QuickLink4.Content = Properties.Settings.Default.QuickLink4;
                    btn_QuickLink4.Content = Properties.Settings.Default.QuickLink4Name;

                }
                else
                {
                    lbl_QuickLink4Name.Content = "";
                    //lbl_QuickLink4.Content = "";
                    btn_QuickLink4.Content = "Add --->";

                }
                if (Properties.Settings.Default.QuickLink5Name != "Void")
                {
                    lbl_QuickLink5Name.Content = Properties.Settings.Default.QuickLink5Name;
                    // lbl_QuickLink5.Content = Properties.Settings.Default.QuickLink5;
                    btn_QuickLink5.Content = Properties.Settings.Default.QuickLink5Name;

                }
                else
                {
                    lbl_QuickLink5Name.Content = "";
                    //lbl_QuickLink5.Content = "";
                    btn_QuickLink5.Content = "Add --->";

                }
                if (Properties.Settings.Default.QuickLink4Name != "Void")
                {
                    lbl_QuickLink6Name.Content = Properties.Settings.Default.QuickLink6Name;
                    //  lbl_QuickLink6.Content = Properties.Settings.Default.QuickLink6;
                    btn_QuickLink6.Content = Properties.Settings.Default.QuickLink6Name;

                }
                else
                {
                    lbl_QuickLink6Name.Content = "";
                    //  lbl_QuickLink6.Content = "";
                    btn_QuickLink6.Content = "Add --->";

                }

                if (Properties.Settings.Default.QuickApp1Name != "")
                {

                    //btn_QuickApp1.Content = Properties.Settings.Default.QuickApp1;
                    lbl_QuickApp1Name.Content = Properties.Settings.Default.QuickApp1Name;
                    btn_QuickApp1.Content = Properties.Settings.Default.QuickApp1Name;

                }
                else
                {
                    //btn_QuickApp1.Content = "";
                    lbl_QuickApp1Name.Content = "";
                    btn_QuickApp1.Content = "Add --->";

                }
                if (Properties.Settings.Default.QuickApp2Name != "")
                {

                    //btn_QuickApp2.Content = Properties.Settings.Default.QuickApp2;
                    lbl_QuickApp2Name.Content = Properties.Settings.Default.QuickApp2Name;
                    btn_QuickApp2.Content = Properties.Settings.Default.QuickApp2Name;

                }
                else
                {
                   // btn_QuickApp2.Content = "";
                    lbl_QuickApp2Name.Content = "";
                    btn_QuickApp2.Content = "Add --->";

                }
                if (Properties.Settings.Default.QuickApp3Name != "")
                {

                    //btn_QuickApp3.Content = Properties.Settings.Default.QuickApp3;
                    lbl_QuickApp3Name.Content = Properties.Settings.Default.QuickApp3Name;
                    btn_QuickApp3.Content = Properties.Settings.Default.QuickApp3Name;

                }
                else
                {
                    //btn_QuickApp3.Content = "";
                    lbl_QuickApp3Name.Content = "";
                    btn_QuickApp3.Content = "Add --->";

                }


                   

        }

        private void btn_Google_Click(object sender, RoutedEventArgs e)
        {
            string link = "https://www.Google.com";
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

        private void btn_Gmail_Click(object sender, RoutedEventArgs e)
        {
            string link = "https://www.Gmail.com";
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

        private void btn_Youtube_Click(object sender, RoutedEventArgs e)
        {
            string link = "https://www.Youtube.com";
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


        internal void OpenSetLinksWindow(int linkid)
        {

            if (MainWindow.currentSetLinksFormOpen)
            {
                return;

            }

            SetLinksForm current = new SetLinksForm();
            currentSetLinksForm = current;
            currentSetLinksForm.SetLinkId(linkid);
            currentSetLinksForm.Show();
            MainWindow.currentSetLinksFormOpen = true;
        }

        internal void OpenSetAppsWindow(int appid)
        {

            if (MainWindow.currentSetLinksFormOpen)
            {
                return;

            }

            SetAppsForm current = new SetAppsForm();
            currentSetAppsForm = current;
            currentSetAppsForm.SetLinkId(appid);
            currentSetAppsForm.Show();
            MainWindow.currentSetAppsFormOpen = true;
        }


        internal void CloseSetLinksWindow()
        {

            currentSetAppsForm.Hide();
            currentSetLinksForm = new SetLinksForm();
            MainWindow.currentSetLinksFormOpen = false;
        }





        internal void CloseSetAppsWindow()
        {

            currentSetAppsForm.Hide();
            currentSetAppsForm = new SetAppsForm();
            MainWindow.currentSetAppsFormOpen = false;
        }


        private void btn_Notepad_Click(object sender, RoutedEventArgs e)
        {
            string link = "notepad.exe";
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

        private void btn_Calculator_Click(object sender, RoutedEventArgs e)
        {
            string link = "Calc.exe";
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

        private void btn_TaskManager_Click(object sender, RoutedEventArgs e)
        {
            string link = "Taskmgr.exe";
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

        private void btn_QLSetup1_Click(object sender, RoutedEventArgs e)
        {


            OpenSetLinksWindow(1);


        }

        private void btn_QLSetup2_Click(object sender, RoutedEventArgs e)
        {
            OpenSetLinksWindow(2);
        }

        private void btn_QLSetup3_Click(object sender, RoutedEventArgs e)
        {
            OpenSetLinksWindow(3);
        }

        private void btn_QLSetup4_Click(object sender, RoutedEventArgs e)
        {
            OpenSetLinksWindow(4);
        }

        private void btn_QLSetup5_Click(object sender, RoutedEventArgs e)
        {
            OpenSetLinksWindow(5);
        }

        private void btn_QLSetup6_Click(object sender, RoutedEventArgs e)
        {
            OpenSetLinksWindow(6);
        }

        private void Links_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.linksFormOpen = false;
        }

        private void btn_QuickLink1_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.QuickLink1 != "Enter Url...")
            {
                string link = Properties.Settings.Default.QuickLink1;

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
        }

        private void btn_QuickLink2_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.QuickLink2 != "Enter Url...")
            {
                string link = Properties.Settings.Default.QuickLink2;

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
        }

        private void btn_QuickLink3_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.QuickLink3 != "Enter Url...")
            {
                string link = Properties.Settings.Default.QuickLink3;

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
        }

        private void btn_QuickLink4_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.QuickLink4 != "Enter Url...")
            {
                string link = Properties.Settings.Default.QuickLink4;

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
        }

        private void btn_QuickLink5_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.QuickLink5 != "Enter Url...")
            {
                string link = Properties.Settings.Default.QuickLink5;

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
        }

        private void btn_QuickLink6_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.QuickLink6 != "Enter Url...")
            {
                string link = Properties.Settings.Default.QuickLink6;

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
        }

        private void btn_QAppSetup1_Click(object sender, RoutedEventArgs e)
        {
            OpenSetAppsWindow(1);
        }

        private void btn_QAppSetup2_Click(object sender, RoutedEventArgs e)
        {
            OpenSetAppsWindow(2);
        }

        private void btn_QAppSetup3_Click(object sender, RoutedEventArgs e)
        {
            OpenSetAppsWindow(3);
        }

        private void btn_QuickApp1_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.QuickApp1 != "Void")
            {
                string link = Properties.Settings.Default.QuickApp1;
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
        }

        private void btn_QuickApp2_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.QuickApp2 != "Void")
            {
                string link = Properties.Settings.Default.QuickApp2;
                try
                {
                    Process proc = new Process();
                    proc.StartInfo.UseShellExecute = true;
                    proc.StartInfo.FileName = link;
                    proc.Start();


                }
                catch (Exception)
                {
                    // WriteToMapleConsole(ex.Message);
                }
            }
        }

        private void btn_QuickApp3_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.QuickApp3 != "Void")
            {
                string link = Properties.Settings.Default.QuickApp3;
                try
                {
                    Process proc = new Process();
                    proc.StartInfo.UseShellExecute = true;
                    proc.StartInfo.FileName = link;
                    proc.Start();


                }
                catch (Exception)
                {
                    // WriteToMapleConsole(ex.Message);
                }
            }
        }
    }
}
