using OpenCvSharp;
using System;
using System.Globalization;
using System.Windows.Data;

namespace OpenCVTester.Converter
{
    public class ImageSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Mat imageSource = (Mat)value;

            if (imageSource == null)
            {
                return "";
            }

            int width = imageSource.Width;
            int height = imageSource.Height;

            return "Size: " + width.ToString() + " x " + height.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ImageInfoConverter
    {
    }
}
