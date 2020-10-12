using OpenCvSharp;
using System;
using System.Collections.Generic;

namespace OpenCVTester.Model
{
    public struct GaussianBlur
    {
        public int kernalSize;
        public double sigma;

        public GaussianBlur(int size, double sigma)
        {
            this.kernalSize = size;
            this.sigma = sigma;
        }
    }

    public struct BilateralFilter
    {
        public int sigmaColor;
        public int sigmaSpace;

        public BilateralFilter(int color, int space)
        {
            this.sigmaColor = color;
            this.sigmaSpace = space;
        }
    }

    public struct TranslationFactor
    {
        public int x;
        public int y;

        public TranslationFactor(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct ShearFactor
    {
        public float x;
        public float y;

        public ShearFactor(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct ResizeFactor
    {
        public float scaleX;
        public float scaleY;

        public ResizeFactor(float x, float y)
        {
            scaleX = x;
            scaleY = y;
        }
    }

    public class ImageProcessing
    {
        private GaussianBlur unsharpMaskFactor = new GaussianBlur(0, 2.0);
        private GaussianBlur sketchFilterFactor = new GaussianBlur(0, 5.0);
        private BilateralFilter cartoonFilterFactor = new BilateralFilter(10, 5);

        public Mat ControlBrightness(Mat imageSource, int value)
        {
            if (imageSource == null)
            {
                return null;
            }

            if (value == 0)
            {
                return imageSource;
            }

            return imageSource + value;
        }
        public Mat ChangeContrast(Mat imageSource, double value)
        {
            if (imageSource == null)
            {
                return null;
            }

            if (value == 0)
            {
                return imageSource;
            }

            return (1 + value) * imageSource - 128 * value;
        }
        public Mat ChangeMeanBlur(Mat imageSource, int value)
        {
            if (imageSource == null)
            {
                return null;
            }

            if (value == 1)
            {
                return imageSource;
            }

            Size size = new Size(value, value);

            return imageSource.Blur(size);
        }
        public Mat ChangeGaussianBlur(Mat imageSource, GaussianBlur gaussianBlur)
        {
            if (imageSource == null)
            {
                return null;
            }

            if ((gaussianBlur.kernalSize == 1) && (gaussianBlur.sigma == 1.0))
            {
                return imageSource;
            }

            Size size = new Size(gaussianBlur.kernalSize, gaussianBlur.kernalSize);

            return imageSource.GaussianBlur(size, gaussianBlur.sigma);
        }
        public Mat Sharpen(Mat imageSource, double weight)
        {
            if (imageSource == null)
            {
                return null;
            }

            if (weight == 0)
            {
                return imageSource;
            }

            Mat unsharpMask = ChangeGaussianBlur(imageSource, unsharpMaskFactor);

            return (1 + weight) * imageSource - weight * unsharpMask;
        }
        public Mat ApplyMedianFilter(Mat imageSource, int size)
        {
            if (imageSource == null)
            {
                return null;
            }

            if (size == 1)
            {
                return imageSource;
            }

            return imageSource.MedianBlur(size);
        }
        public Mat ApplyBilateralFilter(Mat imageSource, BilateralFilter bilateralFilter)
        {
            if (imageSource == null)
            {
                return null;
            }

            if ((bilateralFilter.sigmaColor == 1) && (bilateralFilter.sigmaSpace == 1))
            {
                return imageSource;
            }

            return imageSource.BilateralFilter(-1, bilateralFilter.sigmaColor, bilateralFilter.sigmaSpace);
        }
        public Mat ApplySketchFilter(Mat imageSource)
        {
            if (imageSource == null)
            {
                return null;
            }

            Mat blurredImage = ChangeGaussianBlur(imageSource, sketchFilterFactor);

            return 255 * (imageSource / blurredImage);
        }
        public Mat ApplyCartoonFilter(Mat imageSource)
        {
            if (imageSource == null)
            {
                return null;
            }

            Mat bilateralImage = ApplyBilateralFilter(imageSource, cartoonFilterFactor);
            Mat cannyImage = 255 - imageSource.Canny(100, 200);
            Cv2.BitwiseAnd(bilateralImage, cannyImage, imageSource);

            return imageSource;
        }
        public Mat Translate(Mat imageSource, TranslationFactor translationFactor)
        {
            if (imageSource == null)
            {
                return null;
            }

            if ((translationFactor.x == 0) && (translationFactor.y == 0))
            {
                return imageSource;
            }

            List<Point2f> src = new List<Point2f>()
            {
                new Point2f(0.0f, 0.0f),
                new Point2f(0.0f, imageSource.Height),
                new Point2f(imageSource.Width, imageSource.Height)
            };

            List<Point2f> dst = new List<Point2f>()
            {
                new Point2f(translationFactor.x, translationFactor.y),
                new Point2f(translationFactor.x, imageSource.Height + translationFactor.y),
                new Point2f(imageSource.Width + translationFactor.x, imageSource.Height + translationFactor.y)
            };            

            Mat affineMatrix = Cv2.GetAffineTransform(src, dst);
            Mat translatedMatrix = new Mat();

            Cv2.WarpAffine(imageSource, translatedMatrix, affineMatrix, new Size(imageSource.Width, imageSource.Height));
            return translatedMatrix;
        }
        public Mat Shear(Mat imageSource, ShearFactor shearFactor)
        {
            if (imageSource == null)
            {
                return null;
            }

            if ((shearFactor.x == 0) && (shearFactor.y == 0))
            {
                return imageSource;
            }

            List<Point2f> src = new List<Point2f>()
            {
                new Point2f(0.0f, 0.0f),
                new Point2f(0.0f, imageSource.Height),
                new Point2f(imageSource.Width, imageSource.Height)
            };

            List<Point2f> dst = new List<Point2f>()
            {
                new Point2f(0.0f, 0.0f),
                new Point2f(shearFactor.y * imageSource.Height, imageSource.Height),
                new Point2f(imageSource.Width + shearFactor.y * imageSource.Height, shearFactor.x * imageSource.Width + imageSource.Height)
            };

            Mat affineMatrix = Cv2.GetAffineTransform(src, dst);
            Mat translatedMatrix = new Mat();

            Cv2.WarpAffine(imageSource, translatedMatrix, affineMatrix, new Size(imageSource.Width, imageSource.Height));
            return translatedMatrix;
        }
        public Mat Resize(Mat imageSource, ResizeFactor resizeFactor)
        {
            if (imageSource == null)
            {
                return null;
            }

            if ((resizeFactor.scaleX == 1) && (resizeFactor.scaleY == 1))
            {
                return imageSource;
            }

            Mat resizedImage = new Mat();

            Cv2.Resize(imageSource, resizedImage, new Size(0, 0), resizeFactor.scaleX, resizeFactor.scaleY);
            return resizedImage;
        }
        public Mat Crop(Mat imageSource, Rect rect)
        {
            return new Mat(imageSource, rect);
        }
        public Mat CalculateHistogram(Mat imageSource)
        {            
            Mat histogram = new Mat();
            int[] histSize = { 256 };
            Rangef[] rangef = { new Rangef(0, 256), };

            Cv2.CalcHist(new Mat[] { imageSource }, new int[] { 0 }, null, histogram, 1, histSize, rangef);
            return histogram;
        }
    }
}
