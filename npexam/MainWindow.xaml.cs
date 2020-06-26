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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class MyLetter
    {
        public Mail mail { get; set; }
        public MailInfo info { get; set; }
        public MyLetter(MailInfo in_info, Mail in_mail)
        {
            mail = in_mail;
            info = in_info;
        }
    }
    public partial class MainWindow : Window
    {
        MailClient client = null;
        public MainWindow(MailClient client)
        {
            InitializeComponent();
            this.client = client;
            Title = client.CurrentMailServer.User.ToString();
            Refresh();
        }
        private void Refresh()
        {
            lbMail.Items.Clear();
            Task.Run(Init);
        }
        private void LbMail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbMail.SelectedItem != null)
            {
                Mail message = ((sender as ListBox).SelectedItem as MyLetter).mail;
                tbFrom.Text = message.From.ToString();
                tbTo.Text = Title;
                tbSubject.Text = message.Subject;
                tbBody.Text = message.TextBody;
                lbAttach.Items.Clear();
                foreach (var item in message.Attachments)
                {
                    lbAttach.Items.Add(item);
                }
            }
        }
        private void Init()
        {
            var messages = client.u_GetMailInfos();
            foreach (var mailinfo in messages)
            {
                Mail tmpMail = client.GetMail(mailinfo);
                tmpMail.Subject = tmpMail.Subject.Replace("(Trial Version)", "");
                lbMail.Dispatcher.Invoke(() =>
                {
                    lbMail.Items.Add(new MyLetter(mailinfo, tmpMail));
                });
            }
        }
        private void ClearFields()
        {
            tbFrom.Text = tbTo.Text = tbSubject.Text = tbBody.Text = "";
            lbAttach.Items.Clear();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            client.Delete((lbMail.SelectedItem as MyLetter).info);
            lbMail.Items.Remove(lbMail.SelectedItem);
        }

        private void Reply_Click(object sender, RoutedEventArgs e)
        {
            if (lbMail.SelectedItem != null)
            {
                Mail message = (lbMail.SelectedItem as MyLetter).mail;
                Letter letter = new Letter(client, message.Subject, message.From);
                letter.ShowDialog();
            }
        }

        private void Compose_Click(object sender, RoutedEventArgs e)
        {
            Letter letter = new Letter(client);
            letter.ShowDialog();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
