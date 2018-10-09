using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class FoodShelfViewModel : BindableBase
    {

        private FoodShelf Model; 

        public ReadOnlyReactiveCollection<Food> Foods { get; }

        public ReactiveProperty<DateTime> SelectedDate { get; }

        public ReactiveProperty<bool> Checked { get; } = new ReactiveProperty<bool>(false);

        public ReactiveCommand Send_ShowAllFood { get; } = new ReactiveCommand();

        public ObservableCollection<Food> SelectedFood { get; private set; } = new ObservableCollection<Food>();

        public ReactiveCommand Send_BindingFoods { get; } = new ReactiveCommand();


        /// <summary>
        /// ctor
        /// </summary>
        public FoodShelfViewModel()
        {
            var model = new FoodShelf();
            model.Create("first", DateTime.Now.AddDays(1), DateTime.Now, FoodType.Other, new BitmapImage(new Uri("/Resources/information_image.png", UriKind.Relative)));
            model.Create("second", DateTime.Now.AddDays(8), DateTime.Now, FoodType.Other, new BitmapImage(new Uri("/Resources/information_image.png", UriKind.Relative)));
            this.Foods = model.FoodCollection.ToReadOnlyReactiveCollection(m => m);
            this.Model = model;

            this.SelectedDate = new ReactiveProperty<DateTime>(DateTime.Today);
            SelectedDate.Subscribe((a) =>
            {
                this.SelectedFood.Clear();
                var selectFoods = this.Foods.Where(x => x.LimitDate.Date == a.Date);
                foreach (var food in selectFoods)
                {
                    this.SelectedFood.Add(food);
                }
            });


            this.Send_ShowAllFood.Subscribe(x =>
            {
                this.SelectedFood.Clear();
                if (this.Checked.Value == false) return;
                foreach (var food in this.Foods)
                {
                    this.SelectedFood.Add(food);
                }
            });

            this.Send_BindingFoods.Subscribe((x) =>
            {
                var tes = ((DateTime)((ItemsControl)x).DataContext);
                var selected = this.Foods.Where(f => f.LimitDate.Date == tes.Date);
                ((ItemsControl)x).ItemsSource = selected;
            });


        }

        /// <summary>
        /// modelを渡したときのctor
        /// </summary>
        /// <param name="model"></param>
        public FoodShelfViewModel(FoodShelf model)
        {
            this.Model = model;
            this.Foods = this.Model.FoodCollection.ToReadOnlyReactiveCollection(m => m);

            this.SelectedDate = new ReactiveProperty<DateTime>(DateTime.Today);
            SelectedDate.Subscribe((a) =>
            {
                this.SelectedFood.Clear();
                var selectedFoods = this.Foods.Where(x => x.LimitDate.Date == a.Date);
                foreach (var food in selectedFoods)
                {
                    this.SelectedFood.Add(food);
                }
            });

            this.Send_ShowAllFood.Subscribe(x =>
            {
                this.SelectedFood.Clear();
                if (this.Checked.Value == false) return;
                foreach (var food in this.Foods)
                {
                    this.SelectedFood.Add(food);
                }
            });

            this.Send_BindingFoods.Subscribe((x) =>
            {
                var tes = ((DateTime)((ItemsControl)x).DataContext);
                var selected = this.Foods.Where(f => f.LimitDate.Date == tes.Date);
                ((ItemsControl)x).ItemsSource = selected;
            });
        }
    }
}
