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
    /// <summary>
    /// 食材の名前を設定するクラス
    /// </summary>
    public class StepOfFoodNameViewModel : BindableBase
    {
        /// <summary>
        /// 一連の作業ステップ管理オブジェクト
        /// </summary>
        private WorkStepModel _workStepModel;

        /// <summary>
        /// 操作される食材
        /// </summary>
        public ReactiveProperty<FoodModel> Food { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public StepOfFoodNameViewModel()
        {
            this._workStepModel = WorkStepModel.GetInstance();

            this.Food = this._workStepModel.ToReactivePropertyAsSynchronized(
                x => x.ManipulateFood,
                convert => convert,
                convertBack => convertBack,
                ReactivePropertyMode.RaiseLatestValueOnSubscribe | ReactivePropertyMode.DistinctUntilChanged,
                false);
        }
    }
}
