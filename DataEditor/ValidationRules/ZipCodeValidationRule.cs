using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace DataEditor.ValidationRules
{
    public class ZipCodeValidationRule : ValidationRule
    {
        private const int MaxZipCodeLength = 10;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string zipCode = (string)value;

                if (string.IsNullOrEmpty(zipCode))
                    return new ValidationResult(false, "The zip code cannot be empty.");

                if (zipCode.Length > MaxZipCodeLength)
                    return new ValidationResult(false, $"Please enter a zip code not longer than {MaxZipCodeLength} digits.");

                if (!zipCode.All(char.IsDigit))
                    return new ValidationResult(false, $"The zip code must contain only digits.");
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Incorrect zip code: {e.Message}.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
