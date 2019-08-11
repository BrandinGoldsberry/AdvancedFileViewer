using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageAPI
{
    public class ImageRecolor
    {
        /// <summary>
        /// This method saves the image that is passed in 
        /// </summary>
        /// <param name="img">The image that is being saved</param>
        /// <param name="path">The directory to the image</param>
        /// <param name="name">The name of the image</param>
        /// <param name="extension">The extension of the image</param>
        public static void SaveImage(Image img, string path, string name, string extension)
        {
            img.Save(path + name + "_copy" + extension);
        }

        /// <summary>
        /// This method recolors the image to the user's RGB values
        /// and returns the new bitmap to the user
        /// </summary>
        /// <param name="bmp">The image being modified to the user's specifications</param>
        /// <param name="path">The directory to the image</param>
        /// <param name="name">The name of the image</param>
        /// <param name="extension">The extension of the image</param>
        /// <param name="redVal">The user's specified red value</param>
        /// <param name="greenVal">The user's specified green value</param>
        /// <param name="blueVal">The user's specified blue value</param>
        public static Bitmap RGBRecolor(Bitmap bmp, string path, string name, string extension, byte redVal, byte greenVal, byte blueVal)
        {
            int width = bmp.Width;
            int height = bmp.Height;
            Bitmap nbmp = new Bitmap(bmp);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color p = bmp.GetPixel(x, y);
                    int a = p.A;

                    nbmp.SetPixel(x, y, Color.FromArgb(a, redVal, greenVal, blueVal));
                }
            }
            return nbmp;
        }

        /// <summary>
        /// This method recolors the image to grayscale
        /// and returns the new bitmap to the user
        /// </summary>
        /// <param name="bmp">The image being modified to grayscale</param>
        /// <param name="path">The directory to the image</param>
        /// <param name="name">The name of the image</param>
        /// <param name="extension">The extension of the image</param>
        public static Bitmap GrayscaleRecolor(Bitmap bmp, string path, string name, string extension)
        {
            //A bitmap variable that uses the passed in bitmap image to be grayscaled
            Bitmap d = new Bitmap(bmp.Width, bmp.Height);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int x = 0; x < bmp.Height; x++)
                {
                    Color oc = bmp.GetPixel(i, x);
                    int grayScale = (int)((oc.R * 0.3) + (oc.G * 0.59) + (oc.B * 0.11));
                    Color nc = Color.FromArgb(oc.A, grayScale, grayScale, grayScale);
                    d.SetPixel(i, x, nc);
                }
            }
            return d;
        }
    }
}
