using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ConvolutionWpf.Commands.Helpers
{
    public class ConvolutionHelper
    {
        public static byte[] ConvolutionFilter(WriteableBitmap image, double[,] filterMatrix)
        {
            var pixels = new byte[image.PixelHeight * image.BackBufferStride];
            image.CopyPixels(pixels, image.BackBufferStride, 0);

            var resultPixels = new byte[pixels.Length];

            var filterWidth = filterMatrix.GetLength(1);
            var filterOffset = (filterWidth - 1) / 2;

            for (int i = filterOffset; i < image.Width - filterOffset; i++)
            {
                for (int j = filterOffset; j < image.Height - filterOffset; j++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        double sum = 0;
                        for (int filterY = 0; filterY < filterWidth; filterY++)
                        {
                            for (int filterX = 0; filterX < filterWidth; filterX++)
                            {
                                int index = (j - filterOffset + filterY) * image.BackBufferStride + 4 * (i - filterOffset + filterX);
                                sum += pixels[index + c] * filterMatrix[filterY, filterX];
                            }
                        }                        
                        int convIndex = j * image.BackBufferStride + 4 * i;
                        resultPixels[convIndex + c] = (byte)sum;
                    }
                }
            }

            return resultPixels;
        }

        public static double[,] GetBoxBlurFilterMatrix(int length) 
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


    }
}
