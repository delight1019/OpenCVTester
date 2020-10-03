using System.IO;
using OpenCvSharp;

namespace OpenCVTester.Model
{
    public class ImageModel
    {
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

        public ImageModel()
        {
            _originImage = new Mat();
            _currentImage = new Mat();
        }
    }
}
