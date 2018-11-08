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
        //private FoodBoughtDateEditStep _foodBoughtDateEditStepModel;

        private WorkStepModel _workStepModel;

        //public ReactiveProperty<DateTime> BoughtDate { get; }
        public ReactiveProperty<FoodModel> Food { get; }

        public StepOfFoodBoughtDateViewModel()
        {
            //this._foodBoughtDateEditStepModel = FoodBoughtDateEditStep.GetInstance();

            this._workStepModel = WorkStepModel.GetInstance();

            //this.BoughtDate = this._foodBoughtDateEditStepModel.ToReactivePropertyAsSynchronized(
            //    m => m.BoughtDate,
            //    convert => convert,
            //    convertBack: cb => cb,
            //    mode: ReactivePropertyMode.DistinctUntilChanged | ReactivePropertyMode.RaiseLatestValueOnSubscribe,
            //    ignoreValidationErrorValue: false);


            this.Food = this._workStepModel.ToReactivePropertyAsSynchronized(
                m => m.ManipulateFood,
                convert => convert,
                convertBack: cb => cb,
                mode: ReactivePropertyMode.DistinctUntilChanged | ReactivePropertyMode.RaiseLatestValueOnSubscribe,
                ignoreValidationErrorValue: false);
        }
    }
}
