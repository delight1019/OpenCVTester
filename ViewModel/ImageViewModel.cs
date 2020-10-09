using Microsoft.Win32;
using OpenCvSharp;
using OpenCVTester.Common;
using OpenCVTester.Model;
using System.Windows.Input;

namespace OpenCVTester.ViewModel
{
    public sealed class ImageViewModel : ImageViewModelBase
    {
        private bool _isVisible;
        private bool _isLoaded;                

        public override bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                NotifyPropertyChanged("IsVisible");
            }
        }
        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                _isLoaded = value;
                NotifyPropertyChanged("IsLoaded");
            }
        }        

        public void LoadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                ImageSource = _imageModel.RegisterImage(openFileDialog.FileName);
                IsLoaded = true;
            }
        }                
        public void SubtractImage(Mat image1, Mat image2)
        {
            AdjustSize(ref image1, ref image2);
            ImageSource = _imageModel.SubtractImages(image1, image2);
        }

        public ImageViewModel(ImageType imageType)
        {
            ImageType = imageType;

            IsLoaded = false;
        }        
    }
}
