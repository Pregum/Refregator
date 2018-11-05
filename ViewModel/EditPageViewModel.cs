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
    public class EditPageViewModel : BindableBase, IDisposable
    {

        private FoodShelfModel _foodShelfModel;

        private WorkStepModel _workStepModel;

        public ReadOnlyReactiveCollection<IStep> WorkSteps { get; private set; }

        private ReactiveProperty<IStep> _currentStep;

        public ReactiveProperty<IStep> CurrentStep
        {
            get { return _currentStep; }
            private set { this.SetProperty(ref _currentStep, value); }
        }

        public ReactiveProperty<FoodModel> ManipulateFood;

        public ReactiveProperty<bool> ButtonVisibility { get; } = new ReactiveProperty<bool>(true);

        public ReactiveProperty<bool> WorkLoadVisibility { get; } = new ReactiveProperty<bool>(false);

        public ReadOnlyReactiveCollection<FoodModel> Foods { get; }

        //public ReactiveProperty<DateTime> InputBoughtDate { get; } = new ReactiveProperty<DateTime>(DateTime.Today);
        //public ReactiveProperty<DateTime> InputLimitDate { get; } = new ReactiveProperty<DateTime>(DateTime.Today.AddDays(7));
        //public ReactiveProperty<double> BorderOpacity { get; } = new ReactiveProperty<double>(0.5);
        //public ReactiveProperty<double> EllipseOpacity { get; } = new ReactiveProperty<double>(0.5);

        public ReactiveProperty<string> NextContent { get; } = new ReactiveProperty<string>("次へ");

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

        public ReactiveCommand Send_PrevStep { get; } = new ReactiveCommand();

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        /// <summary>
        /// ctor
        /// </summary>
        public EditPageViewModel()
        {
            var model = new FoodShelfModel();
            model.Create("first", DateTime.Now.AddDays(1), DateTime.Now, FoodType.Other, new BitmapImage(new Uri("/Resources/information_image.png", UriKind.Relative)));
            model.Create("second", DateTime.Now.AddDays(8), DateTime.Now, FoodType.Other, new BitmapImage(new Uri("/Resources/information_image.png", UriKind.Relative)));
            this._foodShelfModel = model;
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

            //this._workStepModel = new WorkStepModel();
            this._workStepModel = WorkStepModel.GetInstance();
            this.WorkSteps = this._workStepModel.RegisterSteps.ToReadOnlyReactiveCollection(this._workStepModel.RegisterSteps.ToCollectionChanged(), System.Reactive.Concurrency.Scheduler.CurrentThread).AddTo(this.Disposable);
            this.CurrentStep = new ReactiveProperty<IStep>(this.WorkSteps.First());

            this.ManipulateFood = new ReactiveProperty<FoodModel>(this._workStepModel.ManipulateFood);

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
                
                    this.CurrentStep.Value.Update(ManipulateFood.Value);
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
                            this._foodShelfModel.FoodCollection.Add(ManipulateFood.Value);
                            Debug.WriteLine("食材を追加しました...");

                            // 各コントロールの初期化(次へボタンや、progress tracker等)
                            foreach (var aStep in this.WorkSteps)
                            {
                                aStep.Init();
                            }

                            this._workStepModel.InitializeFood();
                            this.ManipulateFood = new ReactiveProperty<FoodModel>(this._workStepModel.ManipulateFood);
                            this.ButtonVisibility.Value = !this.ButtonVisibility.Value;
                            this.WorkLoadVisibility.Value = !this.WorkLoadVisibility.Value;
                            this._currentStepIndex = 0;
                            this.CurrentStep.Value = this.WorkSteps[this._currentStepIndex];

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

            this.Send_PrevStep.Subscribe((namevigationService) =>
            {
                if (namevigationService is NavigationService navigation)
                {

                    if (this.CurrentStep.Value == this.WorkSteps.First())
                    {
                        this.ButtonVisibility.Value = !this.ButtonVisibility.Value;
                        this.WorkLoadVisibility.Value = !this.WorkLoadVisibility.Value;
                    }

                    if (this._currentStepIndex > 0)
                    {
                        this.CurrentStep.Value.Revert();
                        this._currentStepIndex--;
                        this._currentStep.Value = this.WorkSteps[_currentStepIndex];
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
