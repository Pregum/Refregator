using Prism.Mvvm;
using System;
using System.Linq;
using System.Collections.ObjectModel;

using System.Windows.Media.Imaging;
using MVVM_Refregator.Common;
using System.IO;

namespace MVVM_Refregator.Model
{
    /// <summary>
    /// 食材管理クラス
    /// </summary>
    public class FoodShelfModel : BindableBase
    {

        /// <summary>
        /// 食材のコレクション
        /// </summary>
        private ObservableCollection<FoodModel> _foodCollection = new ObservableCollection<FoodModel>();

        /// <summary>
        /// 食材のコレクション
        /// </summary>
        public ObservableCollection<FoodModel> FoodCollection
        {
            get { return _foodCollection; }
            private set { SetProperty(ref _foodCollection, value); }
        }

        /// <summary>
        /// singleton
        /// </summary>
        private static FoodShelfModel _instance = null;

        /// <summary>
        /// singleton
        /// </summary>
        /// <returns></returns>
        public static FoodShelfModel GetInstance()
        {
            _instance = _instance ?? new FoodShelfModel();
            return _instance;
        }

        /// <summary>
        /// ctor
        /// </summary>
        private FoodShelfModel()
        {
        }

        /// <summary>
        /// 食材情報が保存されているJsonファイルを読み込んでデータをロードします
        /// </summary>
        /// <param name="jsonFilePath">Jsonのファイルパス</param>
        /// <returns></returns>
        public bool Load(string jsonFilePath = @"food_data.json")
        {
            if (File.Exists(jsonFilePath) == false)
            {
                return false;
            }

            var food = JsonManager.LoadJsonFrom<ObservableCollection<FoodModel>>();
            this.FoodCollection = food;
            return true;
        }

        /// <summary>
        /// 現在の食材データをJsonファイル等の外部バックアップを取ります
        /// </summary>
        /// <param name="destinationPath">出力先のファイルパス</param>
        /// <returns></returns>
        public bool Save(string destinationPath = @"food_data.json")
        {
            JsonManager.SaveJsonTo(this.FoodCollection, destinationPath);
            return true;
        }

        /// <summary>
        /// 新しい食材を追加します
        /// </summary>
        /// <param name="name">食材名</param>
        /// <param name="limitDate">賞味期限</param>
        /// <param name="boughtDate">購入日</param>
        /// <param name="kindType">食材のタイプ</param>
        /// <param name="image">食材画像</param>
        /// <returns></returns>
        public bool Create(string name, DateTime limitDate, DateTime boughtDate, FoodType kindType, BitmapImage image)
        {
            var newFood = new FoodModel(name, limitDate, boughtDate, kindType, image);
            this.FoodCollection.Add(newFood);
            this.Save();

            return true;
        }

        /// <summary>
        /// 新しい食材を追加します
        /// </summary>
        /// <param name="food">追加される食材</param>
        /// <returns></returns>
        public bool Create(FoodModel food)
        {
            this.FoodCollection.Add(food);
            this.Save();

            return true;
        }

        /// <summary>
        /// 指定したIdに一致する食材オブジェクトを削除します
        /// </summary>
        /// <param name="id">食材に紐づいたId</param>
        /// <returns></returns>
        public bool Delete(uint id)
        {
            if (this.FoodCollection.Count(x => x.Id == id) == 1)
            {
                this.FoodCollection.Remove(this.FoodCollection.Single(x => x.Id == id));
                this.Save();
            }
            else
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 指定したIdに一致する食材オブジェクトの中身を変更します。
        /// </summary>
        /// <param name="id">食材オブジェクトに紐づいたId</param>
        /// <param name="name">食材名</param>
        /// <param name="limitDate">賞味期限日</param>
        /// <param name="boughtDate">購入日</param>
        /// <param name="kindType">食材の種類</param>
        /// <param name="image">食材画像</param>
        /// <returns></returns>
        public bool Update(uint id, string name, DateTime limitDate, DateTime boughtDate, FoodType kindType, BitmapImage image)
        {
            if (this.FoodCollection.Count(x => x.Id == id) == 1)
            {
                // todo: 購入日より消費期限が必ず遅いことを示すバリデーションを追加しておく
                var targetFood = this.FoodCollection.First(x => x.Id == id);
                targetFood.Name = name;
                targetFood.LimitDate = limitDate;
                targetFood.BoughtDate = boughtDate;
                targetFood.KindType = kindType;
                targetFood.Image = image;

                this.Save();
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 食材オブジェクトの中身を変更します
        /// </summary>
        /// <param name="food">変更後の食材</param>
        /// <see cref="Update(uint, string, DateTime, DateTime, FoodType, BitmapImage)"/>
        /// <returns></returns>
        public bool Update(FoodModel food)
        {
            return Update(food.Id, food.Name, food.LimitDate, food.BoughtDate, food.KindType, food.Image);
        }

    }
}
