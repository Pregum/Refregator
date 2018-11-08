using MVVM_Refregator.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Disposables;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Windows.Navigation;
using System.Diagnostics;

namespace MVVM_Refregator.ViewModel
{
    /// <summary>
    /// 食材編集ページのViewModel
    /// </summary>
    public class EditPageViewModel : BindableBase, IDisposable
    {
        /// <summary>
        /// 食材の管理を行うモデル
        /// </summary>
        private FoodShelfModel _foodShelfModel;

        /// <summary>
        /// 一連の作業の管理を行うモデル
        /// </summary>
        private WorkStepModel _workStepModel;

        /// <summary>
        /// ある一連の作業のコレクション
        /// </summary>
        /// <example>登録作業ステップ</example>
        public ReadOnlyReactiveCollection<IStep> WorkSteps { get; private set; }

        ///// <summary>
        ///// 現在の作業ステップ
        ///// </summary>
        //private ReactiveProperty<IStep> _currentStep;

        /// <summary>
        /// 現在の作業ステップ
        /// </summary>
        //public ReactiveProperty<IStep> CurrentStep { get; private set; }

        ///// <summary>
        ///// 操作される食材
        ///// WorkStepModel内に移行させる予定
        ///// </summary>
        //public ReactiveProperty<FoodModel> ManipulateFood;

        /// <summary>
        /// 作業へ移行するためのコントロールのVisibility
        /// </summary>
        public ReactiveProperty<bool> ButtonVisibility { get; } = new ReactiveProperty<bool>(true);

        /// <summary>
        /// 作業用コントロールのVisibility
        /// </summary>
        public ReactiveProperty<bool> WorkLoadVisibility { get; } = new ReactiveProperty<bool>(false);

        /// <summary>
        /// 最後のステップか
        /// </summary>
        public ReactiveProperty<bool> IsLastStep { get; }

        /// <summary>
        /// 現在の作業の種類
        /// </summary>
        public ReactiveProperty<WorkType> CurrentWorkStepsType { get; }

        /// <summary>
        /// 管理されている食材
        /// </summary>
        public ReadOnlyReactiveCollection<FoodModel> Foods { get; }

        /// <summary>
        /// 選択されている食材
        /// </summary>
        public ReactiveProperty<FoodModel> SelectedFood { get; } = new ReactiveProperty<FoodModel>();

        /// <summary>
        /// 食材が選択されているか
        /// </summary>
        public ReactiveProperty<bool> IsSelectedFood { get; private set; } = new ReactiveProperty<bool>(false);

        /// <summary>
        /// 次へボタンのContent
        /// </summary>
        public ReactiveProperty<string> NextContent { get; } = new ReactiveProperty<string>("次へ");

        /// <summary>
        /// 前へボタンのContent
        /// </summary>
        public ReactiveProperty<string> PrevContent { get; } = new ReactiveProperty<string>("前へ");

        //private int _currentStepIndex = 0;

        /// <summary>
        /// 登録画面に遷移する際のコマンド
        /// </summary>
        public ReactiveCommand Send_NavigateRegister { get; } = new ReactiveCommand();

        /// <summary>
        /// 次のステップへ遷移する際のコマンド
        /// </summary>
        public ReactiveCommand Send_NextStep { get; } = new ReactiveCommand();

        /// <summary>
        /// 前のステップへ遷移する際のコマンド
        /// </summary>
        public ReactiveCommand Send_PrevStep { get; } = new ReactiveCommand();

        /// <summary>
        /// 食材の変更を行う際のコマンド
        /// </summary>
        public ReactiveCommand Send_ModifyFood { get; } = new ReactiveCommand();

        /// <summary>
        /// 食材の削除を行う際のコマンド
        /// </summary>
        public ReactiveCommand Send_RemoveFood { get; } = new ReactiveCommand();

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        ///// <summary>
        ///// ctor
        ///// </summary>
        //public EditPageViewModel()
        //{
        //}

        /// <summary>
        /// DI時のctor
        /// </summary>
        /// <param name="model"></param>
        //public EditPageViewModel(FoodShelfModel model)
        public EditPageViewModel()
        {
            // FoodShelfModel関係
            this._foodShelfModel = FoodShelfModel.GetInstance();
            this.Foods = this._foodShelfModel.FoodCollection.ToReadOnlyReactiveCollection(_foodShelfModel.FoodCollection.ToCollectionChanged(), System.Reactive.Concurrency.Scheduler.CurrentThread).AddTo(this.Disposable);
            this._foodShelfModel.FoodCollection.CollectionChangedAsObservable().Subscribe(x => RaisePropertyChanged(nameof(Foods)));

            // WorkStepModel関係
            this._workStepModel = WorkStepModel.GetInstance();
            this.WorkSteps = this._workStepModel.CurrentWorkSteps.ToReadOnlyReactiveCollection(
                this._workStepModel.CurrentWorkSteps.ToCollectionChanged(),
                System.Reactive.Concurrency.Scheduler.CurrentThread).AddTo(this.Disposable);
            this._workStepModel.CurrentWorkSteps.CollectionChangedAsObservable().Subscribe((x) => RaisePropertyChanged(nameof(this.WorkSteps)));

            // 食材選択プロパティ変更時
            this.SelectedFood.Subscribe((_) =>
            {
                this.IsSelectedFood.Value = this.SelectedFood?.Value != null;
            });

            // 次へボタンのコンテントの変更
            this.IsLastStep = this._workStepModel.ObserveProperty(x => x.IsLastStep).ToReactiveProperty();
            this.IsLastStep.Subscribe((isLastStep) =>
            {
                this.NextContent.Value = isLastStep ? "登録" : "次へ";
            });

            // 現在の作業の種類プロパティの購読
            this.CurrentWorkStepsType = this._workStepModel.ObserveProperty(m => m.CurrentWorkStepsType).ToReactiveProperty();
            this.CurrentWorkStepsType.Subscribe((currentWorkStepsType) =>
            {
                this.ButtonVisibility.Value = currentWorkStepsType == WorkType.StandBy;
                this.WorkLoadVisibility.Value = currentWorkStepsType != WorkType.StandBy;
            });

            // 登録ボタンのコマンド
            this.Send_NavigateRegister.Subscribe((x) =>
            {
                if (x is NavigationService navigation)
                {
                    //this.ButtonVisibility.Value = !this.ButtonVisibility.Value;
                    //this.WorkLoadVisibility.Value = !this.WorkLoadVisibility.Value;

                    this._workStepModel.NavigateAddWork(navigation);
                }
            });

            // 次へボタンのコマンド
            this.Send_NextStep.Subscribe((navigationService) =>
            {
                if (navigationService is NavigationService navigation)
                {
                    this._workStepModel.NextStep(navigation);
                    //if (this.WorkSteps.All(step => step.StepStatus == StepStatusType.Done))
                    //{
                    //    this.ButtonVisibility.Value = !this.ButtonVisibility.Value;
                    //    this.WorkLoadVisibility.Value = !this.WorkLoadVisibility.Value;
                    //}
                }
            });



            // 戻るボタンをクリック時のコマンド
            this.Send_PrevStep.Subscribe((namevigationService) =>
            {
                if (namevigationService is NavigationService navigation)
                {
                    this._workStepModel.PrevStep(navigation);
                    //this.NextContent.Value = this._workStepModel.IsLastStep ? "登録" : "次へ";
                    //if (this.WorkSteps.All(step => step.StepStatus == StepStatusType.New))
                    //{
                    //    this.ButtonVisibility.Value = !this.ButtonVisibility.Value;
                    //    this.WorkLoadVisibility.Value = !this.WorkLoadVisibility.Value;
                    //}
                }
            });

            // 削除時のコマンド
            this.Send_RemoveFood.Subscribe((x) =>
            {
                if (x is NavigationService navigation)
                {
                    this._workStepModel.NavigateDeleteWork(this.SelectedFood.Value, navigation);
                }
            });

            // 変更時のコマンド
            this.Send_ModifyFood.Subscribe((x) =>
            {
                if (x is NavigationService navigation)
                {
                    if (this.SelectedFood.Value != null)
                    {
                        //this.WorkLoadVisibility.Value = !this.WorkLoadVisibility.Value;
                        //this.ButtonVisibility.Value = !this.ButtonVisibility.Value;

                        //this._workStepModel.SetFood(this.SelectedFood.Value);

                        //this.CurrentStep.Value.Navigate(navigation);
                        this._workStepModel.NavigateUpdateWork(this.SelectedFood.Value, navigation);
                    }
                }
            });
        }

        /// <summary>
        /// dispose
        /// </summary>
        public void Dispose()
        {
        }
    }
}
