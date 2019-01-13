using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using MVVM_Refregator.Model;
using MVVM_Refregator.ViewModel;

using Reactive.Bindings;

namespace MVVM_Refregator.Common
{
    public class DateFoodsMapMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var targetDate = (DateTime)values[0];

            if (values[1] is ReadOnlyReactiveCollection<FoodModel> foodCollection)
            {
                //System.Diagnostics.Debug.WriteLine($" updating Calendar.ItemsSource  ...");

                var chunkItem = foodCollection.Where(x => x.LimitDate.Date == targetDate.Date);

                if (chunkItem.Any())
                {
                    System.Diagnostics.Debug.WriteLine($" target food is confirm : {chunkItem.Count()} , in {targetDate.Date}  ");
                }

                return chunkItem;
            }

            System.Diagnostics.Debug.WriteLine($"target food is nothing)");

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
