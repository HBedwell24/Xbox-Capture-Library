using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace XboxGameClipLibrary.Implementations
{
    public class CredentialValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = Convert.ToString(value);
            Regex regex = new Regex("^[a-z0-9]*$");

            if (string.IsNullOrEmpty(strValue))
            {
                return new ValidationResult(false, $"Value cannot be empty.");
            }
            else if (strValue.Length != 40)
            {
                return new ValidationResult(false, "Value must be 40 characters in length.");
            }
            else if (!regex.IsMatch(strValue))
            {
                return new ValidationResult(false, "Value must only contain lowercase letters and numbers.");
            }
            return ValidationResult.ValidResult;
        }
    }
}