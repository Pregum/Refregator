using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reactive.Bindings;

using MVVM_Refregator.ViewModel;
using System.Collections.ObjectModel;
using MVVM_Refregator.Model;
using System.Windows.Controls;
using System.Windows.Navigation;

using MVVM_Refregator.Common;
using System.Reactive.Linq;

namespace MVVM_Refregator.ViewModel
{
    /// <summary>
    /// 最初
    /// </summary>
    public class StartupViewModel
    {
        /// <summary>
        /// 食材管理クラス
        /// </summary>
        private FoodShelfModel _model;

        public ReactiveProperty<Uri> SelectedContentPage { get; private set; }

        public ObservableCollection<Uri> ContentPages { get; private set; }

        public ReactiveProperty<DisplayPageStatus> CheckedPage { get; } = new ReactiveProperty<DisplayPageStatus>(DisplayPageStatus.DashBoard);

        public ReactiveCommand NavigationViewModel { get; } = new ReactiveCommand();

        public ReactiveCommand SaveJsonCommand { get; } = new ReactiveCommand();

        public ReactiveCommand LoadJsonCommand { get; } = new ReactiveCommand();


        public StartupViewModel()
        {
            this._model = FoodShelfModel.GetInstance();
            InitProperty();
        }

        private void InitProperty()
        {
            this.ContentPages = new ObservableCollection<Uri>() {
                new Uri("/View/DashBoardPage.xaml", UriKind.Relative),
                new Uri("/View/FoodCalendarPage.xaml", UriKind.Relative),
                new Uri("/View/EditPage.xaml", UriKind.Relative),
                new Uri("/View/AnalysisPage.xaml", UriKind.Relative),
                new Uri("/View/SettingPage.xaml", UriKind.Relative)
            };

            this.SelectedContentPage = new ReactiveProperty<Uri>(this.ContentPages[0]);

            this.NavigationViewModel
                .Where((dataContext) => dataContext is NavigationService)
                .Subscribe((dataContext) =>
                {
                    ((NavigationService)dataContext).Navigate(this.SelectedContentPage.Value);
                    System.Diagnostics.Debug.WriteLine($"Debug {this.SelectedContentPage.Value}に遷移しました.");
                });


            this.CheckedPage.Subscribe((checkedButton) =>
            {
                if ((checkedButton is DisplayPageStatus displayPage) == false)
                    this.SelectedContentPage.Value = this.ContentPages[0];

                switch (displayPage)
                {
                    case DisplayPageStatus.DashBoard:
                        this.SelectedContentPage.Value = this.ContentPages[0];
                        break;
                    case DisplayPageStatus.Calendar:
                        this.SelectedContentPage.Value = this.ContentPages[1];
                        break;
                    case DisplayPageStatus.Editor:
                        this.SelectedContentPage.Value = this.ContentPages[2];
                        break;
                    case DisplayPageStatus.NutrientInfo:
                        this.SelectedContentPage.Value = this.ContentPages[3];
                        break;
                    case DisplayPageStatus.Setting:
                        this.SelectedContentPage.Value = this.ContentPages[4];
                        break;
                    default:
                        break;
                }
            });

            this.SaveJsonCommand.Subscribe(() =>
            {
                var success = this._model.Save();
                System.Diagnostics.Debug.WriteLine($"SaveCommandが実行されました");
            });

            this.LoadJsonCommand.Subscribe(() =>
            {
                var result = this._model.Load();
                System.Diagnostics.Debug.WriteLine($"LoadCommandが実行されました");
            });
        }
    }
}
