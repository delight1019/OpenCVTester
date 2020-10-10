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
        private int _brightness;
        private double _contrast;
        private int _meanBlur;
        private GaussianBlur _gaussianBlur;
        private double _sharpening;

        private void Initialize()
        {
            _brightness = 0;
            _contrast = 0;
            _meanBlur = 1;
            _gaussianBlur = new GaussianBlur(1, 1.0);
            _sharpening = 0;
        }
        private Mat MakeCurrentImage()
        {
            Mat tempImage = _originImage;

            tempImage = _imageProcessing.ControlBrightness(tempImage, _brightness);
            tempImage = _imageProcessing.ChangeContrast(tempImage, _contrast);
            tempImage = _imageProcessing.ChangeMeanBlur(tempImage, _meanBlur);
            tempImage = _imageProcessing.ChangeGaussianBlur(tempImage, _gaussianBlur);
            tempImage = _imageProcessing.Sharpen(tempImage, _sharpening);

            _currentImage = tempImage;
            return _currentImage;
        }

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
        public int GetBrightness()
        {
            return _brightness;
        }
        public double GetContrast()
        {
            return _contrast;
        }
        public int GetMeanBlur()
        {
            return _meanBlur;
        }
        public int GetGaussianBlurSize()
        {
            return _gaussianBlur.kernalSize;
        }
        public double GetGaussianBlurSigma()
        {
            return _gaussianBlur.sigma;
        }
        public double GetSharpening()
        {
            return _sharpening;
        }
        public Mat CalculateHistogram()
        {
            _histogram = _imageProcessing.CalculateHistogram(_currentImage);
            return _histogram;
        }
        public Mat ControlBrightness(int value)
        {
            _brightness = value;
            return MakeCurrentImage();
        }
        public Mat ChangeContrast(double value)
        {
            _contrast = value;
            return MakeCurrentImage();
        }
        public Mat ChangeMeanBlur(int value)
        {
            _meanBlur = value;
            return MakeCurrentImage();
        }
        public Mat ChangeGaussianBlurSize(int value)
        {
            _gaussianBlur.kernalSize = value;
            return MakeCurrentImage();
        }
        public Mat ChangeGaussianBlurSigma(double value)
        {
            _gaussianBlur.sigma = value;
            return MakeCurrentImage();
        }
        public Mat Sharpen(double value)
        {
            _sharpening = value;
            return MakeCurrentImage();
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
        public Mat NormalizeHistogram()
        {
            _currentImage = _currentImage.Normalize(0, 255, NormTypes.MinMax);
            return _currentImage;
        }
        public Mat EqualizeHistogram()
        {
            _currentImage = _currentImage.EqualizeHist();
            return _currentImage;
        }

        public ImageModel()
        {
            _imageProcessing = new ImageProcessing();
            _originImage = new Mat();
            _currentImage = new Mat();
            _histogram = new Mat();

            Initialize();
        }
    }
}
