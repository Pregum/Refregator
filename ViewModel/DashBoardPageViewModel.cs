using Prism.Mvvm;
using System.Linq;
using MVVM_Refregator.Model;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Collections;
using LiveCharts.Wpf;
using LiveCharts;

namespace MVVM_Refregator.ViewModel
{
    public class DashBoardPageViewModel : BindableBase
    {
        private FoodShelfModel _foodShelfModel = FoodShelfModel.GetInstance();

        public ObservableCollection<FoodModel> UsestFoods { get; }

        public ObservableCollection<FoodModel> DangerousFoods { get; }

        public IEnumerable<object> Test { get; }

        public IEnumerable<object> Test_2 { get; }

        public SeriesCollection GraphSources { get; }

        public ObservableCollection<string> Labels { get; }

        public Func<string, string> Format { get; }


        public DashBoardPageViewModel()
        {
            //this.UsestFoods = new ObservableCollection<FoodModel>(this._foodShelfModel.FoodCollection.Where(x => x.HasUsed).GroupBy(x => x.KindType).OrderBy(x => x.Count()).Take(3).Select(x => x.First()));
            this.Test_2 = new ObservableCollection<object>(this._foodShelfModel.FoodCollection.Where(x => x.HasUsed).GroupBy(x => x.KindType).OrderBy(x => x.Count()).Take(3).Select(x => x.First()).Select((x, i) => new { x, i = (i + 1) }));

            //this.DangerousFoods = new ObservableCollection<FoodModel>(this._foodShelfModel.FoodCollection.Where(x => !x.HasUsed).OrderBy(x => x.LimitDate));
            //this.Test = new ObservableCollection<object>(this._foodShelfModel.FoodCollection.Where(x => !x.HasUsed).OrderBy(x => x.LimitDate)).Select((x, i) => new { x, i = (i + 1) });
            this.Test = new ObservableCollection<object>(this._foodShelfModel.FoodCollection.Where(x => !x.HasUsed && x.LimitDate.Date >= DateTime.Today.Date).OrderBy(x => x.LimitDate)).Select((x, i) => new { x, i = (i + 1) });

            var sources = this._foodShelfModel.FoodCollection.Where(x => x.HasUsed && x.UsedDate > DateTime.Today.AddMonths(-1)).GroupBy(x => x.UsedDate);
            var hoge = Enumerable.Range(1, (DateTime.Today - DateTime.Today.AddMonths(-1)).Days).Select(x => DateTime.Today.AddMonths(-1).AddDays(x))
                .GroupJoin(sources, (x) => x.Date, source => source.Key, (date, values) => new { Key = date, Num = values.FirstOrDefault(x => x.Key == date)?.Count() ?? 0});   // new { Key = x.Key, Num = x.Count() }));

            this.GraphSources = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<int>( hoge.Select(x => x.Num))
                }
            };

            this.Labels = new ObservableCollection<string>(hoge.Select(x => x.Key.ToString("yyyy/MM/dd")));

            this.Format = new Func<string, string>(x => x + "  hoge ");
            


            //this.Labels = new ObservableCollection<string>(Enumerable.Range(0,(DateTime.Today - DateTime.Today.AddMonths(-1)).Days).Select(x => DateTime.Today.AddMonths(-1).AddDays(x).ToString("yyyy/MM/dd")));
        }
    }
}