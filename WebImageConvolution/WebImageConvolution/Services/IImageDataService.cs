using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace WebImageConvolution.Services
{
    public interface IImageDataService
    {
        string Name { get; set; }
        Bitmap OriginalImage { get; set; }
    }
}
