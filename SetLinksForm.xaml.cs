using System;
using System.Collections.Generic;
using System.Deployment.Internal;
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
    /// Interaction logic for SetLinksForm.xaml
    /// </summary>
    public partial class SetLinksForm : Window
    {
        internal static int linkid = 0;
  


        public SetLinksForm()
        {
            InitializeComponent();
        }


        internal void SetLinkId(int id)
        {

            linkid = id;
            lbl_Name.Content = $"Set QuickLink{id}";

        }





        internal void SetLink()
        {
            string link = "";
            string name = "";
            if (linkid > 0)
            {
                switch (linkid)
                {
                    case 1:
                        link = txb_QuickLinkPath.Text;
                        name = txb_QuicklinkName.Text;
                        Properties.Settings.Default.QuickLink1 = link;
                        Properties.Settings.Default.QuickLink1Name = name;
                        Properties.Settings.Default.Save();
                        MainWindow.currentLinksForm.btn_QuickLink1.Content = name;
                        MainWindow.currentLinksForm.lbl_QuickLink1Name.Content = name;

                        break;

                    case 2:
                        link = txb_QuickLinkPath.Text;
                        name = txb_QuicklinkName.Text;
                        Properties.Settings.Default.QuickLink2 = link;
                        Properties.Settings.Default.QuickLink2Name = name;
                        Properties.Settings.Default.Save();
                        MainWindow.currentLinksForm.btn_QuickLink2.Content = name;
                        MainWindow.currentLinksForm.lbl_QuickLink2Name.Content = name;

                        break;
                    case 3:
                        link = txb_QuickLinkPath.Text;
                        name = txb_QuicklinkName.Text;
                        Properties.Settings.Default.QuickLink3 = link;
                        Properties.Settings.Default.QuickLink3Name = name;
                        Properties.Settings.Default.Save();
                        MainWindow.currentLinksForm.btn_QuickLink3.Content = name;
                        MainWindow.currentLinksForm.lbl_QuickLink3Name.Content = name;

                        break;

                    case 4:
                        link = txb_QuickLinkPath.Text;
                        name = txb_QuicklinkName.Text;
                        Properties.Settings.Default.QuickLink4 = link;
                        Properties.Settings.Default.QuickLink4Name = name;
                        Properties.Settings.Default.Save();
                        MainWindow.currentLinksForm.btn_QuickLink4.Content = name;
                        MainWindow.currentLinksForm.lbl_QuickLink4Name.Content = name;

                        break;
                    case 5:
                        link = txb_QuickLinkPath.Text;
                        name = txb_QuicklinkName.Text;
                        Properties.Settings.Default.QuickLink5 = link;
                        Properties.Settings.Default.QuickLink5Name = name;
                        Properties.Settings.Default.Save();
                        MainWindow.currentLinksForm.btn_QuickLink5.Content = name;
                        MainWindow.currentLinksForm.lbl_QuickLink5Name.Content = name;

                        break;

                    case 6:
                        link = txb_QuickLinkPath.Text;
                        name = txb_QuicklinkName.Text;
                        Properties.Settings.Default.QuickLink6 = link;
                        Properties.Settings.Default.QuickLink6Name = name;
                        Properties.Settings.Default.Save();
                        MainWindow.currentLinksForm.btn_QuickLink6.Content = name;
                        MainWindow.currentLinksForm.lbl_QuickLink6Name.Content = name;

                        break;
                }

                CancelWindow();
            }


            




        }

        internal void CancelWindow()
        {

            this.Close();


        }

        private void btn_QuickLinkSet_Click(object sender, RoutedEventArgs e)
        {
            SetLink();
        }

        private void btn_QuickLinkCancel_Click(object sender, RoutedEventArgs e)
        {
            CancelWindow();
        }

        private void SetLinksWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.currentSetLinksFormOpen = false;
        }
    }
}
