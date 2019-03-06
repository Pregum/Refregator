using System;
using System.Windows.Media.Imaging;

using Prism.Mvvm;
using Newtonsoft.Json;

namespace MVVM_Refregator.Model
{
    /// <summary>
    /// 食品を表すクラス
    /// </summary>
    public class FoodModel : BindableBase
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

        ///// <summary>
        ///// 購入期限
        ///// </summary>
        //private DateTime _boughtDate;
        ///// <summary>
        ///// 購入期限
        ///// </summary>
        //[JsonProperty("BoughtDate")]
        //public DateTime BoughtDate
        //{
        //    get { return _boughtDate; }
        //    set { SetProperty(ref _boughtDate, value); }
        //}

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
        /// 使用日
        /// </summary>
        private DateTime _usedDate;
        /// <summary>
        /// 使用日
        /// </summary>
        [JsonProperty("UsedDate")]
        public DateTime UsedDate
        {
            get { return _usedDate; }
            set { SetProperty(ref _usedDate, value); }
        }

        /// <summary>
        /// この食材は使用されたか判別するフラグ
        /// </summary>
        private bool _hasUsed;
        /// <summary>
        /// この食材は使用されたか判別するフラグ
        /// </summary>
        [JsonProperty("HasUsed")]
        public bool HasUsed
        {
            get { return _hasUsed; }
            set { SetProperty(ref _hasUsed, value); }
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
        [JsonIgnore]
        public BitmapImage Image
        {
            get { return _image; }
            set
            {
                if (value != null)
                {
                SetProperty(ref _image, value);
                    this.ImageString = this.Image.UriSource.ToString().StartsWith("/") ? this.Image.UriSource.ToString() : "/" + this.Image.UriSource.ToString();
                }
            }
        }

        /// <summary>
        /// ImageのUri
        /// </summary>
        private string _imageString;
        /// <summary>
        /// ImageのUri
        /// </summary>
        [JsonProperty("Image")]
        public string ImageString
        {
            get { return _imageString; }
            set
            {
                SetProperty(ref _imageString, value);
                this._image = new BitmapImage(new Uri(value, UriKind.Relative));
            }
        }

        /// <summary>
        /// id用カウンタ
        /// </summary>
        [JsonIgnore]
        private static uint _id_num = 1;

        /// <summary>
        /// 次に設定される
        /// </summary>
        [JsonIgnore]
        public static uint NextId { get { return _id_num; } }

        /// <summary>
        /// idの値を1つ進める
        /// </summary>
        public static void IncrementId() { _id_num++; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="limitDate">消費期限</param>
        /// <param name="usedDate">使用日</param>
        /// <param name="kindType">種類</param>
        /// <param name="statusType">状態</param>
        /// <param name="image">食材画像</param>
        /// <param name="hasUsed">使用されたか</param>
        public FoodModel(string name, DateTime limitDate, DateTime usedDate, FoodType kindType, BitmapImage image, bool hasUsed)
        {
            _id = _id_num++;
            _name = name;
            _limitDate = limitDate;
            //_boughtDate = boughtDate;
            _usedDate = usedDate;
            _kindType = kindType;
            _statusType = FoodStatusType.Fresh;
            _image = image;
            _imageString = _image.UriSource.OriginalString;
            _hasUsed = hasUsed;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="food">コピー元食材</param>
        public FoodModel(FoodModel food) : this(food.Name, food.LimitDate, food.UsedDate, food.KindType, food.Image, food.HasUsed) { }

        /// <summary>
        /// 開発用ctor
        /// </summary>
        public FoodModel() : this("created_" + DateTime.Today.ToShortDateString(), DateTime.Now.AddDays(7), DateTime.Now, FoodType.Other, new BitmapImage(new Uri("/Resources/information_image.png", UriKind.Relative)), false)
        {
        }

        public override string ToString()
        {
            return $"Id : {Id}, Name : {Name}, UsedDate : {UsedDate}, HasUsed : {HasUsed}";
        }

    }
}
