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
            var filter = GetEdgeDetectionFilterMatrix(3);
            var resultPixels = ConvolutionHelper.ConvolutionFilter(image, filter);

            return resultPixels;
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

        protected override void Execute(object parameter, bool ignoreCanExecuteCheck)
        {
            ExecuteCommand();
        }
    }
}