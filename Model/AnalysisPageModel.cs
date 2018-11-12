using System.Collections.ObjectModel;
using MVVM_Refregator.Common;
using Prism.Mvvm;

namespace MVVM_Refregator.Model
{
    public class AnalysisPageModel : BindableBase
    {
        /// <summary>
        /// 食材コレクション用
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
            this.FoodCompositions = JsonManager.ReadJson();

            this.InitFoodCollection();
        }

        /// <summary>
        /// バインド用コレクションの初期化
        /// </summary>
        public void InitFoodCollection()
        {
            this.AllFoods = new ObservableCollection<FoodModel>(_foodShelfModel.FoodCollection);
            this.AnalysisFoods = new ObservableCollection<FoodModel>();
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
    }
}
