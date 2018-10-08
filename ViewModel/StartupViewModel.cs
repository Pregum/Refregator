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

namespace MVVM_Refregator.ViewModel
{
    public class StartupViewModel
    {
        public ReactiveProperty<Uri> SelectedContentPage { get; }

        public ObservableCollection<Uri> ContentPages { get; }

        public FoodShelfViewModel FoodShelfViewModel;

        public ReactiveProperty<DisplayPageStatus> CheckedPage { get; } = new ReactiveProperty<DisplayPageStatus>(DisplayPageStatus.DashBoard);

        public ReactiveCommand NavigationViewModel { get; } = new ReactiveCommand();


        public StartupViewModel()
        {
            this.FoodShelfViewModel = new FoodShelfViewModel();

            this.ContentPages = new ObservableCollection<Uri>() {
                new Uri("/View/DashBoardPage.xaml", UriKind.Relative),
                new Uri("/View/FoodCalendarPage.xaml", UriKind.Relative),
                new Uri("/View/EditPage.xaml", UriKind.Relative)
            };

            this.SelectedContentPage = new ReactiveProperty<Uri>(this.ContentPages[0]);

            this.NavigationViewModel.Subscribe((dataContext) =>
            {
                if ((dataContext is NavigationService) == false) return;

                ((NavigationService)dataContext).Navigate(this.SelectedContentPage.Value);

                System.Diagnostics.Debug.WriteLine($"debug {this.SelectedContentPage.Value}に遷移しました.");
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
                    default:
                        break;
                }
            });
        }
    }
}
