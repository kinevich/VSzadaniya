using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using Catel.MVVM;

namespace ConvolutionWpf.Commands
{
    public class ImpulseNoiseCommand : Command
    {
        private readonly Func<WriteableBitmap> _imageFactory;

        public ImpulseNoiseCommand(Func<WriteableBitmap> imageFactory)
            : base(() => { })
        {
            _imageFactory = imageFactory;
        }

        private void ExecuteCommand()
        {
            var image = _imageFactory();
            if (image == null)
                return;

            byte[] resultPixels = RemoveNoise(image, sensetivity: 3);

            image.WritePixels(new Int32Rect(0, 0, image.PixelWidth, image.PixelHeight), resultPixels, image.BackBufferStride, 0);

        }

        private byte[] RemoveNoise(WriteableBitmap image, int sensetivity)
        {
            var pixels = new byte[image.PixelHeight * image.BackBufferStride];
            image.CopyPixels(pixels, image.BackBufferStride, 0);

            var filter = new List<int>(sensetivity);//list with 3 capacity

            var resultPixels = new byte[pixels.Length];

            var filterOffset = filter.Capacity - 1;

            for (int i = 0; i < image.Width - filterOffset; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        //double sum = 0;
                        //for (int filterY = 0; filterY < filterWidth; filterY++)
                        //{
                        //    for (int filterX = 0; filterX < filterWidth; filterX++)
                        //    {
                        //        int index = (j - filterOffset + filterY) * image.BackBufferStride + 4 * (i - filterOffset + filterX);
                        //        sum += pixels[index + c] * filterMatrix[filterY, filterX];
                        //    }
                        //}

                        //int convIndex = j * image.BackBufferStride + 4 * i;
                        //resultPixels[convIndex + c] = (byte)sum;

                        for (int k = 0; k < filter.Capacity; k++) 
                        {
                            int index = j * image.BackBufferStride + 4 * (i + k);
                            filter.Add(pixels[index + c]);
                        }

                        filter.Sort();
                        var filteredPixel = filter[filter.Capacity / 2];

                        int convIndex = j * image.BackBufferStride + 4 * i;
                        resultPixels[convIndex + c] = (byte)filteredPixel;

                        filter.Clear();
                    }
                }
            }

            return resultPixels;
        }

        protected override void Execute(object parameter, bool ignoreCanExecuteCheck)
        {
            ExecuteCommand();
        }
    }
}