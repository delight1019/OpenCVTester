using OpenCvSharp;

namespace OpenCVTester.Model
{
    public class ImageProcessing
    {
        public Mat ControlBrightness(Mat imageSource, int value)
        {
            if (imageSource == null)
            {
                return null;
            }

            return imageSource + value;
        }
        public Mat ChangeContrast(Mat imageSource, double value)
        {
            if (imageSource == null)
            {
                return null;
            }

            return (1 + value) * imageSource - 128 * value;
        }
        public Mat ChangeMeanBlur(Mat imageSource, int value)
        {
            if (imageSource == null)
            {
                return null;
            }

            Size size = new Size(value, value);

            return imageSource.Blur(size);
        }
        public Mat ChangeGaussianBlur(Mat imageSource, int value)
        {
            if (imageSource == null)
            {
                return null;
            }

            Size size = new Size(value, value);

            return imageSource.GaussianBlur(size, 2.0);
        }
        public Mat Crop(Mat imageSource, Rect rect)
        {
            return new Mat(imageSource, rect);
        }
        public Mat CalculateHistogram(Mat imageSource)
        {            
            Mat histogram = new Mat();
            int[] histSize = { 256 };
            Rangef[] rangef = { new Rangef(0, 256), };

            Cv2.CalcHist(new Mat[] { imageSource }, new int[] { 0 }, null, histogram, 1, histSize, rangef);
            return histogram;
        }
    }
}
