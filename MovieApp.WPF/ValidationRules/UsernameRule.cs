using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace MovieApp.WPF.ValidationRules
{
    public class UsernameRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string username = (string)value;

            if (string.IsNullOrEmpty(username))
                return new ValidationResult(false, "empty username");

            Regex regexForEmail = new Regex("^[a-zA-Z][a-zA-Z0-9._]{3,32}$");

            Match match = regexForEmail.Match(username);
            if (!match.Success)
                return new ValidationResult(false, "wrong username ([4;32] symbols, first letter, then letter | digit | _.)");

            return ValidationResult.ValidResult;
        }
    }
}