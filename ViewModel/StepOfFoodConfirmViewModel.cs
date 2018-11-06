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
    public class StepOfFoodConfirmViewModel : BindableBase
    {
        FoodConfirmStep _confirmModel;

        WorkStepModel _workStepModel;

        public ReactiveProperty<FoodModel> ManipulateFoodModel { get; }

        public StepOfFoodConfirmViewModel()
        {
            this._confirmModel = FoodConfirmStep.GetInstance();
            this._workStepModel = WorkStepModel.GetInstance();

            this.ManipulateFoodModel =
                new ReactiveProperty<FoodModel>(this._workStepModel.ManipulateFood);
        }
    }
}
