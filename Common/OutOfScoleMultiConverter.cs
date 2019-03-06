using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MVVM_Refregator.Common
{
    class OutOfScopeMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.All(x => x is DateTime) == false)
            {
                return DependencyProperty.UnsetValue;
            }
            //throw new NotImplementedException();
            var date = (DateTime)values[0];
            var displayDate = (DateTime)values[1];

            if (date.Month == displayDate.Month)
            {
                return new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            }

            return new SolidColorBrush(Color.FromArgb(255, 100, 100, 100));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
