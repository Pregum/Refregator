using MVVM_Refregator.Model;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MVVM_Refregator.ViewModel
{
    /// <summary>
    /// 食材の種類を設定するViewModel
    /// </summary>
    public class StepOfFoodKindViewModel : BindableBase
    {
        /// <summary>
        /// 一連のステップ管理オブジェクト
        /// </summary>
        private WorkStepModel _workstepModel;

        /// <summary>
        /// 操作される食材
        /// </summary>
        public ReactiveProperty<FoodModel> Food { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public StepOfFoodKindViewModel()
        {
            this._workstepModel = WorkStepModel.GetInstance();

            this.Food = this._workstepModel.ToReactivePropertyAsSynchronized(
                m => m.ManipulateFood,
                convert => convert,
                convertBack => convertBack,
                ReactivePropertyMode.DistinctUntilChanged | ReactivePropertyMode.RaiseLatestValueOnSubscribe,
                false);
        }
    }
}
