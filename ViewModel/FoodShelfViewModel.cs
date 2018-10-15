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

        private FoodShelf Model;

        public ReadOnlyReactiveCollection<Food> Foods { get; }
        //public ReactiveProperty<ObservableCollection<Food>> Foods { get; }

        public ReactiveProperty<DateTime> SelectedDate { get; private set; }

        public ReactiveProperty<bool> Checked { get; } = new ReactiveProperty<bool>(false);

        public ObservableCollection<Food> SelectedFood { get; private set; } = new ObservableCollection<Food>();

        public ReactiveProperty<Dictionary<DateTime, ObservableCollection<Food>>> DateFoodsMap { get; } = new ReactiveProperty<Dictionary<DateTime, ObservableCollection<Food>>>(new Dictionary<DateTime, ObservableCollection<Food>>());

        public ReactiveCommand Send_ShowAllFood { get; } = new ReactiveCommand();

        public ReactiveCommand Send_BindingFoods { get; } = new ReactiveCommand();

        public ReactiveCommand Send_AddFood { get; } = new ReactiveCommand();

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        /// <summary>
        /// ctor
        /// </summary>
        public FoodShelfViewModel()
        {
            var model = new FoodShelf();
            model.Create("first", DateTime.Now.AddDays(1), DateTime.Now, FoodType.Other, new BitmapImage(new Uri("/Resources/information_image.png", UriKind.Relative)));
            model.Create("second", DateTime.Now.AddDays(8), DateTime.Now, FoodType.Other, new BitmapImage(new Uri("/Resources/information_image.png", UriKind.Relative)));
            this.Foods = model.FoodCollection.ToReadOnlyReactiveCollection(m => m).AddTo(this.Disposable); ;
            this.Model = model;

            InitProperty();
        }


        /// <summary>
        /// modelを渡したときのctor
        /// </summary>
        /// <param name="model"></param>
        public FoodShelfViewModel(FoodShelf model)
        {
            this.Model = model;
            // 変更前(このコードを有効にすると最新の変更が反映されず、1つ前の変更内容が表示される)
            //this.Foods = this.Model.FoodCollection.ToReadOnlyReactiveCollection(m => m).AddTo(this.Disposable);
            // FoodCollectionの変更を現行スレッドで即時反映させる
            this.Foods = this.Model.FoodCollection.ToReadOnlyReactiveCollection(Model.FoodCollection.ToCollectionChanged(), System.Reactive.Concurrency.Scheduler.CurrentThread).AddTo(this.Disposable);
            // CollectionChanged時にPropertyChangedを強制的に呼び出す
            Model.FoodCollection.CollectionChangedAsObservable().Subscribe(x => RaisePropertyChanged("Foods"));
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
                this.Model.Create("second", DateTime.Now.AddDays(ram.Next(1,8)), DateTime.Now, FoodType.Other, new BitmapImage(new Uri("/Resources/information_image.png", UriKind.Relative)));
                System.Diagnostics.Debug.WriteLine($" create food.  foodName : {this.Model.FoodCollection[0].Name}");
            });
        }

        /// <summary>
        /// コレクションを賞味期限ごとのコレクションにまとめます。
        /// </summary>
        private void BuildFoodMap()
        {
            this.DateFoodsMap.Value.Clear();
            //var crastaData = this.Foods.Value.GroupBy(x => x.LimitDate);
            var crastaData = this.Foods.GroupBy(x => x.LimitDate);
            foreach (var dayFoods in crastaData)
            {
                var currentDate = dayFoods.First().LimitDate.Date;
                if (this.DateFoodsMap.Value.ContainsKey(currentDate) == false)
                {
                    this.DateFoodsMap.Value.Add(currentDate, new ObservableCollection<Food>());
                }
                foreach (var dayFood in dayFoods)
                {
                    this.DateFoodsMap.Value[currentDate].Add(dayFood);
                }
            }
        }

        public void Dispose()
        {
            this.Disposable.Dispose();
        }
    }
}
