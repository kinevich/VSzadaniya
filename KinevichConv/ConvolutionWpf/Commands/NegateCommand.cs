using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Catel.MVVM;

namespace ConvolutionWpf.Commands
{
    public class NegateCommand : Command
    {
        private readonly Func<WriteableBitmap> _imageFactory;

        public NegateCommand(Func<WriteableBitmap> imageFactory)
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

            for (int i = 0; i < image.PixelWidth; ++i)
            {
                for (int j = 0; j < image.PixelHeight; ++j)
                {
                    int index = j * image.BackBufferStride + 4 * i;

                    for (int c = 0; c < 3; ++c)
                    {
                        byte negative = 255;
                        resultPixels[index + c] = (byte)(negative - pixels[index + c]);
                    }

                    resultPixels[index + 3] = 255; // альфа-канал
                }
            }

            image.WritePixels(new Int32Rect(0, 0, image.PixelWidth, image.PixelHeight), resultPixels, image.BackBufferStride, 0);
        }

        protected override void Execute(object parameter, bool ignoreCanExecuteCheck)
        {
            ExecuteCommand();
        }
    }
}