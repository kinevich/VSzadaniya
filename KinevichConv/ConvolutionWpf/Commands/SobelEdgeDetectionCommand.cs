using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Catel.MVVM;

namespace ConvolutionWpf.Commands
{
    public class SobelEdgeDetectionCommand : Command
    {
        private readonly Func<WriteableBitmap> _imageFactory;

        public SobelEdgeDetectionCommand(Func<WriteableBitmap> imageFactory)
            : base(() => { })
        {
            _imageFactory = imageFactory;
        }

        private void ExecuteCommand()
        {
            var image = _imageFactory();
            if (image == null)
                return;

            byte[] resultPixels = DetectEdges(image);

            image.WritePixels(new Int32Rect(0, 0, image.PixelWidth, image.PixelHeight), resultPixels, image.BackBufferStride, 0);
        }

        private byte[] DetectEdges(WriteableBitmap image)
        {
            var pixels = new byte[image.PixelHeight * image.BackBufferStride];
            image.CopyPixels(pixels, image.BackBufferStride, 0);

            double[,] xKernel = new double[3, 3] { { -1 / 9.0, 0, 1 / 9.0 },
                                                   { -2 / 9.0, 0, 2 / 9.0 },
                                                   { -1 / 9.0, 0, 1 / 9.0 } 
            };

            double[,] yKernel = new double[3, 3] { { -1 / 9.0, -2 / 9.0 , -1 / 9.0 },
                                                   { 0, 0, 0 },
                                                   { 1 / 9.0, 2 / 9.0, 1 / 9.0 } 
            };

            var resultPixels = new byte[pixels.Length];

            var filterWidth = xKernel.GetLength(1);
            var filterOffset = (filterWidth - 1) / 2;
            int index;

            for (int i = filterOffset; i < image.Width - filterOffset; i++)
            {
                for (int j = filterOffset; j < image.Height - filterOffset; j++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        double xColor = 0;
                        double yColor = 0;
                        for (int filterY = 0; filterY < filterWidth; filterY++)
                        {
                            for (int filterX = 0; filterX < filterWidth; filterX++)
                            {
                                index = (j + filterY - filterOffset) * image.BackBufferStride + 4 * (i + filterX - filterOffset);
                                xColor += pixels[index + c] * xKernel[filterY, filterX];
                                yColor += pixels[index + c] * yKernel[filterY, filterX];
                            }
                        }
                        index = j * image.BackBufferStride + 4 * i;
                        resultPixels[index + c] = (byte)Math.Sqrt((xColor * xColor) + (yColor * yColor));
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