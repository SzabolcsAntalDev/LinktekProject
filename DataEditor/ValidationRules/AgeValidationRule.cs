using System;
using System.Globalization;
using System.Windows.Controls;

namespace DataEditor.ValidationRules
{
    public class AgeValidationRule : ValidationRule
    {
        private const int MinAge = 0;
        private const int MaxAge = 125;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var age = int.Parse((string)value);

                if (age < MinAge || age > MaxAge)
                {
                    return new ValidationResult(false, $"Please enter an age between {MinAge}-{MaxAge}.");
                }
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Please enter a valid age between {MinAge}-{MaxAge}.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
