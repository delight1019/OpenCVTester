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
        private ImageViewModelBase _selectedImage;

        private ICommand _loadImageCommand;
        private ICommand _weightedSumCommand;
        private ICommand _subtractCommand;

        public ObservableCollection<ImageViewModelBase> ImageList
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
        public ImageViewModelBase SelectedImage
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
        public ICommand WeightedSumCommand
        {
            get { return (this._weightedSumCommand) ?? (this._weightedSumCommand = new DelegateCommand((param) => WeightedSum())); }
        }
        public ICommand SubtractCommand
        {
            get { return (this._subtractCommand) ?? (this._subtractCommand = new DelegateCommand((param) => Subtract())); }
        }

        public void LoadImage(object parameter)
        {
            ImageViewModel imageViewModel = (parameter as ImageViewModel);
            imageViewModel.LoadImage();
            SelectedImage = imageViewModel;
        }                        
        public void WeightedSum()
        {
            BinaryOperationImageViewModel weightedSumImageViewModel = new BinaryOperationImageViewModel(ImageType.WEIGHTED_SUM);
            weightedSumImageViewModel.AddWeightedImages(_leftImageViewModel.ImageSource, _rightImageViewModel.ImageSource, 0.5, 0.5);
            ImageList.Add(weightedSumImageViewModel);
            SelectedImage = weightedSumImageViewModel;
        }
        public void Subtract()
        {
            BinaryOperationImageViewModel imageViewModel = new BinaryOperationImageViewModel(ImageType.SUBTRACT);
            imageViewModel.SubtractImage(_leftImageViewModel.ImageSource, _rightImageViewModel.ImageSource);
            ImageList.Add(imageViewModel);
            SelectedImage = imageViewModel;
        }

        public MainWindowViewModel()
        {
            _leftImageViewModel = new ImageViewModel(ImageType.IMAGE_1);
            _rightImageViewModel = new ImageViewModel(ImageType.IMAGE_2);

            ImageList = new ObservableCollection<ImageViewModelBase>();
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