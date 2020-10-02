using OpenCVTester.Common;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace OpenCVTester.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _imageSource;        
        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                NotifyPropertyChanged("ImageSource");
            }
        }

        private ICommand _loadImageCommand;
        public ICommand LoadImageCommand
        {
            get { return (this._loadImageCommand) ?? (this._loadImageCommand = new DelegateCommand(() => LoadImage())); }
        }
        public void LoadImage()
        {
            ImageSource = "D:\\ComputerVision\\OpenCVTester\\OpenCVTester\\Image\\cat.bmp";
        }

        public MainWindowViewModel()
        {
            
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