using Microsoft.Win32;
using OpenCvSharp;
using OpenCVTester.Model;
using System.ComponentModel;

namespace OpenCVTester.ViewModel
{
    public class ImageViewModel : INotifyPropertyChanged
    {
        private ImageModel _imageModel;

        private Mat _imageSource;        
        private string _header;

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

        public void LoadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                ImageSource = _imageModel.RegisterImage(openFileDialog.FileName);
            }
        }

        public ImageViewModel(string header)
        {
            _imageModel = new ImageModel();
            _header = header;
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
