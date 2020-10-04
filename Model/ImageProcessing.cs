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

        public Mat Crop(Mat imageSource, Rect rect)
        {
            return new Mat(imageSource, rect);
        }
    }
}
