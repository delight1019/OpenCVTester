using OpenCvSharp;

namespace OpenCVTester.Model
{
    public struct GaussianBlur
    {
        public int kernalSize;
        public double sigma;

        public GaussianBlur(int size, double sigma)
        {
            this.kernalSize = size;
            this.sigma = sigma;
        }
    }

    public struct BilateralFilter
    {
        public int sigmaColor;
        public int sigmaSpace;

        public BilateralFilter(int color, int space)
        {
            this.sigmaColor = color;
            this.sigmaSpace = space;
        }
    }

    public class ImageProcessing
    {
        private GaussianBlur unsharpMaskFactor = new GaussianBlur(0, 2.0);
        private GaussianBlur sketchFilterFactor = new GaussianBlur(0, 5.0);

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
        public Mat ChangeGaussianBlur(Mat imageSource, GaussianBlur gaussianBlur)
        {
            if (imageSource == null)
            {
                return null;
            }

            Size size = new Size(gaussianBlur.kernalSize, gaussianBlur.kernalSize);

            return imageSource.GaussianBlur(size, gaussianBlur.sigma);
        }
        public Mat Sharpen(Mat imageSource, double weight)
        {
            if (imageSource == null)
            {
                return null;
            }

            Mat unsharpMask = ChangeGaussianBlur(imageSource, unsharpMaskFactor);

            return (1 + weight) * imageSource - weight * unsharpMask;
        }
        public Mat ApplyMedianFilter(Mat imageSource, int size)
        {
            if (imageSource == null)
            {
                return null;
            }

            return imageSource.MedianBlur(size);
        }
        public Mat ApplyBilateralFilter(Mat imageSource, BilateralFilter bilateralFilter)
        {
            if (imageSource == null)
            {
                return null;
            }

            return imageSource.BilateralFilter(-1, bilateralFilter.sigmaColor, bilateralFilter.sigmaSpace);
        }
        public Mat ApplySketchFilter(Mat imageSource)
        {
            if (imageSource == null)
            {
                return null;
            }

            Mat blurredImage = ChangeGaussianBlur(imageSource, sketchFilterFactor);

            return 255 * (imageSource / blurredImage);
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
