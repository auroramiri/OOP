using System;
using System.Globalization;
using System.Windows.Data;

namespace lab4.Converters
{
    public class WidthToColumnsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double windowWidth = (double)value;
            double itemWidth = 220; // ширина элемента с отступами
            int columns = (int)Math.Floor(windowWidth / itemWidth);
            return columns > 0 ? columns : 1; // минимум один столбец
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
