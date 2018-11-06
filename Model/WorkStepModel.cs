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
    /// <summary>
    /// 登録作業・変更作業を管理するクラス
    /// </summary>
    public class WorkStepModel : BindableBase
    {
        /// <summary>
        /// 登録作業のステップ
        /// </summary>
        private ObservableCollection<IStep> _registerSteps = new ObservableCollection<IStep>();

        /// <summary>
        /// 登録作業のステップ
        /// </summary>
        public ObservableCollection<IStep> RegisterSteps
        {
            get { return this._registerSteps; }
            set { this.SetProperty(ref _registerSteps, value); }
        }

        /// <summary>
        /// 操作される食材
        /// </summary>
        private FoodModel _manipulateFood = new FoodModel();

        /// <summary>
        /// 操作される食材
        /// </summary>
        public FoodModel ManipulateFood
        {
            get { return _manipulateFood; }
            private set { this.SetProperty(ref _manipulateFood, value); }
        }

        /// <summary>
        /// シングルトンを実現するためのstatic変数
        /// </summary>
        private static WorkStepModel _instance = null;

        /// <summary>
        /// シングルトンを実現するためのstatic変数
        /// </summary>
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
            this.ManipulateFood = manipulateFood;
        }
    }
}
