using OpenCVTester.ViewModel;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OpenCVTester.Converter
{
    public class BinaryOperationImagePanelVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ImageType)value)
            {
                case ImageType.WEIGHTED_SUM:
                    return Visibility.Visible;
                case ImageType.SUBTRACT:
                    return Visibility.Visible;
                case ImageType.ABS_DIFF:
                    return Visibility.Visible;
                default:
                    return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SumWeightVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((ImageType)value == ImageType.WEIGHTED_SUM)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
