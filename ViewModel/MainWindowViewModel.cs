using OpenCvSharp;
using OpenCVTester.Common;
using OpenCVTester.Model;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace OpenCVTester.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private ImageProcessing _imageProcessing;
        private int _brightness;

        private ICommand _loadImageCommand;
        private ICommand _resetBrightnessCommand;

        public Mat LeftImageSource
        {
            get
            {
                return LeftImage.GetImage();
            }
            set
            {
                NotifyPropertyChanged("LeftImageSource");
            }
        }
        public Mat RightImageSource
        {
            get
            {
                return RightImage.GetImage();
            }
            set
            {
                NotifyPropertyChanged("RightImageSource");
            }
        }
        public ImageModel LeftImage { get; set; }
        public ImageModel RightImage { get; set; }        
        public int Brightness
        {
            get { return _brightness; }
            set
            {
                _brightness = value;
                ControlBrightness(_brightness);
                NotifyPropertyChanged("Brightness");
            }
        }

        public ICommand LoadImageCommand
        {
            get { return (this._loadImageCommand) ?? (this._loadImageCommand = new DelegateCommand((param) => LoadImage(param))); }
        }
        public ICommand ResetBrightnessCommand
        {
            get { return (this._resetBrightnessCommand) ?? (this._resetBrightnessCommand = new DelegateCommand((param) => ResetBrightness())); }
        }

        public void LoadImage(object parameter)
        {
            ImageModel imageModel = (parameter as ImageModel);

            if (imageModel.GetCode() == ImageCode.LEFT)
            {
                LeftImageSource = imageModel.RegisterImage("D:\\ComputerVision\\OpenCVTester\\OpenCVTester\\Image\\cat.bmp");
            }
            else if (imageModel.GetCode() == ImageCode.RIGHT)
            {
                RightImageSource = imageModel.RegisterImage("D:\\ComputerVision\\OpenCVTester\\OpenCVTester\\Image\\cropland.png");
            }
        }                
        public void ControlBrightness(int value)
        {
            //ImageSource = _imageProcessing.ControlBrightness(_originImage, value);
        }
        public void ResetBrightness()
        {
            Brightness = 0;
        }

        public MainWindowViewModel()
        {
            _imageProcessing = new ImageProcessing();
            LeftImage = new ImageModel(ImageCode.LEFT);
            RightImage = new ImageModel(ImageCode.RIGHT);
            Brightness = 0;
        }

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}