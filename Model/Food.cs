using System;
using System.Windows.Media.Imaging;

using Prism.Mvvm;
using Newtonsoft.Json;

namespace MVVM_Refregator.Model
{
    /// <summary>
    /// 食品を表すクラス
    /// </summary>
    public class Food : BindableBase 
    {
        /// <summary>
        /// Id
        /// </summary>
        private uint _id;
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("Id")]
        public uint Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        /// <summary>
        /// 名前
        /// </summary>
        private string _name;
        /// <summary>
        /// 名前
        /// </summary>
        [JsonProperty("Name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        /// <summary>
        /// 消費期限
        /// </summary>
        private DateTime _limitDate;
        /// <summary>
        /// 消費期限
        /// </summary>
        [JsonProperty("LimitDate")]
        public DateTime LimitDate
        {
            get { return _limitDate; }
            set { SetProperty(ref _limitDate, value); }
        }

        /// <summary>
        /// 購入期限
        /// </summary>
        private DateTime _boughtDate;
        /// <summary>
        /// 購入期限
        /// </summary>
        [JsonProperty("BoughtDate")]
        public DateTime BoughtDate
        {
            get { return _boughtDate; }
            set { SetProperty(ref _boughtDate, value); }
        }

        /// <summary>
        /// 種類
        /// </summary>
        private FoodType _kindType;
        /// <summary>
        /// 種類
        /// </summary>
        [JsonProperty("KindType")]
        public FoodType KindType
        {
            get { return _kindType; }
            set { SetProperty(ref _kindType, value); }
        }

        /// <summary>
        /// 状態
        /// </summary>
        private FoodStatusType _statusType;
        /// <summary>
        /// 状態
        /// </summary>
        [JsonProperty("StatusType")]
        public FoodStatusType StatusType
        {
            get { return _statusType; }
            set { SetProperty(ref _statusType, value); }
        }

        /// <summary>
        /// 画像
        /// </summary>
        private BitmapImage _image;
        /// <summary>
        /// 画像
        /// </summary>
        [JsonProperty("Image")]
        public BitmapImage Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        /// <summary>
        /// id用カウンタ
        /// </summary>
        private static uint _id_num = 1;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="limitDate">消費期限</param>
        /// <param name="boughtDate">購入期限</param>
        /// <param name="kindType">種類</param>
        /// <param name="statusType">状態</param>
        /// <param name="image">食材画像</param>
        public Food(string name, DateTime limitDate, DateTime boughtDate, FoodType kindType, BitmapImage image)
        {
            _id = _id_num++;
            _name = name;
            _limitDate = limitDate;
            _boughtDate = boughtDate;
            _kindType = kindType;
            _statusType = FoodStatusType.Fresh;
            _image = image;
        }

        public Food() : this("none", DateTime.Now.AddDays(7), DateTime.Now, FoodType.Other, new BitmapImage(new Uri("/Resources/information_image.png", UriKind.Relative)))
        {
        }

        /// <summary>
        /// 消費期限を過ぎたか
        /// </summary>
        [JsonIgnore()]
        public bool IsOverLimitDate
        {
            get { return LimitDate.Date > BoughtDate.Date; }
        }

    }
}
