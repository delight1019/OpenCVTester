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
        private ImageViewModel _leftImageViewModel;
        private ImageViewModel _rightImageViewModel;

        private ImageProcessing _imageProcessing;
        private int _brightness;

        private ICommand _loadImageCommand;
        private ICommand _resetBrightnessCommand;

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
        
        public ImageViewModel LeftImageViewModel
        {
            get { return _leftImageViewModel; }
            set
            {
                _leftImageViewModel = value;
                NotifyPropertyChanged("LeftImageViewModel");
            }
        }
        public ImageViewModel RightImageViewModel
        {
            get { return _rightImageViewModel; }
            set
            {
                _rightImageViewModel = value;
                NotifyPropertyChanged("RightImageViewModel");
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
            ImageViewModel imageViewModel = (parameter as ImageViewModel);
            imageViewModel.LoadImage();
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
            _leftImageViewModel = new ImageViewModel();
            _rightImageViewModel = new ImageViewModel();
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