using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using MVVM_Refregator.Model;

namespace MVVM_Refregator.Common
{
    public class FoodKindLanguageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if(value is FoodType foodType)
            {
                return this.Convert(foodType);
            }

            return DependencyProperty.UnsetValue;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


        private FoodType ConvertBack(string foodKindStr)
        {

            switch (foodKindStr)
            {
                case "お米":
                    return FoodType.Rice;
                case "パン":
                    return FoodType.Bread;
                case "麺":
                    return FoodType.Noodle;
                case "イモ":
                    return FoodType.Potato;
                case "砂糖":
                    return FoodType.Sugar;
                case "スナック":
                    return FoodType.Confectionery;
                case "油":
                    return FoodType.Oil;
                case "豆":
                    return FoodType.Soy;
                case "味噌":
                    return FoodType.Miso;
                case "フルーツ":
                    return FoodType.Fruit;
                case "野菜":
                    return FoodType.Vegetables;
                case "海藻類":
                    return FoodType.Seaweed;
                case "魚介類":
                    return FoodType.SeaFood;
                case "肉類":
                    return FoodType.BeastMeat;
                case "卵":
                    return FoodType.Egg;
                case "乳製品":
                    return FoodType.Milk;
                case "その他乳製品":
                    return FoodType.OtherDairyProducts;
                case "調味料":
                    return FoodType.Seasoning;
                case "その他":
                    return FoodType.Other;
                default:
                    return FoodType.Other;
            }
        }

        private string Convert(FoodType foodType)
        {
            switch (foodType)
            {
                case FoodType.Rice:
                    return "お米";
                case FoodType.Bread:
                    return "パン";
                case FoodType.Noodle:
                    return "麺";
                case FoodType.Potato:
                    return "イモ";
                case FoodType.Sugar:
                    return "砂糖";
                case FoodType.Confectionery:
                    return "スナック";
                case FoodType.Oil:
                    return "油";
                case FoodType.Soy:
                    return "豆";
                case FoodType.Miso:
                    return "味噌";
                case FoodType.Fruit:
                    return "フルーツ";
                case FoodType.Vegetables:
                    return "野菜";
                case FoodType.Seaweed:
                    return "海藻類";
                case FoodType.SeaFood:
                    return "魚介類";
                case FoodType.BeastMeat:
                    return "肉類";
                case FoodType.Egg:
                    return "卵";
                case FoodType.Milk:
                    return "乳製品";
                case FoodType.OtherDairyProducts:
                    return "その他乳製品";
                case FoodType.Seasoning:
                    return "調味料";
                case FoodType.Other:
                    return "その他";
                default:
                    return "-";
            }
        }

    }
}
