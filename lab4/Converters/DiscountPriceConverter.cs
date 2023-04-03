using System;
using System.Globalization;
using System.Windows.Data;

namespace lab4.Converters
{
    public class DiscountPriceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is double price && values[1] is int discount)
            {
                if (discount > 0)
                {
                    double discountedPrice = price * (1 - (double)discount / 100);
                    return $"${price:#.##} ({discountedPrice:$#.##})";
                }
                else
                {
                    return $"${price:#.##}";
                }
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
