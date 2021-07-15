using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace WebImageConvolution.Services
{
    public class ConvolutionService : IConvolutionService
    {
        public Bitmap Image { get; set; }

        public void Blur()
        {
            var filter = GetBoxBlurFilterMatrix(3);

            var resultPixels = ConvolutionFilter(Image, filter);

            Image = GetResultBitmap(Image, resultPixels);
        }

        public void GrayScale()
        {
            var (pixels, resultPixels, stride, format) = GetDataForConvolution(Image);

            for (int i = 0; i < Image.Width; ++i)
            {
                for (int j = 0; j < Image.Height; ++j)
                {
                    int index = j * stride + format * i;

                    double red = pixels[index];
                    double green = pixels[index + 1];
                    double blue = pixels[index + 2];

                    byte gray = (byte)(0.2 * red + 0.7 * green + 0.1 * blue); //Формула перевода в серый

                    for (int c = 0; c < 3; ++c)
                    {
                        resultPixels[index + c] = gray;
                    }

                    if (format == 4)
                    {
                        resultPixels[index + 3] = pixels[index + 3];
                    }
                }
            }

            var resultBitmap = GetResultBitmap(Image, resultPixels);

            Image = resultBitmap;
        }

        public void Log()
        {
            var (pixels, resultPixels, stride, format) = GetDataForConvolution(Image);

            for (int i = 0; i < Image.Width; ++i)
            {
                for (int j = 0; j < Image.Height; ++j)
                {
                    int index = j * stride + format * i;

                    for (int c = 0; c < format; ++c)
                    {
                        var result = 110 * Math.Log10(pixels[index + c] + 1);

                        resultPixels[index + c] = (byte)result;
                    }
                }
            }

            var resultBitmap = GetResultBitmap(Image, resultPixels);

            Image = resultBitmap;
        }

        public void Contrast()
        {
            var (pixels, resultPixels, stride, format) = GetDataForConvolution(Image);

            var p = 0.005;
            double a_low = 0;
            double a_high = 255;


            var pixelsSize = format * Image.Height * Image.Width;
            var histogram = Histogram(pixels, stride, format);

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

            for (int i = 0; i < Image.Width; i++)
            {
                for (int j = 0; j < Image.Height; j++)
                {
                    var index = j * stride + format * i;
                    for (int c = 0; c < format; c++)
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
                }
            }

            var resultBitmap = GetResultBitmap(Image, resultPixels);

            Image = resultBitmap;
        }

        public void Flip()
        {
            var imageRes = new Bitmap(2 * Image.Width, Image.Height, Image.PixelFormat);

            var (pixels, _, stride, format) = GetDataForConvolution(Image);
            var (_, resResultPixels, resStride, _) = GetDataForConvolution(imageRes);

            int halfImResPixWidth = imageRes.Width / 2;
            for (int i = 0; i < imageRes.Height; i++)
            {
                for (int j = 0; j < halfImResPixWidth; j++)
                {
                    for (int c = 0; c < format; c++)
                    {
                        int index = i * stride + format * j;
                        int flIndex = i * resStride + format * j;
                        int revFlIndex = i * resStride + format * (imageRes.Width - j - 1);
                        resResultPixels[flIndex + c] = pixels[index + c];
                        resResultPixels[revFlIndex + c] = pixels[index + c];
                    }
                }
            }

            var resultBitmap = GetResultBitmap(imageRes, resResultPixels);

            Image = resultBitmap;
        }

        public void Negate()
        {
            var (pixels, resultPixels, stride, format) = GetDataForConvolution(Image);

            for (int i = 0; i < Image.Width; ++i)
            {
                for (int j = 0; j < Image.Height; ++j)
                {
                    int index = j * stride + format * i;

                    for (int c = 0; c < format; ++c)
                    {
                        byte negative = 255;
                        resultPixels[index + c] = (byte)(negative - pixels[index + c]);
                    }
                }
            }

            var resultBitmap = GetResultBitmap(Image, resultPixels);

            Image = resultBitmap;
        }

        public void SobelEdges()
        {
            var (pixels, resultPixels, stride, format) = GetDataForConvolution(Image); 

            double[,] xKernel = new double[3, 3] { { -1 / 9.0, 0, 1 / 9.0 },
                                                   { -2 / 9.0, 0, 2 / 9.0 },
                                                   { -1 / 9.0, 0, 1 / 9.0 }
            };

            double[,] yKernel = new double[3, 3] { { -1 / 9.0, -2 / 9.0 , -1 / 9.0 },
                                                   { 0, 0, 0 },
                                                   { 1 / 9.0, 2 / 9.0, 1 / 9.0 }
            };
            var filterWidth = xKernel.GetLength(1);
            var filterOffset = (filterWidth - 1) / 2;

            int index;
            for (int i = filterOffset; i < Image.Width - filterOffset; i++)
            {
                for (int j = filterOffset; j < Image.Height - filterOffset; j++)
                {
                    for (int c = 0; c < format; c++)
                    {
                        double xColor = 0;
                        double yColor = 0;
                        for (int filterY = 0; filterY < filterWidth; filterY++)
                        {
                            for (int filterX = 0; filterX < filterWidth; filterX++)
                            {
                                index = (j + filterY - filterOffset) * stride + format * (i + filterX - filterOffset);
                                xColor += pixels[index + c] * xKernel[filterY, filterX];
                                yColor += pixels[index + c] * yKernel[filterY, filterX];
                            }
                        }
                        index = j * stride + format * i;
                        resultPixels[index + c] = (byte)Math.Sqrt((xColor * xColor) + (yColor * yColor));
                    }
                }
            }

            var resultBitmap = GetResultBitmap(Image, resultPixels);

            Image = resultBitmap;
        }

        public void ImpulseNoise()
        {
            var (pixels, resultPixels, stride, format) = GetDataForConvolution(Image);

            var filter = new List<int>(3);//list with sensitivity capacity

            var filterOffset = filter.Capacity / 2;
            int index;
            int filteredPixel;

            for (int i = filterOffset; i < Image.Width - filterOffset; i++)
            {
                for (int j = filterOffset; j < Image.Height; j++)
                {
                    for (int c = 0; c < format; c++)
                    {
                        for (int k = 0; k < filter.Capacity; k++)
                        {
                            index = j * stride + format * (i - filterOffset + k);
                            filter.Add(pixels[index + c]);
                        }

                        filter.Sort();
                        filteredPixel = filter[filter.Capacity / 2];

                        index = j * stride + format * i;
                        resultPixels[index + c] = (byte)filteredPixel;

                        filter.Clear();
                    }
                }
            }

            var resultBitmap = GetResultBitmap(Image, resultPixels);

            Image = resultBitmap;
        }

        public void Edges()
        {
            var filter = GetEdgeDetectionFilterMatrix(3);

            var resultPixels = ConvolutionFilter(Image, filter);

            Image = GetResultBitmap(Image, resultPixels);
        }

        private static byte[] ConvolutionFilter(Bitmap bitmap, double[,] filter) 
        {
            var (pixels, resultPixels, stride, format) = GetDataForConvolution(bitmap);

            var filterWidth = filter.GetLength(1);
            var filterOffset = filterWidth / 2;

            int index;
            for (int i = filterOffset; i < bitmap.Width - filterOffset; i++)
            {
                for (int j = filterOffset; j < bitmap.Height - filterOffset; j++)
                {
                    for (int c = 0; c < format; c++)
                    {
                        double sum = 0;
                        for (int filterY = 0; filterY < filterWidth; filterY++)
                        {
                            for (int filterX = 0; filterX < filterWidth; filterX++)
                            {
                                index = (j + filterY - filterOffset) *
                                    stride + format * (i + filterX - filterOffset);

                                sum += pixels[index + c] * filter[filterY, filterX];
                            }
                        }

                        index = j * stride + format * i;
                        resultPixels[index + c] = (byte)sum;
                    }
                }
            }

            return resultPixels;
        }

        private static double[,] GetBoxBlurFilterMatrix(int length)
        {
            var filter = new double[length, length];

            for (int i = 0; i < filter.GetLength(0); i++)
            {
                for (int j = 0; j < filter.GetLength(1); j++)
                {
                    filter[i, j] = 1 / (double)(length * length);
                }
            }

            return filter;
        }

        public static double[,] GetEdgeDetectionFilterMatrix(int length)
        {
            var filter = new double[length, length];
            double filterElementCount = length * length;

            for (int i = 0; i < filter.GetLength(0); i++)
            {
                for (int j = 0; j < filter.GetLength(1); j++)
                {
                    filter[i, j] = -1 / filterElementCount;
                }
            }

            // filter's middle
            filter[length / 2, length / 2] = (filterElementCount - 1) / filterElementCount;

            return filter;
        }

        private static int[] CumulativeHistogram(int[] histogram)
        {
            for (int h = 1; h < 256; h++)
            {
                histogram[h] = histogram[h - 1] + histogram[h];
            }
            return histogram;
        }

        private int[] Histogram(byte[] pixels, int stride, int format)
        {
            int[] histogram = new int[256];
            for (int i = 0; i < Image.Width; i++)
            {
                for (int j = 0; j < Image.Height; j++)
                {
                    int index = j * stride + format * i;
                    for (int col = 0; col < format; col++)
                    {
                        histogram[pixels[index + col]] += 1;
                    }
                }
            }
            return histogram;
        }

        private static (byte[] pixels, 
            byte[] resultPixels, int stride, int format) GetDataForConvolution(Bitmap bitmap) 
        {
            BitmapData sourceData =
                bitmap.LockBits(new Rectangle(0, 0,
                bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly,
                bitmap.PixelFormat);

            byte[] pixels = new byte[sourceData.Stride *
                                          sourceData.Height];

            byte[] resultPixels = new byte[sourceData.Stride *
                                           sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixels, 0,
                                       resultPixels.Length);

            bitmap.UnlockBits(sourceData);

            //pixel format in bytes
            var format = System.Drawing.Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;

            return (pixels, resultPixels, sourceData.Stride, format);
        }

        private static Bitmap GetResultBitmap(Bitmap bitmap, byte[] resultPixels) 
        {
            var resultBitmap = new Bitmap(bitmap.Width, bitmap.Height,
                  bitmap.PixelFormat);


            var resultData =
                       resultBitmap.LockBits(new Rectangle(0, 0,
                       resultBitmap.Width, resultBitmap.Height),
                       ImageLockMode.WriteOnly,
                       resultBitmap.PixelFormat);


            Marshal.Copy(resultPixels, 0, resultData.Scan0,
                                       resultPixels.Length);


            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }    
    }
}
