using System;
using System.Globalization;
using System.Windows.Controls;

namespace DataEditor.ValidationRules
{
    public class NameValidationRule : ValidationRule
    {
        private const int MaxNameLength = 30;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string name = (string)value;

                if (string.IsNullOrEmpty(name))
                    return new ValidationResult(false, "The name cannot be empty.");

                if (name.Length > MaxNameLength)
                    return new ValidationResult(false, $"Please enter a name not longer than {MaxNameLength} characters.");
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Incorrect name: {e.Message}.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
