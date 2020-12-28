using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Projekt.Converters
{
    class IntegerToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int tmp = (int)value % 2;
            return ((int)tmp == 0) ? "#FFFFFF" : "#D4BDD9";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
