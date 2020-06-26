using EAGetMail;
using EASendMail;
using Microsoft.Win32;
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
    /// Interaction logic for Letter.xaml
    /// </summary>
    public partial class Letter : Window
    {
        public MailClient client;
        public Letter(MailClient client, string subject = null, EAGetMail.MailAddress address = null)
        {
            InitializeComponent();
            this.client = client;
            tbFrom.Text = client.CurrentMailServer.User.ToString();
            if (!String.IsNullOrWhiteSpace(subject) && address != null)
            {
                tbTo.Text = address.ToString();
                tbSubject.Text = "Re: " + subject;
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(tbTo.Text))
            {
                SmtpServer server = new SmtpServer("smtp.gmail.com")
                {
                    Port = 465,
                    ConnectType = SmtpConnectType.ConnectSSLAuto,
                    User = this.client.CurrentMailServer.User,
                    Password = this.client.CurrentMailServer.Password
                };

                SmtpMail message = new SmtpMail("TryIt")
                {
                    From = this.client.CurrentMailServer.User,
                    To = tbTo.Text,
                    Subject = tbSubject.Text,
                    TextBody = tbBody.Text,
                    Priority = EASendMail.MailPriority.High
                };
                foreach (string item in lbAttach.Items)
                {
                    message.AddAttachment(item);
                }

                SmtpClient smtpclient = new SmtpClient();
                smtpclient.Connect(server);

                try
                {
                    smtpclient.SendMail(message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            Close();
        }

        private void Attach_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if(openFileDialog.ShowDialog() == true)
            {
                foreach (string item in openFileDialog.FileNames)
                {
                    lbAttach.Items.Add(item);
                }
            }
        }
    }
}
