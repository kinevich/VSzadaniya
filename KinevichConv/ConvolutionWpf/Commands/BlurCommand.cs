using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Catel.MVVM;
using Catel.Windows.Interactivity;

namespace ConvolutionWpf.Commands
{
    public class BlurCommand : Command
    {
        private readonly Func<WriteableBitmap> _imageFactory;

        public BlurCommand(Func<WriteableBitmap> imageFactory)
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

            var result = Blur(image.PixelHeight, image.PixelWidth, image.BackBufferStride, pixels, resultPixels).Result;

            //int index = j * image.BackBufferStride + 4 * i;
            //todo
            image.WritePixels(new Int32Rect(0, 0, image.PixelWidth, image.PixelHeight), result, image.BackBufferStride, 0);
        }

        private static async Task<byte[]> Blur(int pixelHeight, int pixelWidth, int backBufferStride,
                                               byte[] pixels, byte[] resultPixels)
        {
            var taleHeight = pixelHeight / 2;
            var taleWidth = pixelWidth / 2;

            var tasks = new[]
            {
                Task.Run(() => BlurTale(backBufferStride, pixels, resultPixels, 0, 0, taleHeight, taleWidth)),
                Task.Run(() => BlurTale(backBufferStride, pixels, resultPixels, 0, taleWidth, taleHeight, pixelWidth)),
                Task.Run(() => BlurTale(backBufferStride, pixels, resultPixels, taleHeight, 0, pixelHeight, taleWidth)),
                Task.Run(() => BlurTale(backBufferStride, pixels, resultPixels, taleHeight, taleWidth, pixelHeight, pixelWidth))
            };

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return resultPixels;
        }

        private static void BlurTale(int backBufferStride, byte[] pixels, byte[] resultPixels, 
                                     int heightStart, int widthStart, int heightEnd, int widthEnd)
        {
            int kSize = 11;
            int kIndent = (kSize - 1) / 2;
            for (int i = kIndent + widthStart; i <= widthEnd - kIndent - 1; ++i)
            {
                for (int j = kIndent + heightStart; j <= heightEnd - kIndent - 1; ++j)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        double sum = 0;
                        for (int jStep = 0; jStep < kSize; jStep++)
                        {
                            for (int iStep = 0; iStep < kSize; iStep++)
                            {
                                int index = (j - kIndent + jStep) * backBufferStride + 4 * (i - kIndent + iStep);
                                sum += pixels[index + c];
                            }
                        }
                        int convIndex = j * backBufferStride + 4 * i;
                        resultPixels[convIndex + c] = (byte)(sum / (kSize * kSize));
                    }
                }
            }
        }

        protected override void Execute(object parameter, bool ignoreCanExecuteCheck)
        {
            ExecuteCommand();
        }
    }
}