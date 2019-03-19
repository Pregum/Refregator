using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MVVM_Refregator.Common;
using Prism.Mvvm;

namespace MVVM_Refregator.Model
{
    public class AnalysisPageModel : BindableBase
    {
        /// <summary>
        /// 食材管理Model
        /// </summary>
        private FoodShelfModel _foodShelfModel = FoodShelfModel.GetInstance();

        /// <summary>
        /// 食品成分表
        /// </summary>
        private ObservableCollection<FoodComposition> _foodCompositions;

        /// <summary>
        /// 食品成分表
        /// </summary>
        public ObservableCollection<FoodComposition> FoodCompositions
        {
            get { return _foodCompositions; }
            private set { this.SetProperty(ref _foodCompositions, value); }
        }

        private FoodComposition _calculateResultFoodComposition;
        /// <summary>
        /// 計算された成分表
        /// </summary>
        public FoodComposition CalculatedResultFoodComposition
        {
            get => this._calculateResultFoodComposition;
            private set => this.SetProperty(ref this._calculateResultFoodComposition, value);
        }

        /// <summary>
        /// 今保存されているすべての食材情報
        /// </summary>
        private ObservableCollection<FoodModel> _allFoods;

        /// <summary>
        /// 保存されている食材情報
        /// </summary>
        public ObservableCollection<FoodModel> AllFoods
        {
            get { return _allFoods; }
            private set { this.SetProperty(ref _allFoods, value); }
        }

        /// <summary>
        /// 解析用コレクション
        /// </summary>
        private ObservableCollection<FoodModel> _analysisFoods;

        /// <summary>
        /// 解析用コレクション
        /// </summary>
        public ObservableCollection<FoodModel> AnalysisFoods
        {
            get { return _analysisFoods; }
            private set { this.SetProperty(ref _analysisFoods, value); }
        }

        /// <summary>
        /// singleton
        /// </summary>
        private static AnalysisPageModel _instance = null;

        /// <summary>
        /// singleton
        /// </summary>
        /// <returns></returns>
        public static AnalysisPageModel GetInstance()
        {
            _instance = _instance ?? new AnalysisPageModel();
            return _instance;
        }

        /// <summary>
        /// ctor 
        /// </summary>
        public AnalysisPageModel()
        {
        }

        /// <summary>
        /// 食材成分表データの読み込み
        /// </summary>
        public void LoadFoodComposition(string targetPath = @"food_composition_japanese.json")
        {
            this.FoodCompositions = JsonManager.ReadJson(targetPath);
            this.InitFoodCollection();
        }

        public void LoadFoodComposition(Func<string, string> pathToTextFunc, string targetPath)
        {
            this.FoodCompositions = JsonManager.ReadJson(pathToTextFunc, targetPath);
            this.InitFoodCollection();
        }

        /// <summary>
        /// バインド用コレクションの初期化
        /// </summary>
        public void InitFoodCollection()
        {
            this.AllFoods = new ObservableCollection<FoodModel>(_foodShelfModel.FoodCollection);
            this.AnalysisFoods = new ObservableCollection<FoodModel>();

            //this.AnalysisFoods =new ObservableCollection<FoodModel>( this.AllFoods.Where(x => x.HasUsed));
            this.AnalysisFoods = new ObservableCollection<FoodModel>(this.AllFoods.Where(x => x.HasUsed && x.UsedDate.Date == DateTime.Today.Date));
            this.CalculateFoodComposition(this.AnalysisFoods);
        }


        public void CalculateComposition(DateTime date)
        {
            this.AnalysisFoods = new ObservableCollection<FoodModel>(this.AllFoods.Where(x => x.HasUsed && x.UsedDate.Date == date.Date));
            this.CalculateFoodComposition(this.AnalysisFoods);
        }

        /// <summary>
        /// 全食材コレクションからグラフ用コレクションへ移動
        /// </summary>
        /// <param name="food">対象food</param>
        public void MoveToAnalysis(FoodModel food)
        {
            this.AnalysisFoods.Add(food);
            this.AllFoods.Remove(food);
        }

        /// <summary>
        /// グラフ用コレクションから全食材コレクションへ移動
        /// </summary>
        /// <param name="food">移動対象のfood</param>
        public void MoveToFoodList(FoodModel food)
        {
            this.AllFoods.Add(food);
            this.AnalysisFoods.Remove(food);
        }

        /// <summary>
        /// 設定された食材の成分表を計算する
        /// </summary>
        public void CalculateFoodComposition(IList<FoodModel> foodModels)
        {
            Func<UnitKind, Nutrient> func = ((UnitKind uni) => new Nutrient(0.0d, uni, false));
            var composition = new FoodComposition(0, 0, 0, "accumulate_composition",
                func(UnitKind.percent),
                func(UnitKind.kcal),
                func(UnitKind.kj),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.mg),
                func(UnitKind.mg),
                func(UnitKind.mg),
                func(UnitKind.mg),
                func(UnitKind.mg),
                func(UnitKind.mg),
                func(UnitKind.mg),
                func(UnitKind.mg),
                func(UnitKind.mg),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.mg),
                func(UnitKind.mg),
                func(UnitKind.mg),
                func(UnitKind.mg),
                func(UnitKind.g),
                func(UnitKind.mg),
                func(UnitKind.mg),
                func(UnitKind.mg),
                func(UnitKind.mg),
                func(UnitKind.micro_g),
                func(UnitKind.micro_g),
                func(UnitKind.mg),
                func(UnitKind.micro_g),
                func(UnitKind.mg),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.g),
                func(UnitKind.percent));

            if (this.AnalysisFoods.Any())
            {
                //foreach (var food in this.AnalysisFoods)
                //{
                //    composition += GetFoodComposition(food.KindType);
                //}
                foreach (var food in foodModels)
                {
                    composition += GetFoodComposition(food.KindType);
                    System.Diagnostics.Debug.WriteLine($"id : {food.Id} の {food.Name}が栄養素計算されました。");
                }
                this.CalculatedResultFoodComposition = composition;
                this.RaisePropertyChanged(nameof(this.CalculatedResultFoodComposition));
            }
            if (this.AnalysisFoods.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine($"選択日に使用された食材が存在しませんでした。");
                this.CalculatedResultFoodComposition = composition;
                this.RaisePropertyChanged(nameof(this.CalculatedResultFoodComposition));
            }
        }

        /// <summary>
        /// 食品タイプに応じて成分表を返す
        /// </summary>
        /// <param name="foodType">食品タイプ</param>
        /// <returns></returns>
        private FoodComposition GetFoodComposition(FoodType foodType)
        {
            // TODO: 全食品を列挙するのは非効率的なので、配列で管理するなどして、パターン化できるか検討
            switch (foodType)
            {
                case FoodType.Rice:
                    return this.FoodCompositions[90];
                case FoodType.Bread:
                    return this.FoodCompositions[30];
                case FoodType.Noodle:
                    return this.FoodCompositions[50];
                case FoodType.Potato:
                    return this.FoodCompositions[170];
                case FoodType.Sugar:
                    return this.FoodCompositions[230];
                case FoodType.Confectionery:
                    return this.FoodCompositions[1850];
                case FoodType.Oil:
                    return this.FoodCompositions[1820];
                case FoodType.Soy:
                    return this.FoodCompositions[280];
                case FoodType.Miso:
                    return this.FoodCompositions[2130];
                case FoodType.Fruit:
                    return this.FoodCompositions[440];
                case FoodType.Vegetables:
                    return this.FoodCompositions[425];
                case FoodType.Seaweed:
                    return this.FoodCompositions[1025];
                case FoodType.SeaFood:
                    return this.FoodCompositions[1040];
                case FoodType.BeastMeat:
                    return this.FoodCompositions[1500];
                case FoodType.Egg:
                    return this.FoodCompositions[1750];
                case FoodType.Milk:
                    return this.FoodCompositions[1756];
                case FoodType.OtherDairyProducts:
                    return this.FoodCompositions[1780];
                case FoodType.Seasoning:
                    return this.FoodCompositions[2110];
                case FoodType.Other:
                    return this.FoodCompositions[2169];
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
