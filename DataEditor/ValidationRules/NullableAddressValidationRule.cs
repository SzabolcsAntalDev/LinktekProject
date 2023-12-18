using System.Globalization;
using System;
using System.Windows.Controls;

namespace DataEditor.ValidationRules
{
    public class NullableAddressValidationRule : ValidationRule
    {
        private const int MaxAddressLength = 40;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string address = (string)value;

                if (address.Length > MaxAddressLength)
                    return new ValidationResult(false, $"Please enter an address not longer than {MaxAddressLength} characters.");
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Incorrect address: {e.Message}.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
