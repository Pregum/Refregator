using System.Windows.Controls;
using System.Windows.Input;

using MVVM_Refregator.ViewModel;

namespace MVVM_Refregator.View
{
    /// <summary>
    /// StepOfSettingFoodName.xaml の相互作用ロジック
    /// </summary>
    public partial class StepOfSettingFoodName : Page
    {
        public StepOfSettingFoodName()
        {
            InitializeComponent();
        }

        private void Button_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            var window = new IconSelectDialog();
            window.ShowDialog();
            var dt = this.DataContext as StepOfFoodNameViewModel;
            if (dt != null && window.IconVM.IsCancel == false)
            {
                dt.Food.Value.Image = window.IconVM.SelectedImage;
            }
        }
    }
}
