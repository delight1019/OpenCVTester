using Microsoft.Win32;
using OpenCvSharp;
using OpenCVTester.Model;
using System.ComponentModel;

namespace OpenCVTester.ViewModel
{
    public class ImageViewModel : INotifyPropertyChanged
    {
        private Mat _imageSource;
        private ImageModel _imageModel;

        public Mat ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                NotifyPropertyChanged("ImageSource");
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

        public ImageViewModel()
        {
            _imageModel = new ImageModel();
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
