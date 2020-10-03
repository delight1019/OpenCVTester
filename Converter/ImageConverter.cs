using OpenCvSharp;
using OpenCVTester.Model;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace OpenCVTester.Converter
{
    public class ImageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Mat imageSource = (Mat)values[0];

            if ((imageSource == null) || (imageSource.Width == 0) || (imageSource.Height == 0))
            {
                return null;
            }

            Bitmap bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(imageSource);

            using (Stream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                stream.Seek(0, SeekOrigin.Begin);
                BitmapDecoder bdc = new BmpBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);

                return bdc.Frames[0];
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
