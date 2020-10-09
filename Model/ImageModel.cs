using System.IO;
using OpenCvSharp;

namespace OpenCVTester.Model
{
    public class ImageModel
    {
        private ImageProcessing _imageProcessing;
        private Mat _originImage;
        private Mat _currentImage;
        private Mat _histogram;

        public Mat RegisterImage(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                _originImage = new Mat(imagePath, ImreadModes.Grayscale);
                _currentImage = _originImage;
                return _currentImage;
            }
            else
            {
                return null;
            }
        }
        public Mat GetImage()
        {
            return _currentImage;
        }
        public Mat GetHistogram()
        {
            return _histogram;
        }
        public Mat CalculateHistogram()
        {
            _histogram = _imageProcessing.CalculateHistogram(_currentImage);
            return _histogram;
        }
        public Mat ControlBrightness(int value)
        {
            _currentImage = _imageProcessing.ControlBrightness(_originImage, value);
            return _currentImage;
        }
        public Mat AddWeightedImages(Mat image1, Mat image2, double alpha, double beta)
        {
            Cv2.AddWeighted(image1, alpha, image2, beta, 0, _originImage);
            _currentImage = _originImage;
            return _currentImage;
        }
        public Mat SubtractImages(Mat image1, Mat image2)
        {
            Cv2.Subtract(image1, image2, _originImage);
            _currentImage = _originImage;
            return _currentImage;
        }
        public Mat AbsDiff(Mat image1, Mat image2)
        {
            Cv2.Absdiff(image1, image2, _originImage);
            _currentImage = _originImage;
            return _currentImage;
        }

        public ImageModel()
        {
            _imageProcessing = new ImageProcessing();
            _originImage = new Mat();
            _currentImage = new Mat();
            _histogram = new Mat();
        }
    }
}
