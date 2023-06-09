﻿using OpenAI_API;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

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


            if (Properties.Settings.Default.OpenAiToken != "Void")
            {
                SetApiSaved();
            }



        }



        public void AskQuestion(string value)
        {
            question = value;
            StreamGPT();
        }


        public async Task StreamGPT()
        {
            // WriteToMapleConsole("Using API Key"+ Properties.Settings.Default.OpenAiToken);
            OpenAIAPI api = new OpenAIAPI(Properties.Settings.Default.OpenAiToken);

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
            //WriteToMapleConsole(txb_ApiInput.Text);



            if (!token.StartsWith("sk"))
            {
                WriteToMapleConsole("Token must start with sk");
                return;
            }

            if (token.Length > 5)
            {
                Properties.Settings.Default.OpenAiToken = token;
                Properties.Settings.Default.Save();
                //Config.AiConfig.OpenAiApiToken = token;
                txb_ApiInput.Text = "";
                SetApiSaved();
                WriteToMapleConsole($"{Properties.Settings.Default.OpenAiToken} Registered");
                WriteToMapleConsole($"{Environment.NewLine}Token Saved");
            }
            else
            {
                WriteToMapleConsole("Token too short");

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

        internal void PostQuestionToMapleConsole(string message)
        {
            if (rtb_MapleConsole.Document.Blocks.Count >= 1)
            {
                rtb_MapleConsole.AppendText(Environment.NewLine);

            }
            rtb_MapleConsole.AppendText(message);
            rtb_MapleConsole.ScrollToEnd();
        }

        internal void btn_InputEnter_Click(object sender, RoutedEventArgs e)
        {
            question = new TextRange(rtb_MapleInput.Document.ContentStart, rtb_MapleInput.Document.ContentEnd).Text;

            PostQuestionToMapleConsole($"User: {question}");
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
            rtb_MapleConsole.Document.Blocks.Add(new Paragraph(new Run(Environment.NewLine + "MapleAI: ")));
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

                PostQuestionToMapleConsole($"User: {question}");
                AskQuestion(question);

                rtb_MapleInput.Document.Blocks.Clear();
                rtb_MapleInput.Focus();
                e.Handled = true;
            }
        }

        private void ChatForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.aiFormOpen = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string link = "https://platform.openai.com/";
            try
            {
                Process proc = new Process();
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.FileName = link;
                proc.Start();


            }
            catch
            {

            }
        }
    }
}
