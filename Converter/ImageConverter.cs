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
            if (!values[0].GetType().Equals(typeof(Mat)))
            {
                return null;
            }

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

    public class HistogramConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!value.GetType().Equals(typeof(Mat)))
            {
                return null;
            }            

            Mat imageSource = value as Mat;

            if ((imageSource == null) || (imageSource.Width == 0) || (imageSource.Height == 0))
            {
                return null;
            }

            int width = 512;
            int height = 300;

            double minVal, maxVal;
            Cv2.MinMaxLoc(imageSource, out minVal, out maxVal);
            imageSource = imageSource * (maxVal != 0 ? height / maxVal : 0.0);

            Mat histogram = new Mat(new OpenCvSharp.Size(width, height), MatType.CV_8UC3, Scalar.All(255));
            int[] histogramSize = { 256 };
            Scalar color = Scalar.All(100);

            for (int i = 0; i < 256; ++i)
            {
                int binWidth = (int)((double)width / 256);
                histogram.Rectangle(
                    new OpenCvSharp.Point(i * binWidth, histogram.Rows - (int)(imageSource.Get<float>(i))),
                    new OpenCvSharp.Point((i + 1) * binWidth, histogram.Rows),
                    color,
                    -1);
            }

            Bitmap bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(histogram);

            using (Stream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                stream.Seek(0, SeekOrigin.Begin);
                BitmapDecoder bdc = new BmpBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);

                return bdc.Frames[0];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
