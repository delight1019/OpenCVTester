using OpenCvSharp;

namespace OpenCVTester.ViewModel
{
    public class WeightedSumImageViewModel : ImageViewModelBase
    {
        private bool _isVisible;
        private Mat _imageSource1;
        private Mat _imageSource2;
        private double _alpha;
        private double _beta;

        public Mat ImageSource1
        {
            get { return _imageSource1; }
            set
            {
                _imageSource1 = value;
                NotifyPropertyChanged("ImageSource1");
            }
        }
        public Mat ImageSource2
        {
            get { return _imageSource2; }
            set
            {
                _imageSource2 = value;
                NotifyPropertyChanged("ImageSource2");
            }
        }
        public double Alpha
        {
            get { return _alpha; }
            set
            {
                if (_alpha != value)
                {
                    _alpha = value;
                    AddWeightedImages(_imageSource1, _imageSource2, _alpha, _beta);
                    NotifyPropertyChanged("Alpha");
                }                
            }
        }
        public double Beta
        {
            get { return _beta; }
            set
            {
                if (_beta != value)
                {
                    _beta = value;
                    AddWeightedImages(_imageSource1, _imageSource2, _alpha, _beta);
                    NotifyPropertyChanged("Beta");
                }                
            }
        }

        public override bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                NotifyPropertyChanged("IsVisible");
            }
        }        
        
        public void AddWeightedImages(Mat image1, Mat image2, double alpha, double beta)
        {
            ImageSource1 = image1;
            ImageSource2 = image2;
            Alpha = alpha;
            Beta = beta;

            if (image1.Width > image2.Width)
            {
                image1 = _imageProcessing.Crop(image1, new Rect(0, 0, image2.Width, image1.Height));
            }
            else
            {
                image2 = _imageProcessing.Crop(image2, new Rect(0, 0, image1.Width, image2.Height));
            }

            if (image1.Height > image2.Height)
            {
                image1 = _imageProcessing.Crop(image1, new Rect(0, 0, image1.Width, image2.Height));
            }
            else
            {
                image2 = _imageProcessing.Crop(image2, new Rect(0, 0, image2.Width, image1.Height));
            }

            ImageSource = _imageModel.AddWeightedImages(image1, image2, alpha, beta);
        }

        public WeightedSumImageViewModel(ImageType imageType)
        {
            ImageType = imageType;
            _imageSource1 = new Mat();
            _imageSource2 = new Mat();
        }
    }
}
