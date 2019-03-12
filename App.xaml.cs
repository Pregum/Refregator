using System;
using System.Linq;
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

        private NotificationContent _notificationContent = new NotificationContent
        {
            Title = "食材管理アプリ",
            Message = "期限が今日までの食材があります。",
            Type = NotificationType.Warning,
        };

        private NotificationContent oneDayLimitContent = new NotificationContent
        {
            Title = "食材管理アプリ",
            Message = "期限が1週間以内の食材があります。",
            Type = NotificationType.Warning,
        };

        private NotificationManager _notificationManager = new NotificationManager();

        private bool canNotification = MVVM_Refregator.Properties.Settings.Default.WindowNotificationStatus;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // 食材データを読み込む(既定値はfood_composition.json)
            var ins = FoodShelfModel.GetInstance();
            ins.Load();

            // 食品成分表の読み込みを行う(既定値food_composition_japanese.json)
            AnalysisPageModel.GetInstance().LoadFoodComposition();

            if (this.canNotification)
            {
                if (ins.FoodCollection.Any(x => x.HasUsed == false && x.LimitDate.Date == DateTime.Today.Date))
                {
                    this._notificationManager.Show(this._notificationContent, expirationTime: TimeSpan.FromSeconds(4));
                }
                else if (ins.FoodCollection.Any(x => x.HasUsed == false && x.LimitDate.Date <= DateTime.Today.Date.AddDays(7).Date && x.LimitDate.Date >= DateTime.Today))
                {
                    this._notificationManager.Show(this.oneDayLimitContent, expirationTime: TimeSpan.FromSeconds(4));
                }
            }

            // Viewの起動
            var view = new StartupWindow();
            view.ShowDialog();

            // Notification消すため
            Application.Current.Shutdown();
        }
    }
}
