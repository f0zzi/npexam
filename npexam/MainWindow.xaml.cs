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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void LbMail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MailInfo m = (MailInfo)(sender as ListBox).SelectedItem;
            //Mail message = client.GetMail(m);
            //tbFrom.Text = message.From.ToString();
            //tbTo.Text = tbLogin.Text;
            //tbSubject.Text = message.Subject;
            //tbBody.Text = message.TextBody;
            //foreach (var item in message.Attachments)
            //{
            //    lbAttach.Items.Add(item);
            //}
        }
    }
}
