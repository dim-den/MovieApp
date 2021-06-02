using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace MovieApp.WPF.ValidationRules
{
    public class NameRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string name = (string)value;

            if (string.IsNullOrEmpty(name))
                return new ValidationResult(false, "empty field");

            Regex regexForEmail = new Regex("[a-zA-Zа-яА-Я-]{3,64}$");

            Match match = regexForEmail.Match(name);
            if (!match.Success)
                return new ValidationResult(false, "wrong field, size [2;64] letters");

            return ValidationResult.ValidResult;
        }
    }
}