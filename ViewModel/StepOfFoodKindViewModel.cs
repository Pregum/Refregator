using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Refregator.Model;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MVVM_Refregator.ViewModel
{
    public class StepOfFoodKindViewModel : BindableBase
    {
        private WorkStepModel _workstepModel;

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
