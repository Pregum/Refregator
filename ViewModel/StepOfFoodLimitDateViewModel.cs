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
    public class StepOfFoodLimitDateViewModel : BindableBase
    {
        private WorkStepModel _workStepModel;

        public ReactiveProperty<FoodModel> Food { get; }

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
