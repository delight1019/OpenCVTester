using System.IO;
using OpenCvSharp;

namespace OpenCVTester.Model
{
    public enum ImageCode
    {
        LEFT, RIGHT
    }

    public class ImageModel
    {
        private ImageCode _code;
        private Mat _originImage;
        private Mat _currentImage;

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
        public ImageCode GetCode()
        {
            return _code;
        }

        public ImageModel(ImageCode code)
        {
            _code = code;
            _originImage = new Mat();
            _currentImage = new Mat();
        }
    }
}
