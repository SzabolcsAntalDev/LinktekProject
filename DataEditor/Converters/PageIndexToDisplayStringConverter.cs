using System;
using System.Globalization;
using System.Windows.Data;

namespace DataEditor.Converters
{
    public class PageIndexToDisplayStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is int pageIndex)
                    return pageIndex + 1;

                // value is null when TotalNumberOfPage is not initialized yet
                return "?";
            }
            catch (Exception ex)
            {
                // the exception should be logged into the log file of the application
                return "?";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
