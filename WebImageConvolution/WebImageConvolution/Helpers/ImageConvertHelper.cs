using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace WebImageConvolution.Helpers
{
    public static class ImageConvertHelper
    {
        public static byte[] ImageToBytes(Image image)
        {
            var converter = new ImageConverter();
            var data = (byte[])converter.ConvertTo(image, typeof(byte[]));
            return data;
        }

        public static string ImageToBase64String(Image image)
        {
            var converter = new ImageConverter();
            var data = (byte[])converter.ConvertTo(image, typeof(byte[]));

            string base64Image = Convert.ToBase64String(data);

            return base64Image;
        }
    }
}
