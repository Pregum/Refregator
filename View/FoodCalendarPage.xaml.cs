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
    /// FoodCalendarPage.xaml の相互作用ロジック
    /// </summary>
    public partial class FoodCalendarPage : Page
    {
        public FoodCalendarPage()
        {
            InitializeComponent();
        }

        private void ItemsControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var tes = (ItemsControl)sender;
            //System.Windows.MessageBox.Show(tes.DataContext.ToString());
            System.Diagnostics.Debug.WriteLine("call by datacontext changed. : " + tes.DataContext.ToString());
            this.foodShelf.Send_BindingFoods.Execute(tes);
        }

        private void ItemsControl_Initialized(object sender, EventArgs e)
        {
            var tes = (ItemsControl)sender;
            //System.Windows.MessageBox.Show(tes.DataContext.ToString());
            System.Diagnostics.Debug.WriteLine("call by dataContextChanged : " + tes.DataContext.ToString());
            this.foodShelf.Send_BindingFoods.Execute(tes);
        }
    }
}
