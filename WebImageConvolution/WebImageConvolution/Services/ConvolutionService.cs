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
        public Bitmap Blur(Bitmap originalBitmap)
        {
            BitmapData sourceData =
                originalBitmap.LockBits(new Rectangle(0, 0,
                originalBitmap.Width, originalBitmap.Height),
                ImageLockMode.ReadOnly,
                originalBitmap.PixelFormat);


            byte[] pixels = new byte[sourceData.Stride *
                                          sourceData.Height];


            byte[] resultPixels = new byte[sourceData.Stride *
                                           sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixels, 0,
                                       resultPixels.Length);


            originalBitmap.UnlockBits(sourceData);

            var filterMatrix = GetBoxBlurFilterMatrix(3);


            var filterWidth = filterMatrix.GetLength(1);
            var filterOffset = filterWidth / 2;
            int index;

            for (int i = filterOffset; i < originalBitmap.Width - filterOffset; i++)
            {
                for (int j = filterOffset; j < originalBitmap.Height - filterOffset; j++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        double sum = 0;
                        for (int filterY = 0; filterY < filterWidth; filterY++)
                        {
                            for (int filterX = 0; filterX < filterWidth; filterX++)
                            {
                                index = (j + filterY - filterOffset) * sourceData.Stride + 4 * (i + filterX - filterOffset);
                                sum += pixels[index + c] * filterMatrix[filterY, filterX];
                            }
                        }
                        index = j * sourceData.Stride + 4 * i;
                        resultPixels[index + c] = (byte)sum;
                    }
                }
            }

            Bitmap resultBitmap = new Bitmap(originalBitmap.Width,
                                      originalBitmap.Height);


            BitmapData resultData =
                       resultBitmap.LockBits(new Rectangle(0, 0,
                       resultBitmap.Width, resultBitmap.Height),
                       ImageLockMode.WriteOnly,
                       PixelFormat.Format32bppArgb);


            Marshal.Copy(resultPixels, 0, resultData.Scan0,
                                       resultPixels.Length);


            resultBitmap.UnlockBits(resultData);


            return resultBitmap;
        }

        public Bitmap Contrast(Bitmap originalBitmap)
        {
            throw new NotImplementedException();
        }

        public Bitmap Edges(Bitmap originalBitmap)
        {
            throw new NotImplementedException();
        }

        public Bitmap Flip(Bitmap originalBitmap)
        {
            throw new NotImplementedException();
        }

        public Bitmap GrayScale(Bitmap originalBitmap)
        {
            throw new NotImplementedException();
        }

        public Bitmap ImpulseNoise(Bitmap originalBitmap)
        {
            throw new NotImplementedException();
        }

        public Bitmap Log(Bitmap originalBitmap)
        {
            throw new NotImplementedException();
        }

        public Bitmap Negate(Bitmap originalBitmap)
        {
            throw new NotImplementedException();
        }

        public Bitmap SobelEdges(Bitmap originalBitmap)
        {
            throw new NotImplementedException();
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
