﻿using Prism.Mvvm;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using System.Windows.Media.Imaging;
using MVVM_Refregator.Common;
using System.IO;

namespace MVVM_Refregator.Model
{
    /// <summary>
    /// 食材のコンテナクラス
    /// </summary>
    public class FoodShelfModel  : BindableBase
    {

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
        /// ctor
        /// </summary>
        public FoodShelfModel()
        {
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

            return true;
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
            // todo: ここには、json.netを読み込んでコレクションを生成する処理を実装する
            var food = JsonManager.LoadJsonFrom<ObservableCollection<FoodModel>>();
            //this.FoodCollection = food;
            this.FoodCollection.Clear();
            foreach (var aFood in food)
            {
                this.FoodCollection.Add(aFood);
            }
            return true;
        }

        /// <summary>
        /// 現在の食材データをJsonファイル等の外部バックアップを取ります
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            // todo: とりあえず現在の食材コレクションのデータをJsonファイル等に出力する処理を実装する
            JsonManager.SaveJsonTo<ObservableCollection<FoodModel>>(this.FoodCollection);
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
            }
            return true;
        }

    }
}
