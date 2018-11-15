using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media.Imaging;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Reactive.Bindings.Interactivity;

using MVVM_Refregator.Model;
using System.Reactive.Disposables;

namespace MVVM_Refregator.ViewModel
{
    public class FoodViewModel : BindableBase, IDisposable
    {
        public ReactiveProperty<uint> Id { get; } 
        public ReactiveProperty<string> Name { get; }
        public ReactiveProperty<DateTime> LimitDate { get; }
        public ReactiveProperty<DateTime> BoughtDate { get; }
        public ReactiveProperty<FoodType> KindType { get; }
        public ReactiveProperty<FoodStatusType> StatusType { get; }
        public ReactiveProperty<BitmapImage> Image { get; }

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        public FoodViewModel(FoodModel food)
        {
            this.Id = food.ObserveProperty(x => x.Id).ToReactiveProperty().AddTo(this.Disposable);
            this.Name = food.ObserveProperty(x => x.Name).ToReactiveProperty().AddTo(this.Disposable);
            this.LimitDate = food.ObserveProperty(x => x.LimitDate).ToReactiveProperty().AddTo(this.Disposable);
            this.BoughtDate = food.ObserveProperty(x => x.BoughtDate).ToReactiveProperty().AddTo(this.Disposable);
            this.KindType = food.ObserveProperty(x => x.KindType).ToReactiveProperty().AddTo(this.Disposable);
            this.StatusType = food.ObserveProperty(x => x.StatusType).ToReactiveProperty().AddTo(this.Disposable);
            this.Image = food.ObserveProperty(x => x.Image).ToReactiveProperty().AddTo(this.Disposable);
        }

        public FoodViewModel()
        {
        }

        public void Dispose()
        {
            this.Disposable.Dispose();
        }
    }
}
