using Prism.Mvvm;
using System.Linq;
using MVVM_Refregator.Model;
using System.Collections.ObjectModel;
using System;

namespace MVVM_Refregator.ViewModel
{
    public class DashBoardPageViewModel : BindableBase
    {
        private FoodShelfModel _foodShelfModel = FoodShelfModel.GetInstance();

        public ObservableCollection<FoodModel> UsestFoods { get; }

        public ObservableCollection<FoodModel> DangerousFoods { get; }

        public ObservableCollection<object> UsedCountFrequencyFoodList { get; }

        public ObservableCollection<object> FoodLimitDateList { get; }

        public ObservableCollection<object> FoodTypeCatalog { get; }

        public DashBoardPageViewModel()
        {
            this.UsedCountFrequencyFoodList = new ObservableCollection<object>(this._foodShelfModel
                .FoodCollection
                .Where(x => x.HasUsed)
                .GroupBy(x => x.KindType)
                .OrderByDescending(x => x.Count())
                .ThenBy(x => x.First().KindType)
                .Take(3)
                .Select(x => x.First())
                .Select((x, i) => new { x, i = (i + 1) }));

            this.FoodLimitDateList = new ObservableCollection<object>(this._foodShelfModel
                .FoodCollection
                .Where(x => !x.HasUsed && x.LimitDate.Date >= DateTime.Today.Date)
                .OrderBy(x => x.LimitDate)
                .Select((x, i) => new { x, i = (i + 1) }));

            var foodTypes = Enum.GetValues(typeof(FoodType)).Cast<FoodType>();
            var usedFoods = this._foodShelfModel
                .FoodCollection
                .Where(x => x.HasUsed)
                .GroupBy(x => x.KindType);
            //.OrderBy(x => x.First().KindType);

            //this.FoodTypeCatalog = new ObservableCollection<object>(
            //    foodTypes.Select(x =>
            //        new {
            //            FoodType = x,
            //            UsedTime = usedFoods.Count(y => y.First().KindType == x)
            //        }));

            //this.FoodTypeCatalog = new ObservableCollection<object>(
            //    foodTypes.Select(x =>
            //        new
            //        {
            //            FoodType = x,
            //            UsedTime = usedFoods.Where(y => y.First().KindType == x).Count()
            //        }));

            this.FoodTypeCatalog = new ObservableCollection<object>(
                foodTypes.GroupJoin(
                    usedFoods,
                    foodType => foodType,
                    usedFood => usedFood.Key,
                    (fType, usedType) =>
                    new
                    {
                        fType = fType,
                        usedType = usedType,
                    }).
                    SelectMany(
                    x => x.usedType.DefaultIfEmpty(),
                    (x, t) => new
                    {
                        FoodType = x.fType,
                        UsedTime = t?.Count() ?? 0
                    }));

        }
    }
}