using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace WebImageConvolution.Services
{
    public interface IConvolutionService
    {
        Bitmap GrayScale(Bitmap originalBitmap);
        Bitmap Blur(Bitmap originalBitmap);
        Bitmap Log(Bitmap originalBitmap);
        Bitmap Contrast(Bitmap originalBitmap);
        Bitmap Flip(Bitmap originalBitmap);
        Bitmap Negate(Bitmap originalBitmap);
        Bitmap SobelEdges(Bitmap originalBitmap);
        Bitmap ImpulseNoise(Bitmap originalBitmap);
        Bitmap Edges(Bitmap originalBitmap);
    }
}