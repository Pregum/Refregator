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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVM_Refregator.View
{
    /// <summary>
    /// SettingPage.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingPage : Page
    {
        public SettingPage()
        {
            var canNotify = Properties.Settings.Default.WindowNotificationStatus; 
            InitializeComponent();

            this.DataContext = System.IO.File.ReadAllText(@"license.txt");

            if (canNotify) { this.OnNotification.IsChecked = true; }
            else { this.OffNotification.IsChecked = true; }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.WindowNotificationStatus = true;
            Properties.Settings.Default.Save();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.WindowNotificationStatus = false;
            Properties.Settings.Default.Save();
        }
    }
}
