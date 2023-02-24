using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HealthCare.View.Patient
{
    class DateConcatenateConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime1 = (DateTime)values[0];
            DateTime dateTime2 = (DateTime)values[1];
            return dateTime1.ToString("t") + " - " + dateTime2.ToString("t");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
