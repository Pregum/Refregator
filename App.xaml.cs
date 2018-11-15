using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MVVM_Refregator.Model;

using MVVM_Refregator.View;

namespace MVVM_Refregator
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // food_composition.jsonを読み込む
            //FoodShelfModel.GetInstance().Load();
            AnalysisPageModel.GetInstance();

            var view = new StartupWindow();
            view.Show();
        }
    }
}
