using Prism.Mvvm;
using System;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

using MVVM_Refregator.Model;

namespace MVVM_Refregator.ViewModel
{
    /// <summary>
    /// 食材の賞味期限を設定するViewModel
    /// </summary>
    public class StepOfFoodLimitDateViewModel : BindableBase
    {
        /// <summary>
        /// 一連のステップ管理オブジェクト
        /// </summary>
        private WorkStepModel _workStepModel;

        /// <summary>
        /// 操作される食材
        /// </summary>
        public ReactiveProperty<FoodModel> Food { get; }

        /// <summary>
        /// 選択可能な最初の日付
        /// </summary>
        public ReactiveProperty<DateTime> StartDate { get; } = new ReactiveProperty<DateTime>(DateTime.Now.Date);

        /// <summary>
        /// ctor
        /// </summary>
        public StepOfFoodLimitDateViewModel()
        {
            this._workStepModel = WorkStepModel.GetInstance();

            this.Food = this._workStepModel.ToReactivePropertyAsSynchronized(
                m => m.ManipulateFood,
                convert => convert,
                convertBack => convertBack,
                ReactivePropertyMode.DistinctUntilChanged | ReactivePropertyMode.RaiseLatestValueOnSubscribe,
                false);
        }
    }
}
