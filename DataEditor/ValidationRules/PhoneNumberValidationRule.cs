using System.Globalization;
using System.Linq;
using System;
using System.Windows.Controls;

namespace DataEditor.ValidationRules
{
    public class PhoneNumberValidationRule : ValidationRule
    {
        private const int MaxPhoneNumberLength = 20;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string phoneNumber = (string)value;

                if (string.IsNullOrEmpty(phoneNumber))
                    return new ValidationResult(false, "The phone number cannot be empty.");

                if (phoneNumber.Length > MaxPhoneNumberLength)
                    return new ValidationResult(false, $"Please enter a phone number not longer than {MaxPhoneNumberLength} digits.");

                if (!phoneNumber.All(char.IsDigit))
                    return new ValidationResult(false, $"The phone number must contain only digits.");
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Incorrect phone number: {e.Message}.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
