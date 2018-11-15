using Prism.Mvvm;

using Reactive.Bindings;

using MVVM_Refregator.Model;

namespace MVVM_Refregator.ViewModel
{
    public class StepOfFoodConfirmViewModel : BindableBase
    {
        WorkStepModel _workStepModel;

        public ReactiveProperty<FoodModel> ManipulateFoodModel { get; }

        public StepOfFoodConfirmViewModel()
        {
            this._workStepModel = WorkStepModel.GetInstance();

            this.ManipulateFoodModel = new ReactiveProperty<FoodModel>(this._workStepModel.ManipulateFood);
        }
    }
}
