﻿using Prism.Mvvm;
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
        /// <summary>
        /// 変更前の食材名(Previous用)
        /// </summary>
        private string _oldFoodName;

        /// <summary>
        /// Viewのページ
        /// </summary>
        private Uri _editPage = new Uri("/View/StepOfSettingFoodName.xaml", UriKind.Relative);

        /// <summary>
        /// 作業名
        /// </summary>
        public string Name => "食材の設定";

        /// <summary>
        /// 作業状態
        /// </summary>
        private StepStatusType _stepStatus = StepStatusType.New;

        /// <summary>
        /// 作業状態
        /// </summary>
        public StepStatusType StepStatus { get { return _stepStatus; } }

        /// <summary>
        /// 食材名
        /// </summary>
        private string _foodName = "食材１";

        /// <summary>
        /// 食材名
        /// </summary>
        public string FoodName
        {
            get { return _foodName; }
            set { this.SetProperty(ref _foodName, value); }
        }

        /// <summary>
        /// singleton用
        /// </summary>
        private static FoodNameEditStep _instance = null;

        /// <summary>
        /// singleton用
        /// </summary>
        /// <returns></returns>
        public static FoodNameEditStep GetInstance()
        {
            _instance = _instance ?? new FoodNameEditStep();
            return _instance;
        }

        /// <summary>
        /// ctor
        /// </summary>
        private FoodNameEditStep()
        {
        }

        /// <summary>
        /// 画面遷移を行います
        /// </summary>
        /// <param name="navigation">遷移処理元のFrameのNavigationService</param>
        public void Navigate(NavigationService navigation)
        {
            this.SetProperty(ref _stepStatus, StepStatusType.Working, nameof(this.StepStatus));
            navigation.Navigate(this._editPage);
        }

        /// <summary>
        /// 食材に対し、名前の変更を行い自身を完了状態にします
        /// </summary>
        /// <param name="food">変更を加える食材</param>
        public void Update(FoodModel food)
        {
            if (food == null)
            {
                return;
            }
            food.Name = this.FoodName;
            this._oldFoodName = this.FoodName;
            this.SetProperty(ref _stepStatus, StepStatusType.Done, nameof(this.StepStatus));
        }

        /// <summary>
        /// 初期化を行います
        /// </summary>
        public void Init()
        {
            this.FoodName = "食材１";;
            this.SetProperty(ref _stepStatus, StepStatusType.New, nameof(this.StepStatus));
        }

        /// <summary>
        /// 元の状態に戻します
        /// </summary>
        public void Revert()
        {
            this.FoodName = this._oldFoodName;
            this.SetProperty(ref _stepStatus, StepStatusType.New, nameof(this.StepStatus));
        }
    }

    /// <summary>
    /// 購入日を設定するステップです
    /// </summary>
    public class FoodBoughtDateEditStep : BindableBase, IStep
    {
        private DateTime _boughtDate;

        private Uri _editPage = new Uri("/View/StepOfSettingFoodBoughtDate.xaml", UriKind.Relative);

        private StepStatusType _stepStatus = StepStatusType.New;

        public DateTime BoughtDate { get; set; } = DateTime.Today;

        public string Name => "購入日の設定";

        public StepStatusType StepStatus { get => _stepStatus; }

        private static FoodBoughtDateEditStep _instance = null;
        public static FoodBoughtDateEditStep GetInstance()
        {
            _instance = _instance ?? new FoodBoughtDateEditStep();
            return _instance;
        }

        private FoodBoughtDateEditStep()
        {
        }

        /// <summary>
        /// 購入日を設定するページへ遷移します
        /// </summary>
        /// <param name="navigation">遷移処理が行われるフレームのNavigationService</param>
        public void Navigate(NavigationService navigation)
        {
            this.SetProperty(ref _stepStatus, StepStatusType.Working, nameof(this.StepStatus));
            navigation.Navigate(this._editPage);
        }

        /// <summary>
        /// 食材に対し、購入日を設定し自身を完了状態にします
        /// </summary>
        /// <param name="food">変更を加える食材</param>
        public void Update(FoodModel food)
        {
            if (food == null)
            {
                return;
            }
            food.BoughtDate = this.BoughtDate;
            this._boughtDate = this.BoughtDate;
            this.SetProperty(ref _stepStatus, StepStatusType.Done, nameof(this.StepStatus));
        }

        /// <summary>
        /// 自身を初期状態にします
        /// </summary>
        public void Init()
        {
            this.BoughtDate = DateTime.Today;
            this.SetProperty(ref _stepStatus, StepStatusType.New, nameof(this.StepStatus));
        }

        public void Revert()
        {
            this.BoughtDate = this._boughtDate;
            this.SetProperty(ref _stepStatus, StepStatusType.New, nameof(this.StepStatus));
        }
    }

    /// <summary>
    /// 賞味期限を設定するステップです
    /// </summary>
    public class FoodLimitDateEditStep : BindableBase, IStep
    {
        private DateTime _oldLimitDate;

        private Uri _editPage = new Uri("/View/StepOfSettingFoodLimitDate.xaml", UriKind.Relative);

        private StepStatusType _stepStatus = StepStatusType.New;

        public string Name => "賞味期限の設定";

        public DateTime LimitDate { get; set; } = DateTime.Today.AddDays(7);

        public StepStatusType StepStatus { get => _stepStatus; }

        private static FoodLimitDateEditStep _instance = null;
        public static FoodLimitDateEditStep GetInstance()
        {
            _instance = _instance ?? new FoodLimitDateEditStep();
            return _instance;
        }

        private FoodLimitDateEditStep()
        {
        }

        public void Navigate(NavigationService navigation)
        {
            this.SetProperty(ref _stepStatus, StepStatusType.Working, nameof(this.StepStatus));
            navigation.Navigate(this._editPage);
        }

        public void Update(FoodModel food)
        {
            if (food == null)
            {
                return;
            }

            food.LimitDate = this.LimitDate;
            this._oldLimitDate = this.LimitDate;
            this.SetProperty(ref _stepStatus, StepStatusType.Done, nameof(this.StepStatus));
        }

        /// <summary>
        /// 初期化を行います
        /// </summary>
        public void Init()
        {
            this.LimitDate = DateTime.Today.AddDays(7);
            this.SetProperty(ref _stepStatus, StepStatusType.New, nameof(this.StepStatus));
        }

        public void Revert()
        {
            this.LimitDate = this._oldLimitDate;
            this.SetProperty(ref _stepStatus, StepStatusType.New, nameof(this.StepStatus));
        }
    }

    /// <summary>
    /// 重量を設定するステップです
    /// </summary>
    public class WeightEditStep : BindableBase, IStep
    {
        private StepStatusType _stepStatus = StepStatusType.New;

        public float Weight { get; set; }

        public string Name => "重量の設定";

        public StepStatusType StepStatus { get => _stepStatus; }

        public void Navigate(NavigationService navigation)
        {
            throw new NotImplementedException();
        }

        public void Update(FoodModel food)
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void Revert()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 最終確認を行うステップです
    /// </summary>
    public class FoodConfirmStep : BindableBase, IStep
    {
        private StepStatusType _stepStatus = StepStatusType.New;

        private Uri _editPage = new Uri("/View/StepOfSettingRegisterConfirm.xaml", UriKind.Relative);

        public StepStatusType StepStatus { get => _stepStatus; }

        public string Name => "確認画面";

        private static FoodConfirmStep _instance = null;
        public static FoodConfirmStep GetInstance()
        {
            _instance = _instance ?? new FoodConfirmStep();
            return _instance;
        }

        private FoodConfirmStep()
        {
        }

        public void Navigate(NavigationService navigation)
        {
            this.SetProperty(ref _stepStatus, StepStatusType.Working, nameof(this.StepStatus));
            navigation.Navigate(this._editPage);
        }

        public void Update(FoodModel food)
        {
            if (food == null)
            {
                return;
            }
            this.SetProperty(ref _stepStatus, StepStatusType.Done, nameof(this.StepStatus));
        }

        public void Init()
        {
            this.SetProperty(ref _stepStatus, StepStatusType.New, nameof(this.StepStatus));
        }

        public void Revert()
        {
            this.SetProperty(ref _stepStatus, StepStatusType.New, nameof(this.StepStatus));
        }
    }
}
