﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace WebImageConvolution.Services
{
    public class ImageDataService : IImageDataService
    {
        public string Name { get; set; }
        public Bitmap OriginalImage { get; set; }
    }
}
