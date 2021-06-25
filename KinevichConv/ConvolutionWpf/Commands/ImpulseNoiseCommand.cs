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

            var filter = new List<int>(sensetivity);//list with sensitivity capacity

            var resultPixels = new byte[pixels.Length];

            var filterOffset = filter.Capacity / 2;
            int index;
            int filteredPixel;

            for (int i = filterOffset; i < image.Width - filterOffset; i++)
            {
                for (int j = filterOffset; j < image.Height; j++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        for (int k = 0; k < filter.Capacity; k++) 
                        {
                            index = j * image.BackBufferStride + 4 * (i - filterOffset + k);
                            filter.Add(pixels[index + c]);
                        }

                        filter.Sort();
                        filteredPixel = filter[filter.Capacity / 2];

                        index = j * image.BackBufferStride + 4 * i;
                        resultPixels[index + c] = (byte)filteredPixel;

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