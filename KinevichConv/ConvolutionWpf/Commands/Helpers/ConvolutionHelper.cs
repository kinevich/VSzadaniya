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
            var filterOffset = filterWidth / 2;
            int index;

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
                                index = (j + filterY - filterOffset) * image.BackBufferStride + 4 * (i + filterX - filterOffset);
                                sum += pixels[index + c] * filterMatrix[filterY, filterX];
                            }
                        }
                        index = j * image.BackBufferStride + 4 * i;
                        resultPixels[index + c] = (byte)sum;
                    }
                }
            }

            return resultPixels;
        }
    }
}
