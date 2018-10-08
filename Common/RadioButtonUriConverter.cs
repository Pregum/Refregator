using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using MVVM_Refregator.Model;

namespace MVVM_Refregator.Common
{
    public class RadioButtonUriConverter : IValueConverter
    {
        /// <summary>
        /// View -> ViewModelへの変換
        /// </summary>
        /// <param name="value">ソースの値</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var parameterString = parameter as string;
            if (parameterString == null)
            {
                return System.Windows.DependencyProperty.UnsetValue;
            }

            if (Enum.IsDefined(value.GetType(), value) == false)
            {
                return System.Windows.DependencyProperty.UnsetValue;
            }

            object parameterValue = Enum.Parse(value.GetType(), parameterString);

            return (int)parameterValue == (int)value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameterString = parameter as string;
            return parameterString == null ? System.Windows.DependencyProperty.UnsetValue : Enum.Parse(targetType, parameterString);
        }
    }
}
