using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reactive.Bindings;

using MVVM_Refregator.ViewModel;
using System.Collections.ObjectModel;

namespace MVVM_Refregator.ViewModel
{
    public class StartupViewModel
    {
        public ReactiveProperty<Uri> SeletedContentPage { get; }

        public ObservableCollection<Uri> ContentPages { get; }

        public FoodShelfViewModel FoodShelfViewModel;

        public StartupViewModel()
        {
            this.FoodShelfViewModel = new FoodShelfViewModel();

            this.ContentPages = new ObservableCollection<Uri>() { new Uri("/View/FoodCalendarPage.xaml", UriKind.Relative) };
            this.SeletedContentPage = new ReactiveProperty<Uri>(this.ContentPages[0]);
        }
    }
}
