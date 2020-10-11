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

    public class ImageWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Mat imageSource = (Mat)value;

            if (imageSource == null)
            {
                return "";
            }

            int coefficient = 1;

            if (parameter == null)
            {
                coefficient = 1;
            }
            else
            {
                int.TryParse(parameter.ToString(), out coefficient);
            }

            return imageSource.Width * coefficient;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ImageHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Mat imageSource = (Mat)value;

            if (imageSource == null)
            {
                return "";
            }

            int coefficient = 1;

            if (parameter == null)
            {
                coefficient = 1;
            }
            else
            {
                int.TryParse(parameter.ToString(), out coefficient);
            }

            return imageSource.Height * coefficient;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
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
