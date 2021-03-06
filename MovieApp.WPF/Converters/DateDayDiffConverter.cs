using System;
using System.Globalization;
using System.Windows.Data;

namespace MovieApp.WPF.Converters
{
    public class DateDayDiffConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime day = (DateTime)value;

            return (int)((day - DateTime.Now).TotalDays);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}