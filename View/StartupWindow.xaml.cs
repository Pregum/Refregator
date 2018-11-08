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

            //if (App.Current.Resources["FoodShelfKey"] is FoodShelfModel foodShelfModel)
            //{
            //    //foodShelfModel.FoodCollection.Add(new Food());
            //    this.DataContext = new StartupViewModel(foodShelfModel);
            //}
            this.DataContext = new StartupViewModel();
        }
    }
}
