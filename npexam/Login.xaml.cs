using EAGetMail;
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

namespace npexam
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        MailClient client = null;
        MailServer server = null;
        public Login()
        {
            InitializeComponent();
            client = new MailClient("TryIt");
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbEmail.Text) || String.IsNullOrWhiteSpace(tbPassword.Password))
                MessageBox.Show("Enter Login/Password");
            else
            {
                server = new MailServer(
                    "imap.gmail.com",
                    tbEmail.Text,
                    tbPassword.Password,
                    ServerProtocol.Imap4)
                {
                    SSLConnection = true,
                    Port = 993
                };
                try
                {
                    client.Connect(server);
                    if (client.Connected)
                        MessageBox.Show("Connected");
                    //Close();
                    else
                        MessageBox.Show("Error. Invalid e-mail/password");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CloseHandler(object sender, EventArgs e)
        {

        }
    }
}
