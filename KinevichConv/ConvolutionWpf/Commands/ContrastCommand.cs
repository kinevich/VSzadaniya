using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Catel.MVVM;

namespace ConvolutionWpf.Commands
{
    public class ContrastCommand : Command
    {
        private readonly Func<WriteableBitmap> _imageFactory;

        public ContrastCommand(Func<WriteableBitmap> imageFactory)
            : base(() => { })
        {
            _imageFactory = imageFactory;
        }

        public void ExecuteCommand()
        {
            var image = _imageFactory();
            if (image == null)
                return;

            var pixels = new byte[image.PixelHeight * image.BackBufferStride];
            image.CopyPixels(pixels, image.BackBufferStride, 0);

            var resultPixels = new byte[image.PixelHeight * image.BackBufferStride];

            var p = 0.005;
            double a_low = 0;
            double a_high = 255;

            
            var pixelsSize = 3 * image.PixelHeight * image.PixelWidth;
            var histogram = Histogram(image, pixels);

            var cumulativeHistogram = CumulativeHistogram(histogram);

            for (int l = 0; l < 256; l++)
            {
                if (cumulativeHistogram[l] >= pixelsSize * p)
                {
                    a_low = l;
                    break;
                }
            }

            for (int h = 255; h >= 0; h--)
            {
                if (cumulativeHistogram[h] <= pixelsSize * (1 - p))
                {
                    a_high = h;
                    break;
                }
            }

            for (int i = 0; i < image.PixelWidth; i++)
            {
                for (int j = 0; j < image.PixelHeight; j++)
                {
                    var index = j * image.BackBufferStride + 4 * i;
                    for (int c = 0; c < 3; c++)
                    {
                        var a = pixels[index + c];
                        double b;
                        if (a <= a_low)
                        {
                            b = 0;
                        }
                        else if (a >= a_high)
                        {
                            b = 255;
                        }
                        else
                        {
                            b = (a - a_low) / (a_high - a_low) * 255;
                        }
                        resultPixels[index + c] = (byte)b;
                    }
                    resultPixels[index + 3] = (byte)255;
                }
            }

            image.WritePixels(new Int32Rect(0, 0, image.PixelWidth, image.PixelHeight), resultPixels, image.BackBufferStride, 0);
        }

        private static int[] CumulativeHistogram(int[] histogram)
        {
            for (int h = 1; h < 256; h++)
            {
                histogram[h] = histogram[h - 1] + histogram[h];
            }
            return histogram;
        }

        private static int[] Histogram(WriteableBitmap image, byte[] pixels)
        {
            int[] histogram = new int[256];
            for (int i = 0; i < image.PixelWidth; i++)
            {
                for (int j = 0; j < image.PixelHeight; j++)
                {
                    int index = j * image.BackBufferStride + 4 * i;
                    for (int col = 0; col < 3; col++)
                    {
                        histogram[pixels[index + col]] += 1;
                    }
                }
            }
            return histogram;
        }


        protected override void Execute(object parameter, bool ignoreCanExecuteCheck)
        {
            ExecuteCommand();
        }
    }
}

/* int nOfB = image.PixelHeight * image.BackBufferStride;
             int nOfP = image.PixelHeight * image.PixelWidth;
             int pn = 256;
             double[] pnR = new double[pn];
             double[] pnG = new double[pn];
             double[] pnB = new double[pn];

             for (int p = 0; p < nOfB; p += 4)
             {
                 pnR[pixels[p]]++;
                 pnG[pixels[p + 1]]++;
                 pnB[pixels[p + 2]]++;
             }

             for (int prob = 0; prob < pn; prob++)
             {
                 pnR[prob] /= nOfP;
                 pnG[prob] /= nOfP;
                 pnB[prob] /= nOfP;
             }

             for (int i = 0; i < image.PixelHeight; i++)
             {
                 for (int j = 0; j < image.PixelWidth; j++)
                 {
                     int index = i * image.BackBufferStride + j * 4;                  
                     for (int c = 0; c < 3; c++)
                     {
                         double sum = 0;
                         for (int q = 0; q < pixels[index + c]; q++)
                         {
                             if (c == 0)
                             {
                                 sum += pnR[q];
                             } 
                             else if (c == 1)
                             {
                                 sum += pnG[q];
                             } 
                             else
                             {
                                 sum += pnB[q];
                             }
                             resultPixels[index + c] = (byte)Math.Floor(255 * sum);
                             resultPixels[index + 3] = 255;
                         }
                     }
                 }
             }  */
//int index = j * image.BackBufferStride + 4 * i;
//todo
