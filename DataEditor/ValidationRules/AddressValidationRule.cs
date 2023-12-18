using System.Globalization;
using System.Windows.Controls;

namespace DataEditor.ValidationRules
{
    public class AddressValidationRule : NullableAddressValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var baseResult = base.Validate(value, cultureInfo);

            if (baseResult != ValidationResult.ValidResult)
                return baseResult;

            return string.IsNullOrEmpty((string)value)
                ? new ValidationResult(false, "The address cannot be empty.")
                : ValidationResult.ValidResult;
        }
    }
}
