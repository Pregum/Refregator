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

        /// <summary>
        /// 現在の作業ステップ
        /// </summary>
        private ReactiveProperty<IStep> _currentStep;

        /// <summary>
        /// 現在の作業ステップ
        /// </summary>
        public ReactiveProperty<IStep> CurrentStep
        {
            get { return _currentStep; }
            private set { this.SetProperty(ref _currentStep, value); }
        }

        /// <summary>
        /// 操作される食材
        /// WorkStepModel内に移行させる予定
        /// </summary>
        public ReactiveProperty<FoodModel> ManipulateFood;

        /// <summary>
        /// 作業へ移行するためのコントロールのVisibility
        /// </summary>
        public ReactiveProperty<bool> ButtonVisibility { get; } = new ReactiveProperty<bool>(true);

        /// <summary>
        /// 作業用コントロールのVisibility
        /// </summary>
        public ReactiveProperty<bool> WorkLoadVisibility { get; } = new ReactiveProperty<bool>(false);

        /// <summary>
        /// 管理されている食材
        /// </summary>
        public ReadOnlyReactiveCollection<FoodModel> Foods { get; }

        public ReactiveProperty<FoodModel> SelectedFood { get; } = new ReactiveProperty<FoodModel>();

        public ReactiveProperty<bool> IsSelectedFood { get; private set; } = new ReactiveProperty<bool>(false);

        /// <summary>
        /// 次へボタンのContent
        /// </summary>
        public ReactiveProperty<string> NextContent { get; } = new ReactiveProperty<string>("次へ");

        /// <summary>
        /// 前へボタンのContent
        /// </summary>
        public ReactiveProperty<string> PrevContent { get; } = new ReactiveProperty<string>("前へ");

        private int _currentStepIndex = 0;

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

        /// <summary>
        /// ctor
        /// </summary>
        public EditPageViewModel()
        {
        }

        /// <summary>
        /// DI時のctor
        /// </summary>
        /// <param name="model"></param>
        public EditPageViewModel(FoodShelfModel model)
        {
            this._foodShelfModel = model;
            this.Foods = this._foodShelfModel.FoodCollection.ToReadOnlyReactiveCollection(_foodShelfModel.FoodCollection.ToCollectionChanged(), System.Reactive.Concurrency.Scheduler.CurrentThread).AddTo(this.Disposable);
            this._foodShelfModel.FoodCollection.CollectionChangedAsObservable().Subscribe(x => RaisePropertyChanged(nameof(Foods)));

            this.SelectedFood.Subscribe((y) =>
            {
                //this.IsSelectedFood.Value = this.SelectedFood?.Value.ObserveProperty(x => x != null).ToReactiveProperty();
                this.IsSelectedFood.Value = this.SelectedFood?.Value != null;
            });

            //this._workStepModel = new WorkStepModel();
            this._workStepModel = WorkStepModel.GetInstance();
            this._workStepModel.Initialize();

            this.WorkSteps = this._workStepModel.RegisterSteps.ToReadOnlyReactiveCollection(this._workStepModel.RegisterSteps.ToCollectionChanged(), System.Reactive.Concurrency.Scheduler.CurrentThread).AddTo(this.Disposable);
            this.CurrentStep = new ReactiveProperty<IStep>(this.WorkSteps.First());

            this.ManipulateFood = new ReactiveProperty<FoodModel>(this._workStepModel.ManipulateFood);

            // 登録ボタンのコマンド
            this.Send_NavigateRegister.Subscribe((x) =>
            {
                if (x is NavigationService navigation)
                {
                    this.ButtonVisibility.Value = !this.ButtonVisibility.Value;
                    this.WorkLoadVisibility.Value = !this.WorkLoadVisibility.Value;

                    this.CurrentStep.Value.Navigate(navigation);
                }
            });

            this.Send_NextStep.Subscribe((navigationService) =>
            {
                if (navigationService is NavigationService navigation)
                {

                    this.CurrentStep.Value.Update(this.ManipulateFood.Value);
                    if (this.CurrentStep.Value.StepStatus == StepStatusType.Done)
                    {
                        // nullでないかつ今のステップが最後でない場合
                        if (this.WorkSteps?.LastOrDefault() != null && this.WorkSteps?.LastOrDefault() != this.CurrentStep.Value)
                        {
                            this._currentStepIndex++;
                            this.CurrentStep.Value = this.WorkSteps[_currentStepIndex];
                            this.CurrentStep.Value.Navigate(navigation);
                        }
                        else
                        {
                            // 最後のステップが完了した場合食材データを更新する
                            Debug.WriteLine("食材を追加中...");
                            this._foodShelfModel.FoodCollection.Add(this.ManipulateFood.Value);
                            Debug.WriteLine("食材を追加しました...");

                            // 各コントロールの初期化(次へボタンや、progress tracker等)
                            this._workStepModel.Initialize();
                            this.ManipulateFood = new ReactiveProperty<FoodModel>(this._workStepModel.ManipulateFood);
                            this.ButtonVisibility.Value = !this.ButtonVisibility.Value;
                            this.WorkLoadVisibility.Value = !this.WorkLoadVisibility.Value;
                            this._currentStepIndex = 0;
                            this.CurrentStep.Value = this.WorkSteps[this._currentStepIndex];
                            this.NextContent.Value = "次へ";

                            return;
                        }

                        // 現在の作業が最後のステップならば次へボタンのContentを変える
                        if (this.WorkSteps?.LastOrDefault() != null)
                        {
                            this.NextContent.Value = this.WorkSteps?.LastOrDefault().StepStatus == StepStatusType.Working ? "登録" : "次へ";
                        }
                    }
                }
            });

            // 戻るボタンをクリック時のコマンド
            this.Send_PrevStep.Subscribe((namevigationService) =>
            {
                if (namevigationService is NavigationService navigation)
                {
                    // 今のステップが最初のステップならば
                    if (this.CurrentStep.Value == this.WorkSteps.First())
                    {
                        this.ButtonVisibility.Value = !this.ButtonVisibility.Value;
                        this.WorkLoadVisibility.Value = !this.WorkLoadVisibility.Value;
                    }
                    else
                    //// 今のステップが最初のステップでなければ、前のステップへ戻る
                    //if (this._currentStepIndex > 0)
                    {
                        this.CurrentStep.Value.Revert();
                        this._currentStepIndex--;
                        this._currentStep.Value = this.WorkSteps[_currentStepIndex];
                        this.CurrentStep.Value.Navigate(navigation);
                    }
                }

                // 現在の作業が最後のステップならば次へボタンのContentを変える
                if (this.WorkSteps?.LastOrDefault() != null)
                {
                    this.NextContent.Value = this.WorkSteps?.LastOrDefault().StepStatus == StepStatusType.Working ? "登録" : "次へ";
                }
            });

            // 削除時のコマンド
            this.Send_RemoveFood.Subscribe((targetFood) =>
            {
                //if (this.ManipulateFood.Value != null)
                //{
                //    this._workStepModel.SetFood(this.ManipulateFood.Value);
                //}
                if (targetFood is FoodModel food)
                {
                    this._foodShelfModel.Delete(food.Id);
                }
            });

            // 変更時のコマンド
            this.Send_ModifyFood.Subscribe((x) =>
            {
                if (x is NavigationService navigation)
                {
                    if (this.SelectedFood.Value != null)
                    {
                        this.WorkLoadVisibility.Value = !this.WorkLoadVisibility.Value;
                        this.ButtonVisibility.Value = !this.ButtonVisibility.Value;

                        this._workStepModel.SetFood(this.SelectedFood.Value);

                        this.CurrentStep.Value.Navigate(navigation);
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
