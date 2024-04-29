using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace TPW_Project.View.Converter
{
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string colorString)
            {
                try
                {
                    Color color = (Color)ColorConverter.ConvertFromString(colorString);
                    return new SolidColorBrush(color);
                }
                catch (FormatException)
                {
                }
            }

            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
