using System;
using System.Globalization;
using System.Windows.Controls;

namespace DataEditor.ValidationRules
{
    public class CityNameValidationRule :ValidationRule
    {
        private const int MaxCityNameLength = 50;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string address = (string)value;

                if (string.IsNullOrEmpty(address))
                    return new ValidationResult(false, "The name of the city cannot be empty.");

                if (address.Length > MaxCityNameLength)
                    return new ValidationResult(false, $"Please enter a city name not longer than {MaxCityNameLength} characters.");
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Incorrect city name: {e.Message}.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
