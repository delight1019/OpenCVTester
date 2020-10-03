using OpenCvSharp;
using OpenCVTester.Common;
using OpenCVTester.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace OpenCVTester.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private ImageViewModel _leftImageViewModel;
        private ImageViewModel _rightImageViewModel;
        private ImageViewModel _selectedImage;

        private ImageProcessing _imageProcessing;

        private ICommand _loadImageCommand;
        private ICommand _resetBrightnessCommand;

        public ObservableCollection<ImageViewModel> ImageList
        {
            get; set;
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
        public ImageViewModel SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                _selectedImage = value;
                NotifyPropertyChanged("SelectedImage");
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
            SelectedImage = imageViewModel;
        }                
        public void ControlBrightness(int value)
        {
            //ImageSource = _imageProcessing.ControlBrightness(_originImage, value);
        }
        public void ResetBrightness()
        {
            //Brightness = 0;
        }

        public MainWindowViewModel()
        {
            _leftImageViewModel = new ImageViewModel("Image 1");
            _rightImageViewModel = new ImageViewModel("Image 2");

            _imageProcessing = new ImageProcessing();
            ImageList = new ObservableCollection<ImageViewModel>();
            ImageList.Add(_leftImageViewModel);
            ImageList.Add(_rightImageViewModel);

            SelectedImage = LeftImageViewModel;
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