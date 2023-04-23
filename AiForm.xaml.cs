using OpenAI_API;
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
    /// Interaction logic for AiForm.xaml
    /// </summary>
    public partial class AiForm : Window
    {
        internal static string question = "";
        public AiForm()
        {
            InitializeComponent();
            ConfigureSystem();
        }

        internal void ConfigureSystem()
        {
            rtb_MapleConsole.Document.Blocks.Clear();
            rtb_MapleInput.Document.Blocks.Clear();
        }



        public void AskQuestion(string value)
        {
            question = value;
            StreamGPT();
        }


        public async Task StreamGPT()
        {

            OpenAIAPI api = new OpenAIAPI("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"); // shorthand

            try
            {

                var chat1 = api.Chat.CreateConversation();
                chat1.AppendUserInput(question);
                InsertMapleTag();
                await foreach (var res in chat1.StreamResponseEnumerableFromChatbotAsync())
                {
                    WriteToMapleConsole(res);
                }
            }
            catch (Exception ex)
            {

                WriteToMapleConsole($"{ex.Message}");


            }

        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //string token = Properties.Settings.Default.OpenAiToken;
            txb_ApiInput.Text = Properties.Settings.Default.OpenAiToken;
        }

        private void Button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            txb_ApiInput.Text = "";
        }

        private void txb_ApiInput_GotFocus(object sender, RoutedEventArgs e)
        {
            txb_ApiInput.Text = "";
        }

        private void btn_SaveAPI_Click(object sender, RoutedEventArgs e)
        {
            string token = txb_ApiInput.Text;
            WriteToMapleConsole(txb_ApiInput.Text);



            if (token.StartsWith("sk"))
            {
                if (token.Length > 5)
                {
                    Properties.Settings.Default.OpenAiToken = token;
                    Properties.Settings.Default.Save();
                    //Config.AiConfig.OpenAiApiToken = token;
                    txb_ApiInput.Text = "";
                    SetApiSaved();
                    WriteToMapleConsole("Token Saved");
                }
                else
                {
                    WriteToMapleConsole("Token too short");

                }
            }
            else
            {

                WriteToMapleConsole("Token must start with sk");
            }
        }
        private void SetApiSaved()
        {

            ckb_SavedApi.IsChecked = true;
        }

        internal void WriteToMapleConsole(string message)
        {

            rtb_MapleConsole.AppendText(message);
            rtb_MapleConsole.ScrollToEnd();
        }

        internal void btn_InputEnter_Click(object sender, RoutedEventArgs e)
        {
            question = new TextRange(rtb_MapleInput.Document.ContentStart, rtb_MapleInput.Document.ContentEnd).Text;

            WriteToMapleConsole($"Me: {question}");
            AskQuestion(question);

            rtb_MapleInput.Document.Blocks.Clear();
            rtb_MapleInput.Focus();
        }

        internal void ClearMapleInput()
        {


            rtb_MapleConsole.Document.Blocks.Clear();
        }
        private void btn_ClearMapleConsole_Click(object sender, EventArgs e)
        {
            ClearMapleConsole();
        }

        internal void InsertMapleTag()
        {
            rtb_MapleConsole.Document.Blocks.Add(new Paragraph(new Run(Environment.NewLine + "Maple: ")));
        }

        internal void ClearMapleConsole()
        {
            rtb_MapleConsole.Document.Blocks.Clear();

        }

        private void btn_ClearConsole_Click(object sender, RoutedEventArgs e)
        {
            ClearMapleConsole();
        }

        private void rtb_MapleInput_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Return)
            {
                question = new TextRange(rtb_MapleInput.Document.ContentStart, rtb_MapleInput.Document.ContentEnd).Text;

                WriteToMapleConsole($"Me: {question}");
                AskQuestion(question);

                rtb_MapleInput.Document.Blocks.Clear();
                rtb_MapleInput.Focus();
            }
        }

        private void ChatForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.aiFormOpen = false;
        }
    }
}
