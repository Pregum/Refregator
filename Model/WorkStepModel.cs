using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reactive.Bindings;

namespace MVVM_Refregator.Model
{
    public class WorkStepModel : BindableBase
    {
        private ObservableCollection<IStep> _registerSteps = new ObservableCollection<IStep>();

        public ObservableCollection<IStep> RegisterSteps
        {
            get { return this._registerSteps; }
            set { this.SetProperty(ref _registerSteps, value); }
        }

        public FoodModel ManipulateFood => _manipulateFood;
        private static WorkStepModel _instance = null;
        private FoodModel _manipulateFood = new FoodModel();

        public static WorkStepModel GetInstance()
        {
            _instance = _instance ?? new WorkStepModel();
            return _instance;
        }

        /// <summary>
        /// ctor
        /// </summary>
        private WorkStepModel()
        {
            //this.RegisterSteps.Add(new FoodNameEditStep());
            //this.RegisterSteps.Add(new FoodBoughtDateEditStep());
            //this.RegisterSteps.Add(new FoodLimitDateEditStep());
            //this.RegisterSteps.Add(new FoodConfirmStep());

            this.RegisterSteps.Add(FoodNameEditStep.GetInstance());
            this.RegisterSteps.Add(FoodBoughtDateEditStep.GetInstance());
            this.RegisterSteps.Add(FoodLimitDateEditStep.GetInstance());
            this.RegisterSteps.Add(FoodConfirmStep.GetInstance());
        }

        /// <summary>
        /// 操作している食材オブジェクトの初期化
        /// </summary>
        public void InitializeFood()
        {
            this._manipulateFood = new FoodModel();
        }

        /// <summary>
        /// DI時のctor
        /// </summary>
        /// <param name="manipulateFood">操作されるFood</param>
        public WorkStepModel(FoodModel manipulateFood)
        {

        }
    }
}
