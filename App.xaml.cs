using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Resources;
using MVVM_Refregator.Model;
using MVVM_Refregator.View;

using Windows.Storage;
using Notifications.Wpf;

namespace MVVM_Refregator
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {

        private System.Threading.Mutex mutex = new System.Threading.Mutex(false, "FoodCalendar");

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

        private async void Application_StartupAsync(object sender, StartupEventArgs e)
        {
            if (!mutex.WaitOne(0, false))
            {
                MessageBox.Show("食材管理アプリは既に起動しています。", "二重起動防止", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                mutex.Close();
                mutex = null;
                this.Shutdown();
            }
            // food
            var folder = ApplicationData.Current.LocalFolder;
            var foodDataPath = System.IO.Path.Combine(folder.Path, "food_data.json");
            if (File.Exists(foodDataPath) == false)
            {
                StorageFile result = await folder.CreateFileAsync("food_data.json");
            }
            //var folder = System.Windows.Storage.ApplicationData.Current.LocalFolder;

            // 食材データを読み込む(既定値はfood_composition.json)
            var ins = FoodShelfModel.GetInstance();
            var isLoadSuccess = await ins.LoadAsync();
            if (isLoadSuccess == false)
            {
                Application.Current.Shutdown();
            }

            // 食品成分表の読み込みを行う(既定値food_composition_japanese.json)
            string hoge = MVVM_Refregator.Properties.Resources.food_composition_japanese;
            //Assembly assembly = Assembly.GetExecutingAssembly();
            //assembly.GetManifestResourceStream("MVVM_Refregator.App.food_composition_japanese.json");
            //AnalysisPageModel.GetInstance().LoadFoodComposition();
            AnalysisPageModel.GetInstance().LoadFoodComposition((string x) => x, hoge);

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

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (mutex != null)
            {
                mutex.ReleaseMutex();
                mutex.Close();
            }
        }
    }
}
