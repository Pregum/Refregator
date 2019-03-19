using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
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
        /// 最初のステップか
        /// </summary>
        public ReactiveProperty<bool> IsFirsStep { get; }

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
        public ReadOnlyReactiveCollection<FoodModel> Foods { get; private set; }

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
        /// 次のステップへ遷移する次へコマンド
        /// </summary>
        public ReactiveCommand Send_NextStep { get; } = new ReactiveCommand();

        /// <summary>
        /// 前のステップへ遷移する前へコマンド
        /// </summary>
        public ReactiveCommand Send_PrevStep { get; } = new ReactiveCommand();

        /// <summary>
        /// 食材の変更を行う変更コマンド
        /// </summary>
        public ReactiveCommand Send_ModifyFood { get; } = new ReactiveCommand();

        /// <summary>
        /// 食材の削除を行う削除コマンド
        /// </summary>
        public ReactiveCommand Send_RemoveFood { get; } = new ReactiveCommand();

        /// <summary>
        /// 食材を使用済みに設定する使用コマンド
        /// </summary>
        public ReactiveCommand Send_SetUsed { get; } = new ReactiveCommand();

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

            this._foodShelfModel.FoodCollection.CollectionChangedAsObservable().Subscribe(x =>
            {
                switch (x.Action)
                {
                    // 食材追加時
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        foreach (FoodModel food in x.NewItems)
                        {
                            food.PropertyChanged += this.Afood_PropertyChanged;
                        }
                        break;
                    // 食材削除時
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        foreach (FoodModel food in x.OldItems)
                        {
                            food.PropertyChanged -= this.Afood_PropertyChanged;
                        }
                        break;
                    // 食材置き換え時
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                        foreach (FoodModel food in x.OldItems)
                        {
                            food.PropertyChanged -= this.Afood_PropertyChanged;
                        }
                        foreach (FoodModel food in x.NewItems)
                        {
                            food.PropertyChanged += this.Afood_PropertyChanged;
                        }
                        break;
                    // 食材移動時
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                        break;
                    // 食材リセット時
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                        foreach (var food in this._foodShelfModel.FoodCollection)
                        {
                            food.PropertyChanged -= this.Afood_PropertyChanged;
                        }
                        break;
                    default:
                        break;
                }
            });

            foreach (var afood in this._foodShelfModel.FoodCollection)
            {
                afood.PropertyChanged += this.Afood_PropertyChanged;
            }

            // WorkStepModel関係
            this._workStepModel = WorkStepModel.GetInstance();
            this.WorkSteps = this._workStepModel.ObserveProperty(x => x.CurrentWorkSteps).ToReactiveProperty().AddTo(this.Disposable);

            // スタンバイモードでなければスタンバイモードに変更する
            if (this._workStepModel.CurrentWorkStepsType != WorkType.StandBy)
            {
                this._workStepModel.SetStandBy();
            }

            // 選択食材プロパティの購読
            //this.SelectedFood.Subscribe((_) =>
            //{
            //    this.IsSelectedFood.Value = this.SelectedFood?.Value != null;
            //});
            this.IsSelectedFood = this.SelectedFood.Select(x => x != null).ToReactiveProperty().AddTo(this.Disposable);

            // 現在の作業の種類プロパティの購読
            this.CurrentWorkStepsType = this._workStepModel.ObserveProperty(m => m.CurrentWorkStepsType).ToReactiveProperty().AddTo(this.Disposable);
            this.ButtonVisibility = this.CurrentWorkStepsType.Select(x => x == WorkType.StandBy).ToReactiveProperty().AddTo(this.Disposable);
            this.WorkLoadVisibility = this.CurrentWorkStepsType.Select(x => x != WorkType.StandBy).ToReactiveProperty().AddTo(this.Disposable);

            // 最後のステップ判定プロパティの購読
            this.IsLastStep = this._workStepModel.ObserveProperty(x => x.IsLastStep).ToReactiveProperty().AddTo(this.Disposable);

            this.NextContent = this.CurrentWorkStepsType.CombineLatest(this.IsLastStep, (currentWorkStepsType, isLastStep) =>
             {
                 if (isLastStep == false)
                 {
                     return "進む";
                 }
                 switch (currentWorkStepsType)
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
                         return "---";
                 }
             }).ToReactiveProperty().AddTo(this.Disposable);

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

            // 使用コマンドの購読
            this.Send_SetUsed.Subscribe((x) =>
            {
                if (x is NavigationService navigation)
                {
                    if (this.SelectedFood.Value != null)
                    {
                        this._workStepModel.NavigateUseWork(this.SelectedFood.Value, navigation);
                    }
                }
            });

            // 次へコマンドの購読
            this.Send_NextStep.Subscribe(async (navigationService) =>
            {
                if (navigationService is NavigationService navigation)
                {
                    await this._workStepModel.NextStepAsync(navigation);
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
        /// 食材のコレクションが変更されたときに発火されるイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Afood_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is FoodModel food)
            {
                if (e.PropertyName == "HasUsed")
                {
                    if (food.HasUsed)
                    {
                        this.SelectedFood.Value = null;
                    }
                }
            }
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
