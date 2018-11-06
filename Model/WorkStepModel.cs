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

        private FoodModel _manipulateFood = new FoodModel();

        public FoodModel ManipulateFood { get => _manipulateFood; private set => this.SetProperty(ref _manipulateFood, value); }

        private static WorkStepModel _instance = null;

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
            this.RegisterSteps.Add(FoodNameEditStep.GetInstance());
            this.RegisterSteps.Add(FoodBoughtDateEditStep.GetInstance());
            this.RegisterSteps.Add(FoodLimitDateEditStep.GetInstance());
            this.RegisterSteps.Add(FoodConfirmStep.GetInstance());
        }

        /// <summary>
        /// 操作している食材オブジェクトの初期化
        /// </summary>
        public void Initialize()
        {
            //this._manipulateFood = new FoodModel();
            this.ManipulateFood = new FoodModel();
            foreach (var aStep in this.RegisterSteps)
            {
                aStep.Init();
            }
        }

        /// <summary>
        /// 食材をセットします
        /// </summary>
        /// <param name="manipulateFood"></param>
        public void SetFood(FoodModel manipulateFood)
        {
            //this._manipulateFood = manipulateFood;
            this.ManipulateFood = manipulateFood;
        }
    }
}
