using System;
using System.Globalization;
using System.Windows.Data;

namespace MovieApp.WPF.Converters
{
    public class ScoreToStarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double avgScore = (double)value;
            int starNumber = Int32.Parse((string)parameter);

            return avgScore >= starNumber;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}