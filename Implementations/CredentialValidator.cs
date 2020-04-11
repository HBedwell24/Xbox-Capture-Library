using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace XboxGameClipLibrary.Implementations
{
    public class CredentialValidator : ValidationRule
    {
        public int MinimumCharacters { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            Regex regex = new Regex("^[a-z0-9]*$");

            if (string.IsNullOrWhiteSpace(charString))
            {
                return new ValidationResult(false, "Value cannot be empty.");
            }
            else if (charString.Length != MinimumCharacters)
            {
                return new ValidationResult(false, $"Value must be {MinimumCharacters} characters in length.");
            }
            else if (!regex.IsMatch(charString))
            {
                return new ValidationResult(false, "Value must only contain lowercase letters and numbers.");
            }
            return ValidationResult.ValidResult;
        }
    }
}