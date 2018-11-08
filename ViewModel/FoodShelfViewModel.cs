using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MVVM_Refregator.Model;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MVVM_Refregator.ViewModel
{
    public class FoodShelfViewModel : BindableBase, IDisposable
    {

        private FoodShelfModel _foodShelfModel;

        public ReadOnlyReactiveCollection<FoodModel> Foods { get; }
        //public ReactiveProperty<ObservableCollection<Food>> Foods { get; }

        public ReactiveProperty<DateTime> SelectedDate { get; private set; }

        public ReactiveProperty<bool> Checked { get; } = new ReactiveProperty<bool>(false);

        public ObservableCollection<FoodModel> SelectedFood { get; private set; } = new ObservableCollection<FoodModel>();

        //public ReactiveProperty<Dictionary<DateTime, ObservableCollection<FoodModel>>> DateFoodsMap { get; } = new ReactiveProperty<Dictionary<DateTime, ObservableCollection<FoodModel>>>(new Dictionary<DateTime, ObservableCollection<FoodModel>>());

        public ReactiveCommand Send_ShowAllFood { get; } = new ReactiveCommand();

        public ReactiveCommand Send_BindingFoods { get; } = new ReactiveCommand();

        public ReactiveCommand Send_AddFood { get; } = new ReactiveCommand();

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        ///// <summary>
        ///// ctor
        ///// </summary>
        //public FoodShelfViewModel()
        //{
            //var model = new FoodShelfModel();
            //model.Create("first", DateTime.Now.AddDays(1), DateTime.Now, FoodType.Other, new BitmapImage(new Uri("/Resources/information_image.png", UriKind.Relative)));
            //model.Create("second", DateTime.Now.AddDays(8), DateTime.Now, FoodType.Other, new BitmapImage(new Uri("/Resources/information_image.png", UriKind.Relative)));
            //this.Foods = model.FoodCollection.ToReadOnlyReactiveCollection(m => m).AddTo(this.Disposable); ;
            //this._foodShelfModel = model;

            //InitProperty();
        //}


        /// <summary>
        /// modelを渡したときのctor
        /// </summary>
        /// <param name="model"></param>
        //public FoodShelfViewModel(FoodShelfModel model)
        public FoodShelfViewModel()
        {
            //this._foodShelfModel = model;
            this._foodShelfModel = FoodShelfModel.GetInstance();
            // 変更前(このコードを有効にすると最新の変更が反映されず、1つ前の変更内容が表示される)
            // FoodCollectionの変更を現行スレッドで即時反映させる
            this.Foods = this._foodShelfModel.FoodCollection.ToReadOnlyReactiveCollection(_foodShelfModel.FoodCollection.ToCollectionChanged(), System.Reactive.Concurrency.Scheduler.CurrentThread).AddTo(this.Disposable);
            // CollectionChanged時にPropertyChangedを強制的に呼び出す
            _foodShelfModel.FoodCollection.CollectionChangedAsObservable().Subscribe(x => RaisePropertyChanged(nameof(Foods)));
            this.InitProperty();
        }

        private void InitProperty()
        {
            this.SelectedDate = new ReactiveProperty<DateTime>(DateTime.Today).AddTo(this.Disposable);
            SelectedDate.Subscribe((a) =>
            {
                this.SelectedFood.Clear();
                var selectFoods = this.Foods.Where(x => x.LimitDate.Date == a.Date);
                //var selectFoods = this.Foods.Value.Where(x => x.LimitDate.Date == a.Date);
                foreach (var food in selectFoods)
                {
                    this.SelectedFood.Add(food);
                }
            }).AddTo(this.Disposable);

            this.Send_ShowAllFood.Subscribe(x =>
            {
                this.SelectedFood.Clear();
                if (this.Checked.Value == false) return;
                foreach (var food in this.Foods)
                //foreach (var food in this.Foods.Value)
                {
                    this.SelectedFood.Add(food);
                }
            }).AddTo(this.Disposable);

            this.Send_BindingFoods.Subscribe((x) =>
            {
                var tes = ((DateTime)((ItemsControl)x).DataContext);
                var selected = this.Foods.Where(f => f.LimitDate.Date == tes.Date);
                ((ItemsControl)x).ItemsSource = selected;
            }).AddTo(this.Disposable);

            this.Send_AddFood.Subscribe((x) =>
            {
                var ram = new Random();
                this._foodShelfModel.Create("second", DateTime.Now.AddDays(ram.Next(1,8)), DateTime.Now, FoodType.Other, new BitmapImage(new Uri("/Resources/information_image.png", UriKind.Relative)));
                System.Diagnostics.Debug.WriteLine($" create food.  foodName : {this._foodShelfModel.FoodCollection[0].Name}");
            });
        }

        public void Dispose()
        {
            this.Disposable.Dispose();
        }
    }
}
