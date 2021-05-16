using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MovieApp.WPF.ValidationRules
{
    public class PasswordRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string password = (string)value;

            if(string.IsNullOrEmpty(password))
                return new ValidationResult(false, "empty password");

            if (password.Length < 5 || password.Length > 64 )
                return new ValidationResult(false, "length should be between 5 and 64");

            if(!password.Any(char.IsDigit))
                return new ValidationResult(false, "at least 1 digit");

            if (!password.Any(char.IsUpper))
                return new ValidationResult(false, "at least 1 upper letter");

            if (!password.Any(char.IsLower))
                return new ValidationResult(false, "at least 1 lower letter");
                    
            return ValidationResult.ValidResult;
        }
    }
}
