using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace lab4.Converters
{
    public class HideIfEqualConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Проверяем, равно ли значение привязанного свойства значению параметра конвертера
            if (Decimal.TryParse(parameter as string, out decimal parameterValue) && Equals(value, parameterValue))
            {
                // Если значения равны, возвращаем Visibility.Collapsed
                return Visibility.Collapsed;
            }
            else
            {
                // Иначе возвращаем Visibility.Visible
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
