using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace MovieApp.WPF.ValidationRules
{
    public class EmailRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string email = (string)value;

            if (string.IsNullOrEmpty(email))
                return new ValidationResult(false, "empty email");

            Regex regexForEmail = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                          + "@"
                                          + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");

            Match match = regexForEmail.Match(email);
            if (!match.Success)
                return new ValidationResult(false, "wrong email");

            return ValidationResult.ValidResult;
        }
    }
}