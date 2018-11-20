using System;
using MVVM_Refregator.Model;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

using System.Linq;
using System.Collections.ObjectModel;

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

        public ObservableCollection<FoodType> FoodKinds { get; }

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

            var ttt = Enum.GetValues(typeof(FoodType));
            var list = new ObservableCollection<FoodType>();
            foreach (FoodType foodType in ttt)
            {
                list.Add(foodType);
            }

            this.FoodKinds = list;

        }


    }
}
