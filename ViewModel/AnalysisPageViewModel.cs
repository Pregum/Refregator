using System;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

using MVVM_Refregator.Model;
using MVVM_Refregator.Common;

namespace MVVM_Refregator.ViewModel
{
    /// <summary>
    /// AnalysisページのViewModel
    /// </summary>
    public class AnalysisPageViewModel : BindableBase
    {
        /// <summary>
        /// 解析用オブジェクト
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
        /// 食品成分表のコレクション
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

        //[Obsolete("Debug用")]
        //public ReactiveCommand Send_ReadJson { get; } = new ReactiveCommand();

        /// <summary>
        /// 解析用コレクションに追加
        /// </summary>
        public ReactiveCommand Send_AddAnalysisFood { get; } = new ReactiveCommand();

        ///// <summary>
        ///// 食品成分表をエクスポートする(json形式)
        ///// </summary>
        //public ReactiveCommand Send_Serialize { get; } = new ReactiveCommand();

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

            // 解析用コレクションに追加コマンドの購読
            this.Send_AddAnalysisFood.Subscribe((x) =>
            {
                if (x is FoodModel food)
                {
                    this._analysisPageModel.MoveToAnalysis(food);
                }
            });

            // 解析用コレクションから削除コマンドの購読
            this.Send_RemoveAnalysisFood.Subscribe((x) =>
            {
                if (x is FoodModel food)
                {
                    this._analysisPageModel.MoveToFoodList(food);
                }
            });

            // 食材成分計算コマンドの購読
            this.Send_CalculateFoodComposition.Subscribe(() =>
            {
                this._analysisPageModel.CalculateFoodComposition();
            });

            //// 食品成分表のシリアライズ
            //this.Send_Serialize.Subscribe(() =>
            //{
            //    //JsonManager.SaveJsonTo(this._analysisPageModel.FoodCompositions, "serialized_food_composition.json");
            //    JsonManager.SaveFoodComposition(this._analysisPageModel.FoodCompositions, "serialized_food_composition.json");
            //});
        }
    }
}
