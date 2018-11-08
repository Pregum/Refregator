using MVVM_Refregator.Model;
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
    /// FoodCalendarPage.xaml の相互作用ロジック
    /// </summary>
    public partial class FoodCalendarPage : Page
    {
        public FoodCalendarPage()
        {
            InitializeComponent();

            //if (App.Current.Resources["FoodShelfKey"] is FoodShelfModel foodShelfModel)
            //{
            //    if (this.DataContext == null)
            //    {
            //        this.DataContext = new ViewModel.FoodShelfViewModel(foodShelfModel);
            //    }
            //}
            //else
            //{
            //    this.DataContext = new ViewModel.FoodShelfViewModel();
            //}

            this.DataContext = this.DataContext ?? new FoodShelfViewModel();
        }
    }
}
