using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace WebImageConvolution.Services
{
    public interface IConvolutionService
    {
        Bitmap Image { get; set; }
        void GrayScale();
        void Blur();
        void Log();
        void Contrast();
        void Flip();
        void Negate();
        void SobelEdges();
        void ImpulseNoise();
        void Edges();
    }
}