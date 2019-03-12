using System;
using Prism.Mvvm;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Reactive.Bindings.ObjectExtensions;

using MVVM_Refregator.Model;
using System.Reactive.Linq;

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

        public ReactiveProperty<string> CurrentWorkType { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public StepOfFoodConfirmViewModel()
        {
            this._workStepModel = WorkStepModel.GetInstance();

            this.ManipulateFoodModel = new ReactiveProperty<FoodModel>(this._workStepModel.ManipulateFood);

            this.CurrentWorkType = this._workStepModel.ObserveProperty(x => x.CurrentWorkStepsType).Select(x =>
            {
                switch (x)
                {
                    case WorkType.Create:
                        return "登録";
                    case WorkType.Update:
                        return "変更";
                    case WorkType.Delete:
                        return "削除";
                    case WorkType.Use:
                        return "使用済";
                    case WorkType.None:
                    case WorkType.StandBy:
                    default:
                        //throw new InvalidOperationException("現在の作業では呼び出せません。");
                        return "----";
                }
            }).ToReactiveProperty();
        }
    }
}
