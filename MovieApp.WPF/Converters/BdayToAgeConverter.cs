using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MovieApp.WPF.Converters
{
    public class BdayToAgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime bday = (DateTime)value;
            var today = DateTime.Today;
            var age = today.Year - bday.Year;

            if (bday.Date > today.AddYears(-age)) age--;

            return age;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
