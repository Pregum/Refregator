using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace MVVM_Refregator.Model
{
    /// <summary>
    /// 食材名・食材画像を編集するステップです
    /// </summary>
    public class FoodNameEditStep : BindableBase, IStep
    {
        private string _foodName = "食材_1";
        public string FoodName { get { return _foodName; } set { SetProperty(ref _foodName, value); } } 

        public string Name => "食材の設定";

        private Uri _editPage = new Uri("/View/StepOfSettingFoodName.xaml", UriKind.Relative);

        private StepStatusType _stepStatus = StepStatusType.New;

        public StepStatusType StepStatus
        {
            get { return _stepStatus; }
        }

        /// <summary>
        /// 画面遷移を行います
        /// </summary>
        /// <param name="navigation"></param>
        public void Navigate(NavigationService navigation)
        {
            this.SetProperty(ref _stepStatus, StepStatusType.Working, nameof(this.StepStatus));
            navigation.Navigate(this._editPage);
        }

        public void Update(Food food)
        {
            if (food == null)
            {
                return;
            }
            food.Name = this.Name;
            this.SetProperty(ref _stepStatus, StepStatusType.Done, nameof(this.StepStatus));
        }

        /// <summary>
        /// 初期化を行います
        /// </summary>
        public void Init()
        {
            FoodName = "食材_1";
            this.SetProperty(ref _stepStatus, StepStatusType.New, nameof(this.StepStatus));
        }
    }

    /// <summary>
    /// 購入日を設定するステップです
    /// </summary>
    public class FoodBoughtDateEditStep : BindableBase, IStep
    {
        private DateTime _boughtDate = DateTime.Today;
        public DateTime BoughtDate { get { return _boughtDate; } set { this.SetProperty(ref _boughtDate, value); } }

        public string Name => "購入日の設定";

        private Uri _editPage = new Uri("/View/StepOfSettingFoodBoughtDate.xaml", UriKind.Relative);

        private StepStatusType _stepStatus = StepStatusType.New;

        public StepStatusType StepStatus { get => _stepStatus; }

        public void Navigate(NavigationService navigation)
        {
            this.SetProperty(ref _stepStatus, StepStatusType.Working, nameof(this.StepStatus));
            //this.StepStatus = StepStatusType.Working;
            navigation.Navigate(this._editPage);
        }

        public void Update(Food food)
        {
            if (food == null)
            {
                return;
            }
            food.BoughtDate = this.BoughtDate;
            this.SetProperty(ref _stepStatus, StepStatusType.Done, nameof(this.StepStatus));
            //this.StepStatus = StepStatusType.Done;
        }

        public void Init()
        {
            this._boughtDate = DateTime.Today;
            this.SetProperty(ref _stepStatus, StepStatusType.New, nameof(this.StepStatus));
        }
    }

    /// <summary>
    /// 賞味期限を設定するステップです
    /// </summary>
    public class FoodLimitDateEditStep : BindableBase, IStep
    {
        private DateTime _limitDate = DateTime.Today.AddDays(7);
        public DateTime LimitDate { get { return _limitDate; } set { this.SetProperty(ref _limitDate, value); } }

        public string Name => "賞味期限の設定";

        private Uri _editPage = new Uri("/View/StepOfSettingFoodLimitDate.xaml", UriKind.Relative);

        private StepStatusType _stepStatus = StepStatusType.New;

        public StepStatusType StepStatus { get => _stepStatus; }

        public void Navigate(NavigationService navigation)
        {
            this.SetProperty(ref _stepStatus, StepStatusType.Working, nameof(this.StepStatus));
            //this.StepStatus = StepStatusType.Working;
            navigation.Navigate(this._editPage);
        }

        public void Update(Food food)
        {
            if (food == null)
            {
                return;
            }

            food.LimitDate = this.LimitDate;
            this.SetProperty(ref _stepStatus, StepStatusType.Done, nameof(this.StepStatus));
            //this.StepStatus = StepStatusType.Done;
        }

        /// <summary>
        /// 初期化を行います
        /// </summary>
        public void Init()
        {
            this.LimitDate = DateTime.Today.AddDays(7);
            this.SetProperty(ref _stepStatus, StepStatusType.New, nameof(this.StepStatus));
        }
    }

    /// <summary>
    /// 重量を設定するステップです
    /// </summary>
    public class WeightEditStep : BindableBase, IStep
    {
        public float Weight { get; set; }

        public string Name => "重量の設定";

        private StepStatusType _stepStatus = StepStatusType.New;

        public StepStatusType StepStatus { get => _stepStatus; }

        public void Navigate(NavigationService navigation)
        {
            throw new NotImplementedException();
        }

        public void Update(Food food)
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 最終確認を行うステップです
    /// </summary>
    public class FoodConfirmStep : BindableBase, IStep
    {
        public string Name => "確認画面";

        private StepStatusType _stepStatus = StepStatusType.New;

        private Uri _editPage = new Uri("/View/StepOfSettingRegisterConfirm.xaml", UriKind.Relative);

        public StepStatusType StepStatus { get => _stepStatus; }

        public void Navigate(NavigationService navigation)
        {
            this.SetProperty(ref _stepStatus, StepStatusType.Working, nameof(this.StepStatus));
            //this.StepStatus = StepStatusType.Working;
            navigation.Navigate(this._editPage);
        }

        public void Update(Food food)
        {
            if (food == null)
            {
                return;
            }
            //this.StepStatus = StepStatusType.Done;
            this.SetProperty(ref _stepStatus, StepStatusType.Done, nameof(this.StepStatus));
        }

        public void Init()
        {
            this.SetProperty(ref _stepStatus, StepStatusType.New, nameof(this.StepStatus));
        }
    }
}
