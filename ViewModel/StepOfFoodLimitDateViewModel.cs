using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

using MVVM_Refregator.Model;

namespace MVVM_Refregator.ViewModel
{
    public class StepOfFoodLimitDateViewModel : BindableBase
    {
        private FoodLimitDateEditStep _foodLimitDateEditStep;

        public ReactiveProperty<DateTime> LimitDate { get; }

        public StepOfFoodLimitDateViewModel()
        {
            this._foodLimitDateEditStep = FoodLimitDateEditStep.GetInstance();

            this.LimitDate = this._foodLimitDateEditStep.ToReactivePropertyAsSynchronized(
                m => m.LimitDate,
                convert => convert,
                convertBack => convertBack,
                ReactivePropertyMode.DistinctUntilChanged | ReactivePropertyMode.RaiseLatestValueOnSubscribe,
                false);
        }
    }
}
