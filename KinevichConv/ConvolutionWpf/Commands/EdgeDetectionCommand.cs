using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Catel.MVVM;
using ConvolutionWpf.Commands.Helpers;

namespace ConvolutionWpf.Commands
{
    public class EdgeDetectionCommand : Command
    {
        private readonly Func<WriteableBitmap> _imageFactory;

        public EdgeDetectionCommand(Func<WriteableBitmap> imageFactory)
            : base(() => { })
        {
            _imageFactory = imageFactory;
        }

        public void ExecuteCommand()
        {
            var image = _imageFactory();
            if (image == null)
                return;

            var resultPixels = DetectEdges(image);

            image.WritePixels(new Int32Rect(0, 0, image.PixelWidth, image.PixelHeight), resultPixels, image.BackBufferStride, 0);
        }

        private byte[] DetectEdges(WriteableBitmap image)
        {
            var filter = ConvolutionHelper.GetEdgeDetectionFilterMatrix(11);
            var resultPixels = ConvolutionHelper.ConvolutionFilter(image, filter);

            return resultPixels;
        }


        protected override void Execute(object parameter, bool ignoreCanExecuteCheck)
        {
            ExecuteCommand();
        }
    }
}