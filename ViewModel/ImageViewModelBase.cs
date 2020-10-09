using OpenCvSharp;
using OpenCVTester.Common;
using OpenCVTester.Model;
using System.ComponentModel;
using System.Windows.Input;

namespace OpenCVTester.ViewModel
{
    public enum ImageType
    {
        [StringValue("Image 1")]
        IMAGE_1,
        [StringValue("Image 2")]
        IMAGE_2,
        [StringValue("Weighted Sum")]
        WEIGHTED_SUM,
        [StringValue("Subtract")]
        SUBTRACT
    }

    public abstract class ImageViewModelBase : INotifyPropertyChanged
    {
        protected ImageProcessing _imageProcessing;
        protected ImageModel _imageModel;
        private ImageType _imageType;
        private int _brightness;

        private ICommand _resetBrightnessCommand;

        private void ControlBrightness(int value)
        {
            ImageSource = _imageModel.ControlBrightness(value);
        }

        public ImageType ImageType
        {
            get { return _imageType; }
            set
            {
                _imageType = value;
                NotifyPropertyChanged("ImageType");
            }
        }
        public Mat ImageSource
        {
            get { return _imageModel.GetImage(); }
            set
            {
                NotifyPropertyChanged("ImageSource");
            }
        }
        public int Brightness
        {
            get { return _brightness; }
            set
            {
                _brightness = value;
                ImageSource = _imageModel.ControlBrightness(_brightness);
                NotifyPropertyChanged("Brightness");
            }
        }

        public ICommand ResetBrightnessCommand
        {
            get { return (this._resetBrightnessCommand) ?? (this._resetBrightnessCommand = new DelegateCommand((param) => ResetBrightness())); }
        }        

        public abstract bool IsVisible
        {
            get; set;
        }

        public void ResetBrightness()
        {
            Brightness = 0;
        }
        public void AdjustSize(ref Mat image1, ref Mat image2)
        {
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
        }

        public ImageViewModelBase()
        {
            _imageProcessing = new ImageProcessing();
            _imageModel = new ImageModel();
        }

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
