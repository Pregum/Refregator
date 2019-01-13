using System;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

using MVVM_Refregator.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Linq;

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
        public ObservableCollection<FoodModel> Foods { get; }

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

        public SeriesCollection SeriesCollection { get; private set; }
        public ObservableCollection<string> Labels { get; private set; }
        public Dictionary<string, ObservableCollection<Nutrient>> NutrientGroup { get; }
        public Dictionary<string, ObservableCollection<string>> LabelGroup { get; }
        public ObservableCollection<string> ComboBoxGroup { get; }
        public ReactiveProperty<string> SelectedText { get; } = new ReactiveProperty<string>("廃棄率");
        public Func<double, string> Formatter { get; } = val => val.ToString("0.##");

        /// <summary>
        /// 解析用コレクションに追加
        /// </summary>
        public ReactiveCommand Send_AddAnalysisFood { get; } = new ReactiveCommand();

        ///// <summary>
        ///// 食品成分表をエクスポートする(json形式)
        ///// </summary>
        //public ReactiveCommand Send_Serialize { get; } = new ReactiveCommand();

        ///// <summary>
        ///// 解析用コレクションから削除
        ///// </summary>
        //public ReactiveCommand Send_RemoveAnalysisFood { get; } = new ReactiveCommand();

        ///// <summary>
        ///// 解析用コレクションの成分表を計算
        ///// </summary>
        //public ReactiveCommand Send_CalculateFoodComposition { get; } = new ReactiveCommand();

        /// <summary>
        /// ctor
        /// </summary>
        public AnalysisPageViewModel()
        {
            // Modelのコレクションの初期化
            this._analysisPageModel.InitFoodCollection();

            this.Foods = this._analysisPageModel.AllFoods;
            //this.AnalysisFoodList = this._analysisPageModel.AnalysisFoods;
            //this.FoodCompositions = this._analysisPageModel.FoodCompositions;

            //this.CalculateFoodComposition = this._analysisPageModel.ObserveProperty(x => x.CalculatedResultFoodComposition).ToReactiveProperty();
            this.CalculateFoodComposition = this._analysisPageModel.ToReactivePropertyAsSynchronized(x => x.CalculatedResultFoodComposition,
                convert => convert,
                convertBack => convertBack,
                ReactivePropertyMode.DistinctUntilChanged | ReactivePropertyMode.RaiseLatestValueOnSubscribe,
                false);

            this.SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "栄養素",
                    Values = new ChartValues<double> {
                        this.CalculateFoodComposition.Value.FattyAcid_Saturated.Value,
                        this.CalculateFoodComposition.Value.FattyAcid_PolyUnsaturated.Value,
                        this.CalculateFoodComposition.Value.FattyAcid_MonoUnsaturated.Value,
                        this.CalculateFoodComposition.Value.Energy_kcal.Value }
                }
            };
            this.Labels = new ObservableCollection<string>() { "飽和脂肪酸", "多価不飽和脂肪酸", "一価不和飽和脂肪酸", "エネルギー(kcal)" };

            this.ComboBoxGroup = new ObservableCollection<string>() {
                "廃棄率", "エネルギー", "水分", "タンパク質",
                "脂質", "脂肪酸", "コレステロール", "炭水化物", "食物繊維", "灰分",
                "無機質その1", "無機質その2", "無機質その3", "無機質その4",
                "ビタミンその1", "ビタミンその2", "ビタミンその3", "ビタミンその4", "ビタミンその5", "ビタミンその6" };

            this.NutrientGroup = new Dictionary<string, ObservableCollection<Nutrient>>
            {
                {"廃棄率", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Refuse } },
                {"エネルギー", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Energy_kcal, this.CalculateFoodComposition.Value.Energy_kj } },
                {"水分", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Water,  } },
                {"タンパク質", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Protein, this.CalculateFoodComposition.Value.Protein_AminoAcidResidues } },
                {"脂質", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Lipid, this.CalculateFoodComposition.Value.FattyAcid_TriacylGlycerol } },
                {"脂肪酸", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.FattyAcid_Saturated, this.CalculateFoodComposition.Value.FattyAcid_MonoUnsaturated, this.CalculateFoodComposition.Value.FattyAcid_PolyUnsaturated } },
                {"コレステロール", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Cholesterol} },
                {"炭水化物", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Carbohydrate, this.CalculateFoodComposition.Value.Carbohydrate_Available } },
                {"食物繊維", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.DietaryFiber_Soluble, this.CalculateFoodComposition.Value.DietaryFiber_Insoluble, this.CalculateFoodComposition.Value.DietaryFiber_Total  } },
                {"灰分", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Ash } },
                {"無機質その1", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Sodium, this.CalculateFoodComposition.Value.Potassium, this.CalculateFoodComposition.Value.Calcium, this.CalculateFoodComposition.Value.Magnesium, } },
                {"無機質その2", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Phosphorus, this.CalculateFoodComposition.Value.Iron, this.CalculateFoodComposition.Value.Zinc, this.CalculateFoodComposition.Value.Copper,  } },
                {"無機質その3", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Manganese, this.CalculateFoodComposition.Value.Iodine, this.CalculateFoodComposition.Value.Selenium, this.CalculateFoodComposition.Value.Chromium,  } },
                {"無機質その4", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Molybdenum} },
                {"ビタミンその1", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Retinol, this.CalculateFoodComposition.Value.Alpha_Carotene, this.CalculateFoodComposition.Value.Beta_Carotene, this.CalculateFoodComposition.Value.Beta_Cryptoxanthin,  } },
                {"ビタミンその2", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Beta_CaroteneEquivalents, this.CalculateFoodComposition.Value.Retinon_ActivityEquivalents, this.CalculateFoodComposition.Value.Vitamin_D, this.CalculateFoodComposition.Value.Alpha_Tocopherol,  } },
                {"ビタミンその3", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Beta_Tocopherol, this.CalculateFoodComposition.Value.Gamma_Tocopherol, this.CalculateFoodComposition.Value.Delta_Tocopherol, this.CalculateFoodComposition.Value.Vitamin_K,  } },
                {"ビタミンその4", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Thiamin, this.CalculateFoodComposition.Value.Riboflavin, this.CalculateFoodComposition.Value.Niacin, this.CalculateFoodComposition.Value.Vitamin_B6,  } },
                {"ビタミンその5", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Vitamin_B12, this.CalculateFoodComposition.Value.Folate, this.CalculateFoodComposition.Value.Pantothenic_Acid, this.CalculateFoodComposition.Value.Biotin,  } },
                {"ビタミンその6", new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Ascorbic_Acid } },
            };


            this.LabelGroup = new Dictionary<string, ObservableCollection<string>>
            {
                {"廃棄率", new ObservableCollection<string>(){"廃棄率"} },
                {"エネルギー", new ObservableCollection<string>(){"エネルギー(kcal)", "エネルギー(kJ)"} },
                {"水分", new ObservableCollection<string>(){"水分"} },
                {"タンパク質", new ObservableCollection<string>(){"タンパク質", "アミノ酸組成によるタンパク質"} },
                {"脂質", new ObservableCollection<string>(){"脂質", "トリアシルグリセロール当量" } },
                {"脂肪酸", new ObservableCollection<string>(){"脂肪酸(飽和)", "脂肪酸(一価不飽和)", "脂肪酸(多価不飽和)",} },
                {"コレステロール", new ObservableCollection<string>(){ "コレステロール", } },
                {"炭水化物", new ObservableCollection<string>(){ "炭水化物", "利用可能炭水化物"} },
                {"食物繊維", new ObservableCollection<string>(){ "食物繊維(水溶性)", "食物繊維(不溶性)","食物繊維(総量)"} },
                {"灰分", new ObservableCollection<string>(){ "灰分", } },
                {"無機質その1", new ObservableCollection<string>(){ "ナトリウム", "カリウム", "カルシウム", "マグネシウム",  } },
                {"無機質その2", new ObservableCollection<string>(){ "リン", "鉄", "亜鉛", "銅", } },
                {"無機質その3", new ObservableCollection<string>(){ "マンガン", "ヨウ素", "セレン", "クロム",} },
                {"無機質その4", new ObservableCollection<string>(){ "モリブデン"} },
                {"ビタミンその1", new ObservableCollection<string>(){ "レチノール", "α-カロテン", "β-カロテン", "β-クリプトサチン",  } },
                {"ビタミンその2", new ObservableCollection<string>(){ "β-カロテン当量", "レチノール活性当量","ビタミンD", "α-トコフェロール",  } },
                {"ビタミンその3", new ObservableCollection<string>(){ "β-トコフェロール", "γ-トコフェロール", "δ-トコフェロール","ビタミンK", } },
                {"ビタミンその4", new ObservableCollection<string>(){ "ビタミンB1", "ビタミンB2", "ナイアシン", "ビタミンB6",} },
                {"ビタミンその5", new ObservableCollection<string>(){ "ビタミンB12", "葉酸", "パントテン酸", "ビオチン", } },
                {"ビタミンその6", new ObservableCollection<string>(){ "ビタミンC"} },
            };

            this.SelectedText.Subscribe((x) =>
            {
                var seriesGroup = this.NutrientGroup[x].Select(y => y.Value);
                var labels = this.LabelGroup[x];
                this.Labels = labels;
                this.RaisePropertyChanged(nameof(this.Labels));
                this.SeriesCollection[0].Values = new ChartValues<double>(seriesGroup);
                this.RaisePropertyChanged(nameof(SeriesCollection));
            });


            //// 解析用コレクションに追加コマンドの購読
            //this.Send_AddAnalysisFood.Subscribe((x) =>
            //{
            //    if (x is FoodModel food)
            //    {
            //        this._analysisPageModel.MoveToAnalysis(food);
            //    }
            //});

            //// 解析用コレクションから削除コマンドの購読
            //this.Send_RemoveAnalysisFood.Subscribe((x) =>
            //{
            //    if (x is FoodModel food)
            //    {
            //        this._analysisPageModel.MoveToFoodList(food);
            //    }
            //});

            //// 食材成分計算コマンドの購読
            //this.Send_CalculateFoodComposition.Subscribe(() =>
            //{
            //    this._analysisPageModel.CalculateFoodComposition();
            //});

            //// 食品成分表のシリアライズ
            //this.Send_Serialize.Subscribe(() =>
            //{
            //    //JsonManager.SaveJsonTo(this._analysisPageModel.FoodCompositions, "serialized_food_composition.json");
            //    JsonManager.SaveFoodComposition(this._analysisPageModel.FoodCompositions, "serialized_food_composition.json");
            //});
        }
    }
}
