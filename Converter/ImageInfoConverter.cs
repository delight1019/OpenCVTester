using OpenCvSharp;
using OpenCVTester.Common;
using OpenCVTester.ViewModel;
using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace OpenCVTester.Converter
{
    public static class ImageTypeParser
    {
        public static string ToString(ImageType imageType)
        {
            Type type = imageType.GetType();
            FieldInfo fieldInfo = type.GetField(imageType.ToString());
            StringValue attribute = fieldInfo.GetCustomAttribute(typeof(StringValue), false) as StringValue;

            return attribute.Value;
        }
    }

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

    public class ImageHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ImageTypeParser.ToString((ImageType)value);
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
