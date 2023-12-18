using System;
using System.Globalization;
using System.Windows.Controls;

namespace DataEditor.ValidationRules
{
    public class SalesValidationRule : ValidationRule
    {
        // the maximum value that can be stored by the Sales column in the database (decimal(16, 2))
        private const decimal MaxValue = 10000000000000000;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (!decimal.TryParse((string)value, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal salesNumber))
                    return new ValidationResult(false, $"The value of sales should be a valid decimal number.");

                if (salesNumber < 0 || salesNumber >= MaxValue)
                    return new ValidationResult(false, $"The value of sales must be between {string.Format("{0:C2}", 0)} and {string.Format("{0:C2}", MaxValue)}.");
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"The value of sales should be a valid decimal number.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
