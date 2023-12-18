using System;
using System.Globalization;
using System.Windows.Data;

namespace DataEditor.Converters
{
    public class PageSizeToDisplayStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return $"(Page size: {(int)value})";
            }
            catch (Exception ex)
            {
                // the exception should be logged into the log file of the application
                return "Cannot determine page size.";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
