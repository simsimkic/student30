using Model.Drug;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace HCIProjekat.View.LekoviView.DataView
{
    public class BooleanStatusConverterColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == (int)DrugStatus.Approved)
                return Brushes.Green;
            else if ((int)value == (int)DrugStatus.Waiting)
                return (Brush)(new BrushConverter()).ConvertFrom("#FF047AA6");
            else
                return Brushes.DarkRed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
