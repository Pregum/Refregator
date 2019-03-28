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

using MVVM_Refregator.ViewModel;
using MVVM_Refregator.Model;

namespace MVVM_Refregator.View
{
    /// <summary>
    /// StartupWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class StartupWindow : Window
    {
        public StartupWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new Uri("/View/DashBoardPage.xaml", UriKind.Relative));
        }

        private void Frame_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == System.Windows.Navigation.NavigationMode.Back)
            {
                e.Cancel = true;
            }
            else if (e.NavigationMode == System.Windows.Navigation.NavigationMode.Forward)
            {
                e.Cancel = true;
            }
            else if (e.NavigationMode == System.Windows.Navigation.NavigationMode.Refresh)
            {
                e.Cancel = true;
            }
        }
    }
}
