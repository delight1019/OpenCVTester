using OpenCvSharp;
using OpenCVTester.Common;
using OpenCVTester.Model;
using System.ComponentModel;
using System.Windows.Input;

namespace OpenCVTester.ViewModel
{
    public enum ImageType
    {
        [StringValue("Image 1")]
        IMAGE_1,
        [StringValue("Image 2")]
        IMAGE_2,
        [StringValue("Weighted Sum")]
        WEIGHTED_SUM,
        [StringValue("Subtract")]
        SUBTRACT,
        [StringValue("Abs Diff")]
        ABS_DIFF
    }

    public abstract class ImageViewModelBase : INotifyPropertyChanged
    {
        protected ImageProcessing _imageProcessing;
        protected ImageModel _imageModel;
        private ImageType _imageType;

        private ICommand _resetBrightnessCommand;
        private ICommand _resetContrastCommand;
        private ICommand _resetMeanBlurCommand;
        private ICommand _resetGaussianBlurCommand;
        private ICommand _resetSharpeningCommand;
        private ICommand _resetMedianFilterCommand;
        private ICommand _resetBilateralFilterCommand;

        private void ResetBrightness()
        {
            Brightness = 0;
        }
        private void ResetContrast()
        {
            Contrast = 0;
        }
        private void ResetMeanBlur()
        {
            MeanBlur = 1;
        }
        private void ResetGaussianBlur()
        {
            GaussianBlurSize = 1;
            GaussianBlurSigma = 1;
        }
        private void ResetSharpening()
        {
            Sharpening = 0;
        }
        private void ResetMedianFilter()
        {
            MedianFilter = 1;
        }
        private void ResetBilateralFilter()
        {
            BilateralFilterSigmaColor = 1;
            BilateralFilterSigmaSpace = 1;
        }

        protected void AdjustSize(ref Mat image1, ref Mat image2)
        {
            if (image1.Width > image2.Width)
            {
                image1 = _imageProcessing.Crop(image1, new Rect(0, 0, image2.Width, image1.Height));
            }
            else
            {
                image2 = _imageProcessing.Crop(image2, new Rect(0, 0, image1.Width, image2.Height));
            }

            if (image1.Height > image2.Height)
            {
                image1 = _imageProcessing.Crop(image1, new Rect(0, 0, image1.Width, image2.Height));
            }
            else
            {
                image2 = _imageProcessing.Crop(image2, new Rect(0, 0, image2.Width, image1.Height));
            }
        }

        public ImageType ImageType
        {
            get { return _imageType; }
            set
            {
                _imageType = value;
                NotifyPropertyChanged("ImageType");
            }
        }
        public Mat ImageSource
        {
            get { return _imageModel.GetImage(); }
            set
            {
                Histogram = _imageModel.CalculateHistogram();
                NotifyPropertyChanged("ImageSource");
            }
        }
        public Mat Histogram
        {
            get { return _imageModel.GetHistogram(); }
            set
            {
                NotifyPropertyChanged("Histogram");
            }
        }
        public int Brightness
        {
            get { return _imageModel.GetBrightness(); }
            set
            {
                ImageSource = _imageModel.ControlBrightness(value);
                NotifyPropertyChanged("Brightness");
            }
        }
        public double Contrast
        {
            get { return _imageModel.GetContrast(); }
            set
            {
                ImageSource = _imageModel.ChangeContrast(value);
                NotifyPropertyChanged("Contrast");
            }
        }
        public int MeanBlur
        {
            get { return _imageModel.GetMeanBlur(); }
            set
            {
                ImageSource = _imageModel.ChangeMeanBlur(value);
                NotifyPropertyChanged("MeanBlur");
            }
        }
        public int GaussianBlurSize
        {
            get { return _imageModel.GetGaussianBlurSize(); }
            set
            {
                ImageSource = _imageModel.ChangeGaussianBlurSize(value);
                NotifyPropertyChanged("GaussianBlurSize");
            }
        }
        public double GaussianBlurSigma
        {
            get { return _imageModel.GetGaussianBlurSigma(); }
            set
            {
                ImageSource = _imageModel.ChangeGaussianBlurSigma(value);
                NotifyPropertyChanged("GaussianBlurSigma");
            }
        }
        public double Sharpening
        {
            get { return _imageModel.GetSharpening(); }
            set
            {
                ImageSource = _imageModel.Sharpen(value);
                NotifyPropertyChanged("Sharpening");
            }
        }
        public int MedianFilter
        {
            get { return _imageModel.GetMedianFilterSize(); }
            set
            {
                ImageSource = _imageModel.ApplyMedianFilter(value);
                NotifyPropertyChanged("MedianFilter");
            }
        }
        public int BilateralFilterSigmaColor
        {
            get { return _imageModel.GetBilateralFilterSigmaColor(); }
            set
            {
                ImageSource = _imageModel.ChangeBilateralFilterSigmaColor(value);
                NotifyPropertyChanged("BilateralFilterSigmaColor");
            }
        }
        public int BilateralFilterSigmaSpace
        {
            get { return _imageModel.GetBilateralFilterSigmaSpace(); }
            set
            {
                ImageSource = _imageModel.ChangeBilateralFilterSigmaSpace(value);
                NotifyPropertyChanged("BilateralFilterSigmaSpace");
            }
        }
        public bool IsSketchFilterOn
        {
            get { return _imageModel.IsSketchFilterOn(); }
            set
            {
                ImageSource = _imageModel.ApplySketchFilter(value);
                NotifyPropertyChanged("IsSketchFilterOn");
            }
        }
        public bool IsCartoonFilterOn
        {
            get { return _imageModel.IsCartoonFilterOn(); }
            set
            {
                ImageSource = _imageModel.ApplyCartoonFilter(value);
                NotifyPropertyChanged("IsCartoonFilterOn");
            }
        }
        public int TranslationX
        {
            get { return _imageModel.GetTranslationX(); }
            set
            {
                ImageSource = _imageModel.TranslateX(value);
                NotifyPropertyChanged("TranslationX");
            }
        }
        public int TranslationY
        {
            get { return _imageModel.GetTranslationY(); }
            set
            {
                ImageSource = _imageModel.TranslateY(value);
                NotifyPropertyChanged("TranslationY");
            }
        }

        public ICommand ResetBrightnessCommand
        {
            get { return (this._resetBrightnessCommand) ?? (this._resetBrightnessCommand = new DelegateCommand((param) => ResetBrightness())); }
        }        
        public ICommand ResetContrastCommand
        {
            get { return (this._resetContrastCommand) ?? (this._resetContrastCommand = new DelegateCommand((param) => ResetContrast())); }
        }
        public ICommand ResetMeanBlurCommand
        {
            get { return (this._resetMeanBlurCommand) ?? (this._resetMeanBlurCommand = new DelegateCommand((param) => ResetMeanBlur())); }
        }
        public ICommand ResetGaussianBlurCommand
        {
            get { return (this._resetGaussianBlurCommand) ?? (this._resetGaussianBlurCommand = new DelegateCommand((param) => ResetGaussianBlur())); }
        }
        public ICommand ResetSharpeningCommand
        {
            get { return (this._resetSharpeningCommand) ?? (this._resetSharpeningCommand = new DelegateCommand((param) => ResetSharpening())); }
        }
        public ICommand ResetMedianFilterCommand
        {
            get { return (this._resetMedianFilterCommand) ?? (this._resetMedianFilterCommand = new DelegateCommand((param) => ResetMedianFilter())); }
        }
        public ICommand ResetBilateralFilterCommand
        {
            get { return (this._resetBilateralFilterCommand) ?? (this._resetBilateralFilterCommand = new DelegateCommand((param) => ResetBilateralFilter())); }
        }

        public abstract bool IsVisible
        {
            get; set;
        }
        
        public void NormalizeHistogram()
        {
            ImageSource = _imageModel.NormalizeHistogram();
        }
        public void EqualizeHistogram()
        {
            ImageSource = _imageModel.EqualizeHistogram();
        }

        public ImageViewModelBase()
        {
            _imageProcessing = new ImageProcessing();
            _imageModel = new ImageModel();
        }

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
