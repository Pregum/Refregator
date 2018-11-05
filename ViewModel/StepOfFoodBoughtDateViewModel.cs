using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MVVM_Refregator.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MVVM_Refregator.ViewModel
{
    public class StepOfFoodBoughtDateViewModel : BindableBase
    {
        private FoodBoughtDateEditStep _foodBoughtDateEditStepModel;

        public ReactiveProperty<DateTime> BoughtDate { get; }

        public StepOfFoodBoughtDateViewModel()
        {
            this._foodBoughtDateEditStepModel = FoodBoughtDateEditStep.GetInstance();

            this.BoughtDate = this._foodBoughtDateEditStepModel.ToReactivePropertyAsSynchronized(
                m => m.BoughtDate,
                convert => convert,
                convertBack: cb => cb,
                mode: ReactivePropertyMode.DistinctUntilChanged | ReactivePropertyMode.RaiseLatestValueOnSubscribe,
                ignoreValidationErrorValue: false);
        }
    }
}
