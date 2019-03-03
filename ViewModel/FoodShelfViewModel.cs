using System;
using System.Reactive.Disposables;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using MVVM_Refregator.Model;
using System.Linq;
using System.Collections.ObjectModel;

namespace MVVM_Refregator.ViewModel
{
    /// <summary>
    /// 食材管理を行うViewModel
    /// </summary>
    public class FoodShelfViewModel : BindableBase, IDisposable
    {
        /// <summary>
        /// 食材管理を行うオブジェクト
        /// </summary>
        private FoodShelfModel _foodShelfModel;

        /// <summary>
        /// 保持している食材コレクション
        /// </summary>
        public ReadOnlyReactiveCollection<FoodModel> Foods { get; }

        public ObservableCollection<FoodModel> FutureFoods { get; }

        /// <summary>
        /// Disposeを行うオブジェクト
        /// </summary>
        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        //public ReactiveCommand Send_AddFood { get; }

        /// <summary>
        /// modelを渡したときのctor
        /// </summary>
        public FoodShelfViewModel()
        {
            this._foodShelfModel = FoodShelfModel.GetInstance();
            // FoodCollectionの変更を現行スレッドで即時反映させる
            this.Foods = this._foodShelfModel.FoodCollection
                .ToReadOnlyReactiveCollection(_foodShelfModel.FoodCollection.ToCollectionChanged(), System.Reactive.Concurrency.Scheduler.CurrentThread)
                .AddTo(this.Disposable);
            // CollectionChanged時にPropertyChangedを強制的に呼び出す
            _foodShelfModel.FoodCollection.CollectionChangedAsObservable().Subscribe(x => RaisePropertyChanged(nameof(Foods)));

            //this.FutureFoods = new ObservableCollection<FoodModel>(this.Foods.Where(x => x.LimitDate.Date >= DateTime.Today.Date && !x.HasUsed));
            this.FutureFoods = new ObservableCollection<FoodModel>(this.Foods.Where(x => !x.HasUsed));



            // デバッグ用
            //this.Send_AddFood.Subscribe((x) =>
            //{
            //    var ram = new Random();
            //    this._foodShelfModel.Create("second", DateTime.Now.AddDays(ram.Next(1,8)), DateTime.Now, FoodType.Other, new BitmapImage(new Uri("/Resources/information_image.png", UriKind.Relative)));
            //    System.Diagnostics.Debug.WriteLine($" create food.  foodName : {this._foodShelfModel.FoodCollection[0].Name}");
            //});
        }


        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            this.Disposable.Dispose();
        }
    }
}
