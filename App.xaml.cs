using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using MVVM_Refregator.Model;
using MVVM_Refregator.View;

using Notifications.Wpf;

namespace MVVM_Refregator
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // 食材データを読み込む(既定値はfood_composition.json)
            var ins = FoodShelfModel.GetInstance();
            ins.Load();

            // 食品成分表の読み込みを行う(既定値food_composition_japanese.json)
            AnalysisPageModel.GetInstance().LoadFoodComposition();

            if (ins.FoodCollection.Any(x => x.HasUsed == false && x.LimitDate.Date < DateTime.Today))
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show(new NotificationContent
                {
                    Title = "食材管理アプリ",
                    Message = "期限が過ぎている食材があります。",
                    Type = NotificationType.Warning,
                });
            }

            // Viewの起動
            var view = new StartupWindow();
            view.Show();
        }
    }
}
