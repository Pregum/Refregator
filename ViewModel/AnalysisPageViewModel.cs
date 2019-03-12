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
        public ReadOnlyReactiveCollection<FoodModel> Foods { get; }

        /// <summary>
        /// 計算後の成分表
        /// </summary>
        public ReactiveProperty<FoodComposition> CalculateFoodComposition { get; }

        /// <summary>
        /// 食品成分表のコレクション
        /// </summary>
        public ObservableCollection<FoodComposition> FoodCompositions { get; }

        //public SeriesCollection SeriesCollection { get; private set; } = new SeriesCollection();
        private SeriesCollection _seriesCollection = new SeriesCollection();
        public SeriesCollection SeriesCollection { get { return this._seriesCollection; } private set { this.SetProperty(ref this._seriesCollection, value); } }
        public ObservableCollection<string> Labels { get; private set; }
        public Dictionary<string, ObservableCollection<Nutrient>> NutrientGroup { get; private set; }
        public Dictionary<string, ObservableCollection<string>> LabelGroup { get; }
        public ObservableCollection<string> ComboBoxGroup { get; }
        public ReactiveProperty<string> SelectedText { get; } = new ReactiveProperty<string>("エネルギー");
        public Func<double, string> Formatter { get; } = val => val.ToString("0.###");
        public ReactiveProperty<string> UnitText { get; private set; } = new ReactiveProperty<string>("kcal");

        private List<string> _keyList = new List<string>();

        /// <summary>
        /// 解析用コレクションに追加
        /// </summary>
        public ReactiveCommand Send_AddAnalysisFood { get; } = new ReactiveCommand();

        /// <summary>
        /// ctor
        /// </summary>
        public AnalysisPageViewModel()
        {
            // Modelのコレクションの初期化
            this._analysisPageModel.InitFoodCollection();

            this.Foods = this._analysisPageModel.AllFoods
                .ToReadOnlyReactiveCollection(_analysisPageModel.AllFoods.ToCollectionChanged(), System.Reactive.Concurrency.Scheduler.CurrentThread);

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
                        this.CalculateFoodComposition.Value.Energy_kcal.Value },
                },
            };
            this.Labels = new ObservableCollection<string>() { "飽和脂肪酸", "多価不飽和脂肪酸", "一価不和飽和脂肪酸", "エネルギー(kcal)" };

            this._keyList = new List<string> {
                "エネルギー",
                "水分",
                "タンパク質",
                "脂質",
                "脂肪酸",
                "コレステロール",
                "炭水化物",
                "食物繊維",
                "灰分",
                "無機質その1",
                "無機質その2",
                "無機質その3",
                "無機質その4",
                "ビタミンその1",
                "ビタミンその2",
                "ビタミンその3",
                "ビタミンその4",
                "ビタミンその5",
                "ビタミンその6",
                 "廃棄率",
                };

            this.ComboBoxGroup = new ObservableCollection<string>() {
                this._keyList[0], this._keyList[1], this._keyList[2],
                this._keyList[3], this._keyList[4], this._keyList[5], this._keyList[6], this._keyList[7], this._keyList[8],
                this._keyList[9], this._keyList[10], this._keyList[11], this._keyList[12],
                this._keyList[13], this._keyList[14], this._keyList[15], this._keyList[16], this._keyList[17], this._keyList[18],this._keyList[19]  };

            this.NutrientGroup = new Dictionary<string, ObservableCollection<Nutrient>>
            {
                //{this._keyList[0], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Energy_kcal, this.CalculateFoodComposition.Value.Energy_kj } },
                {this._keyList[0], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Energy_kcal, } },
                {this._keyList[1], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Water,  } },
                {this._keyList[2], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Protein, this.CalculateFoodComposition.Value.Protein_AminoAcidResidues } },
                {this._keyList[3], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Lipid, this.CalculateFoodComposition.Value.FattyAcid_TriacylGlycerol } },
                {this._keyList[4], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.FattyAcid_Saturated, this.CalculateFoodComposition.Value.FattyAcid_MonoUnsaturated, this.CalculateFoodComposition.Value.FattyAcid_PolyUnsaturated } },
                {this._keyList[5], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Cholesterol} },
                {this._keyList[6], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Carbohydrate, this.CalculateFoodComposition.Value.Carbohydrate_Available } },
                {this._keyList[7], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.DietaryFiber_Soluble, this.CalculateFoodComposition.Value.DietaryFiber_Insoluble, this.CalculateFoodComposition.Value.DietaryFiber_Total  } },
                {this._keyList[8], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Ash } },
                {this._keyList[9], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Sodium, this.CalculateFoodComposition.Value.Potassium, this.CalculateFoodComposition.Value.Calcium, this.CalculateFoodComposition.Value.Magnesium, } },
                {this._keyList[10], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Phosphorus, this.CalculateFoodComposition.Value.Iron, this.CalculateFoodComposition.Value.Zinc, this.CalculateFoodComposition.Value.Copper,  } },
                {this._keyList[11], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Manganese, this.CalculateFoodComposition.Value.Iodine, this.CalculateFoodComposition.Value.Selenium, this.CalculateFoodComposition.Value.Chromium,  } },
                {this._keyList[12], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Molybdenum} },
                {this._keyList[13], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Retinol, this.CalculateFoodComposition.Value.Alpha_Carotene, this.CalculateFoodComposition.Value.Beta_Carotene, this.CalculateFoodComposition.Value.Beta_Cryptoxanthin,  } },
                {this._keyList[14], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Beta_CaroteneEquivalents, this.CalculateFoodComposition.Value.Retinon_ActivityEquivalents, this.CalculateFoodComposition.Value.Vitamin_D, this.CalculateFoodComposition.Value.Alpha_Tocopherol,  } },
                {this._keyList[15], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Beta_Tocopherol, this.CalculateFoodComposition.Value.Gamma_Tocopherol, this.CalculateFoodComposition.Value.Delta_Tocopherol, this.CalculateFoodComposition.Value.Vitamin_K,  } },
                {this._keyList[16], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Thiamin, this.CalculateFoodComposition.Value.Riboflavin, this.CalculateFoodComposition.Value.Niacin, this.CalculateFoodComposition.Value.Vitamin_B6,  } },
                {this._keyList[17], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Vitamin_B12, this.CalculateFoodComposition.Value.Folate, this.CalculateFoodComposition.Value.Pantothenic_Acid, this.CalculateFoodComposition.Value.Biotin,  } },
                {this._keyList[18], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Ascorbic_Acid } },
                {this._keyList[19], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Refuse } },
            };


            this.LabelGroup = new Dictionary<string, ObservableCollection<string>>
            {
                //{this._keyList[0], new ObservableCollection<string>(){"エネルギー(kcal)", "エネルギー(kJ)"} },
                {this._keyList[0], new ObservableCollection<string>(){"エネルギー(kcal)", } },
                {this._keyList[1], new ObservableCollection<string>(){"水分"} },
                {this._keyList[2], new ObservableCollection<string>(){"タンパク質", "アミノ酸組成によるタンパク質"} },
                {this._keyList[3], new ObservableCollection<string>(){"脂質", "トリアシルグリセロール当量" } },
                {this._keyList[4], new ObservableCollection<string>(){"脂肪酸(飽和)", "脂肪酸(一価不飽和)", "脂肪酸(多価不飽和)",} },
                {this._keyList[5], new ObservableCollection<string>(){ "コレステロール", } },
                {this._keyList[6], new ObservableCollection<string>(){ "炭水化物", "利用可能炭水化物"} },
                {this._keyList[7], new ObservableCollection<string>(){ "食物繊維(水溶性)", "食物繊維(不溶性)","食物繊維(総量)"} },
                {this._keyList[8], new ObservableCollection<string>(){ "灰分", } },
                {this._keyList[9], new ObservableCollection<string>(){ "ナトリウム", "カリウム", "カルシウム", "マグネシウム",  } },
                {this._keyList[10], new ObservableCollection<string>(){ "リン", "鉄", "亜鉛", "銅", } },
                {this._keyList[11], new ObservableCollection<string>(){ "マンガン", "ヨウ素", "セレン", "クロム",} },
                {this._keyList[12], new ObservableCollection<string>(){ "モリブデン"} },
                {this._keyList[13], new ObservableCollection<string>(){ "レチノール", "α-カロテン", "β-カロテン", "β-クリプトサチン",  } },
                {this._keyList[14], new ObservableCollection<string>(){ "β-カロテン当量", "レチノール活性当量","ビタミンD", "α-トコフェロール",  } },
                {this._keyList[15], new ObservableCollection<string>(){ "β-トコフェロール", "γ-トコフェロール", "δ-トコフェロール","ビタミンK", } },
                {this._keyList[16], new ObservableCollection<string>(){ "ビタミンB1", "ビタミンB2", "ナイアシン", "ビタミンB6",} },
                {this._keyList[17], new ObservableCollection<string>(){ "ビタミンB12", "葉酸", "パントテン酸", "ビオチン", } },
                {this._keyList[18], new ObservableCollection<string>(){ "ビタミンC"} },
                {this._keyList[19], new ObservableCollection<string>(){"廃棄率"} },
            };

            this.SelectedText.Subscribe((string x) =>
            {
                //var seriesGroup = this.NutrientGroup[x].Select(y => y.Value).ToList();
                var seriesGroup = this.NutrientGroup[x].Select(y =>
                {
                    if (y.UnitKind == UnitKind.mg || y.UnitKind == UnitKind.micro_g)
                        return y.ConvertValue(UnitKind.g);
                    else if (y.UnitKind == UnitKind.kj)
                        return y.ConvertValue(UnitKind.kcal);
                    else
                        return y.Value;
                }).ToList();

                switch (this.NutrientGroup[x].First().UnitKind)
                {
                    case UnitKind.kcal:
                    case UnitKind.kj:
                        this.UnitText.Value = "kcal";
                        break;
                    case UnitKind.g:
                    case UnitKind.mg:
                    case UnitKind.micro_g:
                        this.UnitText.Value = "g";
                        break;
                    case UnitKind.percent:
                        this.UnitText.Value = "%";
                        break;
                    case UnitKind.Undefine:
                    default:
                        this.UnitText.Value = "-";
                        break;
                }

                var labels = this.LabelGroup[x];
                this.Labels = labels;
                this.RaisePropertyChanged(nameof(this.Labels));
                if (this.SeriesCollection?.Chart != null)
                {
                    this.SeriesCollection?.Clear();
                    this.SeriesCollection?.Add(new ColumnSeries() { Title = labels[0] == "廃棄率" ? "廃棄率" : "栄養価", Values = new ChartValues<double>(seriesGroup) });
                    var foo = SeriesCollection.First();
                }
                else
                {
                    this.SeriesCollection = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = labels[0] == "廃棄率" ? "廃棄率" : "栄養価",
                            Values = new ChartValues<double>(seriesGroup),
                        }
                    };
                }
                this.RaisePropertyChanged(nameof(SeriesCollection));
            });
        }

        public void CalcComposition(DateTime date)
        {
            this._analysisPageModel.CalculateComposition(date);

            this.RaisePropertyChanged(nameof(this.CalculateFoodComposition));
            this.UpdateNutrientGroup();
            var keyText = this.SelectedText.Value;
            var seriesGroup = this.NutrientGroup[keyText].Select(y => y.Value).ToList();

            if (this.SeriesCollection?.Chart != null)
            {
                this.SeriesCollection?.Clear();
                this.SeriesCollection?.Add(new ColumnSeries { Title = keyText == "廃棄率" ? "廃棄率" : "栄養価" });
                this.SeriesCollection[0].Values = new ChartValues<double>(seriesGroup);
                this.RaisePropertyChanged(nameof(this.SeriesCollection));
            }
        }

        private void UpdateNutrientGroup()
        {
            this.NutrientGroup = new Dictionary<string, ObservableCollection<Nutrient>>
            {
                //{this._keyList[0], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Energy_kcal, this.CalculateFoodComposition.Value.Energy_kj } },
                {this._keyList[0], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Energy_kcal, } },
                {this._keyList[1], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Water,  } },
                {this._keyList[2], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Protein, this.CalculateFoodComposition.Value.Protein_AminoAcidResidues } },
                {this._keyList[3], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Lipid, this.CalculateFoodComposition.Value.FattyAcid_TriacylGlycerol } },
                {this._keyList[4], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.FattyAcid_Saturated, this.CalculateFoodComposition.Value.FattyAcid_MonoUnsaturated, this.CalculateFoodComposition.Value.FattyAcid_PolyUnsaturated } },
                {this._keyList[5], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Cholesterol} },
                {this._keyList[6], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Carbohydrate, this.CalculateFoodComposition.Value.Carbohydrate_Available } },
                {this._keyList[7], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.DietaryFiber_Soluble, this.CalculateFoodComposition.Value.DietaryFiber_Insoluble, this.CalculateFoodComposition.Value.DietaryFiber_Total  } },
                {this._keyList[8], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Ash } },
                {this._keyList[9], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Sodium, this.CalculateFoodComposition.Value.Potassium, this.CalculateFoodComposition.Value.Calcium, this.CalculateFoodComposition.Value.Magnesium, } },
                {this._keyList[10], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Phosphorus, this.CalculateFoodComposition.Value.Iron, this.CalculateFoodComposition.Value.Zinc, this.CalculateFoodComposition.Value.Copper,  } },
                {this._keyList[11], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Manganese, this.CalculateFoodComposition.Value.Iodine, this.CalculateFoodComposition.Value.Selenium, this.CalculateFoodComposition.Value.Chromium,  } },
                {this._keyList[12], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Molybdenum} },
                {this._keyList[13], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Retinol, this.CalculateFoodComposition.Value.Alpha_Carotene, this.CalculateFoodComposition.Value.Beta_Carotene, this.CalculateFoodComposition.Value.Beta_Cryptoxanthin,  } },
                {this._keyList[14], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Beta_CaroteneEquivalents, this.CalculateFoodComposition.Value.Retinon_ActivityEquivalents, this.CalculateFoodComposition.Value.Vitamin_D, this.CalculateFoodComposition.Value.Alpha_Tocopherol,  } },
                {this._keyList[15], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Beta_Tocopherol, this.CalculateFoodComposition.Value.Gamma_Tocopherol, this.CalculateFoodComposition.Value.Delta_Tocopherol, this.CalculateFoodComposition.Value.Vitamin_K,  } },
                {this._keyList[16], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Thiamin, this.CalculateFoodComposition.Value.Riboflavin, this.CalculateFoodComposition.Value.Niacin, this.CalculateFoodComposition.Value.Vitamin_B6,  } },
                {this._keyList[17], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Vitamin_B12, this.CalculateFoodComposition.Value.Folate, this.CalculateFoodComposition.Value.Pantothenic_Acid, this.CalculateFoodComposition.Value.Biotin,  } },
                {this._keyList[18], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Ascorbic_Acid } },
                {this._keyList[19], new ObservableCollection<Nutrient>(){ this.CalculateFoodComposition.Value.Refuse } },
            };
            System.Diagnostics.Debug.WriteLine($"変更後のコレステロール：{this.CalculateFoodComposition.Value.Cholesterol}");
        }
    }
}
