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
        private Mat _originImage;
        private Mat _imageSource;
        private int _brightness;

        private ICommand _loadImageCommand;
        private ICommand _resetBrightnessCommand;

        public Mat ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                NotifyPropertyChanged("ImageSource");
            }
        }
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
            get { return (this._loadImageCommand) ?? (this._loadImageCommand = new DelegateCommand(() => LoadImage())); }
        }
        public ICommand ResetBrightnessCommand
        {
            get { return (this._resetBrightnessCommand) ?? (this._resetBrightnessCommand = new DelegateCommand(() => ResetBrightness())); }
        }

        public void LoadImage()
        {
            string imagePath = "D:\\ComputerVision\\OpenCVTester\\OpenCVTester\\Image\\cat.bmp";
            _originImage = new Mat(imagePath, ImreadModes.Grayscale);
            ImageSource = _originImage;
        }                
        public void ControlBrightness(int value)
        {
            ImageSource = _imageProcessing.ControlBrightness(_originImage, value);
        }
        public void ResetBrightness()
        {
            Brightness = 0;
        }

        public MainWindowViewModel()
        {
            _imageProcessing = new ImageProcessing();
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