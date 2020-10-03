using Microsoft.Win32;
using OpenCvSharp;
using OpenCVTester.Model;
using System.ComponentModel;
using System.Windows.Input;

namespace OpenCVTester.ViewModel
{
    public class ImageViewModel : INotifyPropertyChanged
    {
        private bool _isLoaded;
        private ImageModel _imageModel;
        private ImageProcessing _imageProcessing;

        private Mat _originImage;
        private Mat _imageSource;        
        private string _header;
        private int _brightness;

        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                _isLoaded = value;
                NotifyPropertyChanged("IsLoaded");
            }
        }
        public Mat ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                NotifyPropertyChanged("ImageSource");
            }
        }
        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                NotifyPropertyChanged("Header");
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

        public void LoadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                _originImage = _imageModel.RegisterImage(openFileDialog.FileName);
                ImageSource = _originImage;
                IsLoaded = true;
            }
        }
        public void ControlBrightness(int value)
        {
            ImageSource = _imageProcessing.ControlBrightness(_originImage, value);
        }

        public ImageViewModel(string header)
        {
            _imageModel = new ImageModel();
            _imageProcessing = new ImageProcessing();
            _header = header;

            IsLoaded = false;
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
