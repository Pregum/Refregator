using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Windows.Navigation;
using MVVM_Refregator.Model;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

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
        /// 現在の一連作業ステップ
        /// </summary>
        public ReactiveProperty<ObservableCollection<IStep>> WorkSteps { get; }

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
        public ReactiveProperty<string> NextContent { get; } = new ReactiveProperty<string>("進む");

        /// <summary>
        /// 前へボタンのContent
        /// </summary>
        public ReactiveProperty<string> PrevContent { get; } = new ReactiveProperty<string>("戻る");

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

        /// <summary>
        /// Disposeをまとめる
        /// </summary>
        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="model"></param>
        public EditPageViewModel()
        {
            // FoodShelfModel関係
            this._foodShelfModel = FoodShelfModel.GetInstance();
            this.Foods = this._foodShelfModel.FoodCollection
                .ToReadOnlyReactiveCollection(_foodShelfModel.FoodCollection.ToCollectionChanged(), System.Reactive.Concurrency.Scheduler.CurrentThread)
                .AddTo(this.Disposable);
            this._foodShelfModel.FoodCollection.CollectionChangedAsObservable().Subscribe(x => RaisePropertyChanged(nameof(Foods)));

            // WorkStepModel関係
            this._workStepModel = WorkStepModel.GetInstance();
            this.WorkSteps = this._workStepModel.ObserveProperty(x => x.CurrentWorkSteps).ToReactiveProperty();

            // スタンバイモードでなければスタンバイモードに変更する
            if (this._workStepModel.CurrentWorkStepsType != WorkType.StandBy)
            {
                this._workStepModel.SetStandBy();
            }

            // 選択食材プロパティの購読
            this.SelectedFood.Subscribe((_) =>
            {
                this.IsSelectedFood.Value = this.SelectedFood?.Value != null;
            });

            // 次へボタンのコンテントプロパティの購読
            this.IsLastStep = this._workStepModel.ObserveProperty(x => x.IsLastStep).ToReactiveProperty();
            this.IsLastStep.Subscribe((isLastStep) =>
            {
                if (isLastStep)
                {
                    switch (this._workStepModel.CurrentWorkStepsType)
                    {
                        case WorkType.Create:
                            this.NextContent.Value = "登録";
                            break;
                        case WorkType.Update:
                            this.NextContent.Value = "更新";
                            break;
                        case WorkType.Delete:
                            this.NextContent.Value = "削除";
                            break;
                        case WorkType.None:
                        case WorkType.StandBy:
                        default:
                            break;
                    }
                }
                else
                {
                    this.NextContent.Value = "進む";
                }
            });

            // 現在の作業の種類プロパティの購読
            this.CurrentWorkStepsType = this._workStepModel.ObserveProperty(m => m.CurrentWorkStepsType).ToReactiveProperty();
            this.CurrentWorkStepsType.Subscribe((currentWorkStepsType) =>
            {
                this.ButtonVisibility.Value = currentWorkStepsType == WorkType.StandBy;
                this.WorkLoadVisibility.Value = currentWorkStepsType != WorkType.StandBy;
            });

            // 登録コマンドの購読
            this.Send_NavigateRegister.Subscribe((x) =>
            {
                if (x is NavigationService navigation)
                {
                    this._workStepModel.NavigateAddWork(navigation);
                }
            });

            // 変更コマンドの購読
            this.Send_ModifyFood.Subscribe((x) =>
            {
                if (x is NavigationService navigation)
                {
                    if (this.SelectedFood.Value != null)
                    {
                        this._workStepModel.NavigateUpdateWork(this.SelectedFood.Value, navigation);
                    }
                }
            });

            // 削除コマンドの購読
            this.Send_RemoveFood.Subscribe((x) =>
            {
                if (x is NavigationService navigation)
                {
                    if (this.SelectedFood.Value != null)
                    {
                        this._workStepModel.NavigateDeleteWork(this.SelectedFood.Value, navigation);
                    }
                }
            });

            // 次へコマンドの購読
            this.Send_NextStep.Subscribe((navigationService) =>
            {
                if (navigationService is NavigationService navigation)
                {
                    this._workStepModel.NextStep(navigation);
                }
            });

            // 戻るコマンドの購読
            this.Send_PrevStep.Subscribe((namevigationService) =>
            {
                if (namevigationService is NavigationService navigation)
                {
                    this._workStepModel.PrevStep(navigation);
                }
            });
        }

        /// <summary>
        /// dispose
        /// </summary>
        public void Dispose()
        {
            this.Disposable.Dispose();
        }
    }
}
