using OpenCVTester.ViewModel;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OpenCVTester.Converter
{
    public class ImagePanelVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (((ImageType)value == ImageType.IMAGE_1) || ((ImageType)value == ImageType.IMAGE_2))
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

    public class WeightedSumImagePanelVisibilityConverter : IValueConverter
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
