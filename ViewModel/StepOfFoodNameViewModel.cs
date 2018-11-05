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
    public class StepOfFoodNameViewModel : BindableBase
    {
        private FoodNameEditStep _foodNameEditStepModel;

        //private WorkStepModel _workStepModel;
        public ReactiveProperty<string> FoodName { get; }

        public StepOfFoodNameViewModel()
        {
            this._foodNameEditStepModel = FoodNameEditStep.GetInstance();
            //this._workStepModel = WorkStepModel.GetInstance();

            //this.FoodName = new ReactiveProperty<string>(this._workStepModel._manipulateFood.Name);
            //this.FoodName = new ReactiveProperty<string>(this._foodNameEditStepModel.FoodName);
            this.FoodName = this._foodNameEditStepModel.ToReactivePropertyAsSynchronized(m => m.FoodName,
                convert => convert,
                convertBack => convertBack,
                ReactivePropertyMode.DistinctUntilChanged | ReactivePropertyMode.RaiseLatestValueOnSubscribe,
                false);

            //this.FoodName = this._model.ObserveProperty(x => x.FoodName).ToReactiveProperty();
            //this.FoodName = this._model.ToReactivePropertyAsSynchronized(
            //    x => x.FoodName, 
            //    convert => convert,
            //    convertBack => convertBack,
            //    ReactivePropertyMode.RaiseLatestValueOnSubscribe | ReactivePropertyMode.DistinctUntilChanged,
            //    false);
        }
    }
}
