﻿using System;
using System.Collections.ObjectModel;
using MVVM_Refregator.Model;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MVVM_Refregator.ViewModel
{
    /// <summary>
    /// AnalysisページのViewModel
    /// </summary>
    public class AnalysisPageViewModel : BindableBase
    {
        /// <summary>
        /// Model
        /// </summary>
        private AnalysisPageModel _analysisPageModel = AnalysisPageModel.GetInstance();

        /// <summary>
        /// 全食材コレクション
        /// </summary>
        public ObservableCollection<FoodModel> FoodList { get; }

        /// <summary>
        /// 計算後の成分表
        /// </summary>
        public ReactiveProperty<FoodComposition> CalculateFoodComposition { get; }

        /// <summary>
        /// 食品成分表
        /// </summary>
        public ObservableCollection<FoodComposition> FoodCompositions { get; }

        /// <summary>
        /// グラフ化する食材コレクション
        /// </summary>
        public ObservableCollection<FoodModel> AnalysisFoodList { get; }

        /// <summary>
        /// 全食材コレクションのDataGridで選択中のFoodModel
        /// </summary>
        public ReactiveProperty<FoodModel> SelectedFood_AllGrid { get; } = new ReactiveProperty<FoodModel>();

        /// <summary>
        /// グラフ化する食材コレクションのDataGridで選択中のFoodModel
        /// </summary>
        public ReactiveProperty<FoodModel> SelectedFood_AnalysisGrid { get; } = new ReactiveProperty<FoodModel>();

        [Obsolete("Debug用")]
        public ReactiveCommand Send_ReadJson { get; } = new ReactiveCommand();

        /// <summary>
        /// 解析用コレクションに追加
        /// </summary>
        public ReactiveCommand Send_AddAnalysisFood { get; } = new ReactiveCommand();

        /// <summary>
        /// 解析用コレクションから削除
        /// </summary>
        public ReactiveCommand Send_RemoveAnalysisFood { get; } = new ReactiveCommand();

        /// <summary>
        /// 解析用コレクションの成分表を計算
        /// </summary>
        public ReactiveCommand Send_CalculateFoodComposition { get; } = new ReactiveCommand();

        /// <summary>
        /// ctor
        /// </summary>
        public AnalysisPageViewModel()
        {
            // Modelのコレクションの初期化
            this._analysisPageModel.InitFoodCollection();

            this.FoodList = this._analysisPageModel.AllFoods;
            this.AnalysisFoodList = this._analysisPageModel.AnalysisFoods;
            this.FoodCompositions = this._analysisPageModel.FoodCompositions;

            //this.CalculateFoodComposition = this._analysisPageModel.ObserveProperty(x => x.CalculatedResultFoodComposition).ToReactiveProperty();
            this.CalculateFoodComposition = this._analysisPageModel.ToReactivePropertyAsSynchronized(x => x.CalculatedResultFoodComposition,
                convert => convert,
                convertBack => convertBack,
                ReactivePropertyMode.DistinctUntilChanged | ReactivePropertyMode.RaiseLatestValueOnSubscribe,
                false);

            this.Send_AddAnalysisFood.Subscribe((x) =>
            {
                if (x is FoodModel food)
                {
                    this._analysisPageModel.MoveToAnalysis(food);
                }
            });

            this.Send_RemoveAnalysisFood.Subscribe((x) =>
            {
                if (x is FoodModel food)
                {
                    this._analysisPageModel.MoveToFoodList(food);
                }
            });

            this.Send_CalculateFoodComposition.Subscribe(() =>
            {
                this._analysisPageModel.CalculateFoodComposition();
            });
        }
    }
}
