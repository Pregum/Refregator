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
using MVVM_Refregator.ViewModel;

namespace MVVM_Refregator.View
{
    /// <summary>
    /// AnalysisPage.xaml の相互作用ロジック
    /// </summary>
    public partial class AnalysisPage : Page
    {
        public AnalysisPage()
        {
            InitializeComponent();
        }

        private void rootCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            var dc = this.DataContext as AnalysisPageViewModel;
            dc.CalcComposition( (DateTime)e.AddedItems[0] );
        }
    }
}
