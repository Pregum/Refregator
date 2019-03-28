using Prism.Mvvm;
using System;
using System.Linq;
using System.Collections.ObjectModel;

using System.Windows.Media.Imaging;
using MVVM_Refregator.Common;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

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

        public async Task<bool> LoadAsync(string jsonFilePath = @"food_data.json")
        {
            ObservableCollection<FoodModel> food = default(ObservableCollection<FoodModel>);
            try
            {
                food = await JsonManager.LoadJsonFromAsync<ObservableCollection<FoodModel>>();
            }
            catch (Exception)
            {
                var result = MessageBox.Show("食材データの読み込みに失敗しました。食材データを初期化してもよろしいでしょうか?", "Food Calendar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var storage = Windows.Storage.ApplicationData.Current.LocalFolder;
                    var file = await storage.CreateFileAsync(jsonFilePath, Windows.Storage.CreationCollisionOption.ReplaceExisting);
                    food = default(ObservableCollection<FoodModel>);
                }
                else
                {
                    MessageBox.Show("アプリを終了します。", "Food Calendar", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            finally
            {
                this.FoodCollection = food ?? new ObservableCollection<FoodModel>();
            }
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

        public async Task<bool> SaveAsync(string destinationPath = @"food_data.json")
        {
            return await JsonManager.SaveJsonToAsync(this.FoodCollection, destinationPath);
        }

        public bool Save(Action<string, string> pathToTextFunc, string path)
        {
            JsonManager.SaveJsonTo(this.FoodCollection, pathToTextFunc, path);
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
        /// <param name="hasUsed">食材は使用済みか確認用フラグ</param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(string name, DateTime limitDate, DateTime boughtDate, FoodType kindType, BitmapImage image, bool hasUsed)
        {
            var newFood = new FoodModel(name, limitDate, boughtDate, kindType, image, hasUsed);
            this.FoodCollection.Add(newFood);
            await this.SaveAsync();

            return true;
        }

        /// <summary>
        /// 新しい食材を追加します
        /// </summary>
        /// <param name="food">追加される食材</param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(FoodModel food)
        {
            this.FoodCollection.Add(food);
            var result = await this.SaveAsync();
            return result;
        }

        /// <summary>
        /// 指定したIdに一致する食材オブジェクトを削除します
        /// </summary>
        /// <param name="id">食材に紐づいたId</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(uint id)
        {
            if (this.FoodCollection.Count(x => x.Id == id) == 1)
            {
                this.FoodCollection.Remove(this.FoodCollection.Single(x => x.Id == id));
                //this.Save();
                await this.SaveAsync();
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
        /// <param name="usedDate">使用日</param>
        /// <param name="kindType">食材の種類</param>
        /// <param name="image">食材画像</param>
        /// <param name="hasUsed">使用済みか判別フラグ</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(uint id, string name, DateTime limitDate, DateTime usedDate, FoodType kindType, BitmapImage image, bool hasUsed)
        {
            if (this.FoodCollection.Count(x => x.Id == id) == 1)
            {
                // todo: 購入日より消費期限が必ず遅いことを示すバリデーションを追加しておく
                var targetFood = this.FoodCollection.First(x => x.Id == id);
                targetFood.Name = name;
                targetFood.LimitDate = limitDate;
                targetFood.UsedDate = usedDate;
                targetFood.KindType = kindType;
                targetFood.Image = image;
                targetFood.HasUsed = hasUsed;

                //this.Save();
                await this.SaveAsync();
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
        public async Task<bool> UpdateAsync(FoodModel food)
        {
            //return Update(food.Id, food.Name, food.LimitDate, food.BoughtDate, food.KindType, food.Image);
            return await this.UpdateAsync(food.Id, food.Name, food.LimitDate, food.UsedDate, food.KindType, food.Image, food.HasUsed);
        }

        /// <summary>
        /// 食材を使用済みに設定します
        /// </summary>
        /// <param name="targetFood">対象の食品</param>
        public async Task SetUsedAsync(FoodModel targetFood)
        {
            var destinationFood = this.FoodCollection.SingleOrDefault(x => x.Id == targetFood.Id);

            if (destinationFood != null)
            {
                destinationFood.HasUsed = true;
                destinationFood.UsedDate = DateTime.Today;

                //this.Save();
                await this.SaveAsync();
                this.RaisePropertyChanged(nameof(this.FoodCollection));
            }
        }
    }
}
