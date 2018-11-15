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

        private WorkStepModel _workStepModel;

        public ReactiveProperty<FoodModel> Food { get; }

        public StepOfFoodBoughtDateViewModel()
        {
            this._workStepModel = WorkStepModel.GetInstance();

            this.Food = this._workStepModel.ToReactivePropertyAsSynchronized(
                m => m.ManipulateFood,
                convert => convert,
                cb => cb,
                ReactivePropertyMode.DistinctUntilChanged | ReactivePropertyMode.RaiseLatestValueOnSubscribe,
                ignoreValidationErrorValue: false);
        }
    }
}
