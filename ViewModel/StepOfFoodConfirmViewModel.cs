using Prism.Mvvm;

using Reactive.Bindings;

using MVVM_Refregator.Model;

namespace MVVM_Refregator.ViewModel
{
    /// <summary>
    /// 作業の最終確認を行うViewModel
    /// </summary>
    public class StepOfFoodConfirmViewModel : BindableBase
    {
        /// <summary>
        /// 一連のステップ管理オブジェクト
        /// </summary>
        WorkStepModel _workStepModel;

        /// <summary>
        /// 操作される食材
        /// </summary>
        public ReactiveProperty<FoodModel> ManipulateFoodModel { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public StepOfFoodConfirmViewModel()
        {
            this._workStepModel = WorkStepModel.GetInstance();

            this.ManipulateFoodModel = new ReactiveProperty<FoodModel>(this._workStepModel.ManipulateFood);
        }
    }
}
