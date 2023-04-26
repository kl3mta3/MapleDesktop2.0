using System.Windows;

namespace MapleDesktop2._0
{
    /// <summary>
    /// Interaction logic for SetAppsForm.xaml
    /// </summary>
    public partial class SetAppsForm : Window
    {
        internal static int appid = 0;
        public SetAppsForm()
        {
            InitializeComponent();
        }

        internal void SetLinkId(int id)
        {

            appid = id;
            lbl_Name.Content = $"Set QuickLink{id}";

        }


        internal void SetLink()
        {
            string link = "";
            string name = "";
            if (appid > 0)
            {
                switch (appid)
                {
                    case 1:
                        link = txb_QuickAppPath.Text;
                        name = txb_QuickAppName.Text;
                        Properties.Settings.Default.QuickApp1 = link;
                        Properties.Settings.Default.QuickApp1Name = name;
                        Properties.Settings.Default.Save();
                        MainWindow.currentLinksForm.btn_QuickApp1.Content = name;
                        MainWindow.currentLinksForm.lbl_QuickApp1Name.Content = name;

                        break;

                    case 2:
                        link = txb_QuickAppPath.Text;
                        name = txb_QuickAppName.Text;
                        Properties.Settings.Default.QuickApp2 = link;
                        Properties.Settings.Default.QuickApp2Name = name;
                        Properties.Settings.Default.Save();
                        MainWindow.currentLinksForm.btn_QuickApp2.Content = name;
                        MainWindow.currentLinksForm.lbl_QuickApp2Name.Content = name;

                        break;
                    case 3:
                        link = txb_QuickAppPath.Text;
                        name = txb_QuickAppName.Text;
                        Properties.Settings.Default.QuickApp3 = link;
                        Properties.Settings.Default.QuickApp3Name = name;
                        Properties.Settings.Default.Save();
                        MainWindow.currentLinksForm.btn_QuickApp3.Content = name;
                        MainWindow.currentLinksForm.lbl_QuickApp3Name.Content = name;

                        break;


                }

                CancelWindow();
            }







        }

        internal void CancelWindow()
        {

            this.Close();


        }

        private void btn_QuickAppSet_Click(object sender, RoutedEventArgs e)
        {
            SetLink();
        }

        private void btn_QuickAppCancel_Click(object sender, RoutedEventArgs e)
        {
            CancelWindow();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.currentSetAppsFormOpen = false;
        }
    }
}
