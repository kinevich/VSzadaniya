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
        public static Bitmap Image { get; set; }

        public void Blur()
        {
            var (pixels, resultPixels, stride) = GetDataForConvolution();

            var filterMatrix = GetBoxBlurFilterMatrix(3);
            var filterWidth = filterMatrix.GetLength(1);
            var filterOffset = filterWidth / 2;

            int index;
            for (int i = filterOffset; i < Image.Width - filterOffset; i++)
            {
                for (int j = filterOffset; j < Image.Height - filterOffset; j++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        double sum = 0;
                        for (int filterY = 0; filterY < filterWidth; filterY++)
                        {
                            for (int filterX = 0; filterX < filterWidth; filterX++)
                            {
                                index = (j + filterY - filterOffset) * 
                                    stride + 4 * (i + filterX - filterOffset);

                                sum += pixels[index + c] * filterMatrix[filterY, filterX];
                            }
                        }
                        index = j * stride + 4 * i;
                        resultPixels[index + c] = (byte)sum;
                    }
                }
            }

            var resultBitmap = GetResultBitmap(resultPixels);

            Image = resultBitmap;
        }

        public void GrayScale()
        {
            throw new NotImplementedException();
        }

        public void Log()
        {
            throw new NotImplementedException();
        }

        public void Contrast()
        {
            throw new NotImplementedException();
        }

        public void Flip()
        {
            throw new NotImplementedException();
        }

        public void Negate()
        {
            throw new NotImplementedException();
        }

        public void SobelEdges()
        {
            throw new NotImplementedException();
        }

        public void ImpulseNoise()
        {
            throw new NotImplementedException();
        }

        public void Edges()
        {
            throw new NotImplementedException();
        }

        private static (byte[] pixels, byte[] resultPixels, int stride) GetDataForConvolution() 
        {
            BitmapData sourceData =
                Image.LockBits(new Rectangle(0, 0,
                Image.Width, Image.Height),
                ImageLockMode.ReadOnly,
                Image.PixelFormat);


            byte[] pixels = new byte[sourceData.Stride *
                                          sourceData.Height];


            byte[] resultPixels = new byte[sourceData.Stride *
                                           sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixels, 0,
                                       resultPixels.Length);


            Image.UnlockBits(sourceData);

            return (pixels, resultPixels, sourceData.Stride);
        }

        private static Bitmap GetResultBitmap(byte[] resultPixels) 
        {
            Bitmap resultBitmap = new Bitmap(Image.Width,
                                      Image.Height);


            BitmapData resultData =
                       resultBitmap.LockBits(new Rectangle(0, 0,
                       resultBitmap.Width, resultBitmap.Height),
                       ImageLockMode.WriteOnly,
                       Image.PixelFormat);


            Marshal.Copy(resultPixels, 0, resultData.Scan0,
                                       resultPixels.Length);


            resultBitmap.UnlockBits(resultData);


            return resultBitmap;
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
    }
}
