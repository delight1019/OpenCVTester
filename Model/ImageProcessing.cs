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

            //Mat matToAdd = new Mat(imageSource.Height, imageSource.Width, imageSource.Type(), value);
            //imageSource.Accumulate(matToAdd);            

            return imageSource + value;
        }
    }
}
