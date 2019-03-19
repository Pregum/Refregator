using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Navigation;

namespace MVVM_Refregator.Model
{
    /// <summary>
    /// 登録作業・変更作業を管理するクラス
    /// </summary>
    public class WorkStepModel : BindableBase
    {
        /// <summary>
        /// 現在のステップのインデックス
        /// </summary>
        private int _currentStepIndex = 0;

        /// <summary>
        /// 現在の作業の種類
        /// </summary>
        private WorkType _currentWorkStepsType;

        /// <summary>
        /// 現在の作業の種類
        /// </summary>
        public WorkType CurrentWorkStepsType
        {
            get { return this._currentWorkStepsType; }
            private set { this.SetProperty(ref _currentWorkStepsType, value); }
        }

        /// <summary>
        /// 食材管理クラス
        /// </summary>
        private FoodShelfModel _foodShelfModel = FoodShelfModel.GetInstance();

        /// <summary>
        /// 現在の作業のステップコレクション
        /// </summary>
        private ObservableCollection<IStep> _currentWorkSteps;

        /// <summary>
        /// 現在の作業のステップコレクション
        /// </summary>
        public ObservableCollection<IStep> CurrentWorkSteps
        {
            get { return _currentWorkSteps; }
            private set { this.SetProperty(ref _currentWorkSteps, value); }
        }

        /// <summary>
        /// 登録作業のステップ
        /// </summary>
        private ObservableCollection<IStep> _registerSteps
            = new ObservableCollection<IStep>() {
                new FoodNameEditStep(),
                new FoodKindStep(),
                new FoodConfirmStep() };

        /// <summary>
        /// 変更作業のステップ
        /// </summary>
        private ObservableCollection<IStep> _updateSteps
            = new ObservableCollection<IStep>(){
                new FoodNameEditStep(),
                new FoodKindStep(),
                new FoodConfirmStep() };

        /// <summary>
        /// 削除作業のステップ
        /// </summary>
        private ObservableCollection<IStep> _deleteStep
            = new ObservableCollection<IStep>() {
                new FoodConfirmStep() };

        /// <summary>
        /// 使用済作業のステップ
        /// </summary>
        private ObservableCollection<IStep> _useStep
            = new ObservableCollection<IStep>
            { new FoodConfirmStep() };

        /// <summary>
        /// 現在の作業ステップ
        /// </summary>
        private IStep _currentStep;

        /// <summary>
        /// 一時保管用(変更時・削除時)
        /// </summary>
        private FoodModel _temporalFood;

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
        /// 今のステップが最初のステップか
        /// </summary>
        public bool IsFirstStep { get { return this._currentStep == this._currentWorkSteps.First(); } }

        /// <summary>
        /// 今のステップが最後のステップか
        /// </summary>
        public bool IsLastStep { get { return this._currentStep == this._currentWorkSteps.Last(); } }

        /// <summary>
        /// singleton
        /// </summary>
        private static WorkStepModel _instance = null;

        /// <summary>
        /// singleton
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
            this._currentWorkSteps = this._registerSteps;
            this.CurrentWorkStepsType = WorkType.StandBy;
        }

        /// <summary>
        /// 次のステップへ移ります
        /// </summary>
        public async System.Threading.Tasks.Task NextStepAsync(NavigationService navigation)
        {
            this._currentStep.Update();
            // 現在の作業が完了していなければ終了
            if (this._currentStep.StepStatus != StepStatusType.Done)
            {
                return;
            }

            // 全作業が完了した場合、食材を処理します
            if (this._currentWorkSteps.All(status => status.StepStatus == StepStatusType.Done) && this._currentWorkSteps.Last() == this._currentStep)
            {
                // 今の作業の種類によって行う動作を分ける(ここが大きくなったらStrategyパターンになる)
                switch (this._currentWorkStepsType)
                {
                    // 作成作業
                    case WorkType.Create:
                        while (this._foodShelfModel.FoodCollection.All(x => x.Id != this._manipulateFood.Id) == false)
                        {
                            FoodModel.IncrementId();
                            this._manipulateFood.Id = FoodModel.NextId;
                        }
                        await this._foodShelfModel.CreateAsync(this._manipulateFood);
                        break;
                    // 更新作業
                    case WorkType.Update:
                        var food = this._manipulateFood;
                        await this._foodShelfModel.UpdateAsync(this._temporalFood.Id, food.Name, food.LimitDate, food.UsedDate, food.KindType, food.Image, food.HasUsed); 
                        break;
                    // 削除作業
                    case WorkType.Delete:
                        await this._foodShelfModel.DeleteAsync(this._manipulateFood.Id);
                        break;
                    case WorkType.Use:
                        await this._foodShelfModel.SetUsedAsync(this._manipulateFood);
                        break;
                        // 未定義または読込作業またはデフォルト
                    case WorkType.StandBy:
                    case WorkType.None:
                    default:
                        break;
                }
                SetStandBy();
            }
            else
            {
                // 現在のステップが最後でなければ、次のステップへ進む
                this._currentStepIndex = this.IsLastStep ? this._currentStepIndex : this._currentStepIndex + 1;
                this._currentStep = this._currentWorkSteps[this._currentStepIndex];
                this._currentStep.Navigate(navigation);
            }

            this.RaisePropertyChanged(nameof(this.IsFirstStep));
            this.RaisePropertyChanged(nameof(this.IsLastStep));
        }

        /// <summary>
        /// スタンバイモードに移行する
        /// </summary>
        public void SetStandBy()
        {
            this.CurrentWorkSteps = this._registerSteps;
            this._currentStepIndex = 0;
            this._currentStep = this._currentWorkSteps[this._currentStepIndex];
            this.CurrentWorkStepsType = WorkType.StandBy;
            this._temporalFood = new FoodModel();

            this.RaisePropertyChanged(nameof(this.IsFirstStep));
            this.RaisePropertyChanged(nameof(this.IsLastStep));
        }

        /// <summary>
        /// 前のステップへ戻ります
        /// </summary>
        /// <param name="navigation">遷移元のフレームの</param>
        public void PrevStep(NavigationService navigation)
        {
            // 現作業ステップが2番目以降の場合、変更を戻し一つ前の作業に戻る
            if (this._currentStepIndex > 0)
            {
                this._currentStep.Revert();

                this._currentStepIndex--;
                this._currentStep = this._currentWorkSteps[this._currentStepIndex];
                this._currentStep.Navigate(navigation);
            }
            // 最初のステップならば待機中に変更
            else if (this._currentStepIndex == 0)
            {
                this.CurrentWorkStepsType = WorkType.StandBy;
            }

            // IsLastStepプロパティの通知
            this.RaisePropertyChanged(nameof(this.IsLastStep));
        }

        /// <summary>
        /// 食材の追加作業に移ります
        /// </summary>
        /// <param name="navigation">画面遷移用Service</param>
        public void NavigateAddWork(NavigationService navigation)
        {
            this.ManipulateFood = new FoodModel();
            this.CurrentWorkSteps = this._registerSteps;
            foreach (IStep aStep in this.CurrentWorkSteps)
            {
                aStep.Init();
            }

            this._currentStepIndex = 0;
            this._currentStep = this.CurrentWorkSteps.First();
            this._currentStep.Navigate(navigation);
            this.CurrentWorkStepsType = WorkType.Create;

            this.RaisePropertyChanged(nameof(this.IsLastStep));
        }

        /// <summary>
        /// 食材の更新作業に移ります
        /// </summary>
        /// <param name="updateFood">更新される食材</param>
        /// <param name="navigation">画面遷移用Service</param>
        public void NavigateUpdateWork(FoodModel updateFood, NavigationService navigation)
        {
            this._temporalFood = updateFood;
            this.ManipulateFood = new FoodModel(this._temporalFood);
            this.CurrentWorkSteps = this._updateSteps;
            foreach (IStep aStep in this.CurrentWorkSteps)
            {
                aStep.Init();
            }

            this._currentStepIndex = 0;
            this.CurrentWorkStepsType = WorkType.Update;
            this._currentStep = this._currentWorkSteps.First();
            this._currentStep.Navigate(navigation);

            this.RaisePropertyChanged(nameof(this.IsLastStep));
        }

        /// <summary>
        /// 食材の削除作業に移ります
        /// </summary>
        /// <param name="deleteFood">削除される食材</param>
        /// <param name="navigation">画面遷移用Service</param>
        public void NavigateDeleteWork(FoodModel deleteFood, NavigationService navigation)
        {
            this.ManipulateFood = deleteFood;
            this.CurrentWorkSteps = this._deleteStep;
            foreach (IStep aStep in this.CurrentWorkSteps)
            {
                aStep.Init();
            }

            this._currentStepIndex = 0;
            this.CurrentWorkStepsType = WorkType.Delete;
            this._currentStep = this._currentWorkSteps.First();
            this._currentStep.Navigate(navigation);

            this.RaisePropertyChanged(nameof(this.IsFirstStep));
            this.RaisePropertyChanged(nameof(this.IsLastStep));
        }

        /// <summary>
        /// 食材の使用済み作業に移ります
        /// </summary>
        /// <param name="useFood"></param>
        /// <param name="navigation"></param>
        public void NavigateUseWork(FoodModel useFood, NavigationService navigation)
        {
            this.ManipulateFood = useFood;
            this.CurrentWorkSteps = this._useStep;
            foreach (IStep aStep in this.CurrentWorkSteps)
            {
                aStep.Init();
            }

            this._currentStepIndex = 0;
            this.CurrentWorkStepsType = WorkType.Use;
            this._currentStep = this._currentWorkSteps.First();
            this._currentStep.Navigate(navigation);

            this.RaisePropertyChanged(nameof(this.IsFirstStep));
            this.RaisePropertyChanged(nameof(this.IsLastStep));
        }

        /// <summary>
        /// 食品を使用済みに設定します
        /// </summary>
        /// <param name="targetFood">対象の食品</param>
        public async System.Threading.Tasks.Task SetUsedAsync(FoodModel targetFood)
        {
            await this._foodShelfModel.SetUsedAsync(targetFood);
        }
    }
}
