using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Catel.MVVM;
using Catel.Windows.Interactivity;
using ConvolutionWpf.Commands.Helpers;

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

            var filter = ConvolutionHelper.GetBoxBlurFilterMatrix(9);
            var resultPixels = ConvolutionHelper.ConvolutionFilter(image, filter);

            image.WritePixels(new Int32Rect(0, 0, image.PixelWidth, image.PixelHeight), resultPixels, image.BackBufferStride, 0);
        }

        //private static async Task<byte[]> BlurAsync()
        //{
        //    var taleHeight = pixelHeight / 2;
        //    var taleWidth = pixelWidth / 2;

        //    var tasks = new[]
        //    {
        //        Task.Run(() => ConvolutionHelper.),

        //        Task.Run(() => ConvolutionHelper.),

        //        Task.Run(() => ConvolutionHelper.),

        //        Task.Run(() => ConvolutionHelper.).
        //    };

        //    await Task.WhenAll(tasks).ConfigureAwait(false);
        //}

        protected override void Execute(object parameter, bool ignoreCanExecuteCheck)
        {
            ExecuteCommand();
        }
    }
}