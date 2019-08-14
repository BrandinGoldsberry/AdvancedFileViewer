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
        /// This method loads a signle image
        /// </summary>
        /// <param name="path">The directory to the image</param>
        /// <param name="name">The name of the image</param>
        /// <param name="extension">The entension of the image</param>
        //public static Bitmap LoadImage(string path, string name, string extension)
        //{
        //    string img_path = path + name + extension;

        //    //Creates a bitmap version of the image and returns it to the user
        //    return new Bitmap(img_path);
        //}

        /// <summary>
        /// This method saves the image that is passed in 
        /// </summary>
        /// <param name="img">The image that is being saved</param>
        /// <param name="path">The directory to the image</param>
        /// <param name="name">The name of the image</param>
        /// <param name="extension">The extension of the image</param>
        private static void SaveImage(Image img, string path, string name, string extension)
        {
            img.Save(path + name + extension);
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
        public static void RGBRecolor(string path, string name, string extension, string newName, byte redVal, byte greenVal, byte blueVal)
        {
            string img_path = path + name + extension;
            Bitmap bmp = new Bitmap(img_path);
            int width = bmp.Width;
            int height = bmp.Height;
            using (Bitmap nbmp = new Bitmap(bmp))
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color p = bmp.GetPixel(x, y);
                        int a = p.A;

                        int redPix = (redVal + nbmp.GetPixel(x, y).R) / 2;
                        int greenPix = (greenVal + nbmp.GetPixel(x, y).G) / 2;
                        int bluePix = (blueVal + nbmp.GetPixel(x, y).B) / 2;

                        nbmp.SetPixel(x, y, Color.FromArgb(a, redPix, greenPix, bluePix));
                    }
                }
                SaveImage(nbmp, path, newName, extension);
            }
        }

        /// <summary>
        /// This method recolors the image to grayscale
        /// and returns the new bitmap to the user
        /// </summary>
        /// <param name="bmp">The image being modified to grayscale</param>
        /// <param name="path">The directory to the image</param>
        /// <param name="name">The name of the image</param>
        /// <param name="extension">The extension of the image</param>
        public static void GrayscaleRecolor(string path, string name, string extension, string newName)
        {
            string img_path = path + name + extension;
            Bitmap bmp = new Bitmap(img_path);
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
            SaveImage(d, path, newName, extension);
        }

        /// <summary>
        /// This method resizes an image the user passes in
        /// and then returns it to the user
        /// </summary>
        /// <param name="imgResize">The image to be resized</param>
        /// <param name="width">The width of the imageto be resized</param>
        /// <param name="height">the height of the imageto be resized</param>
        /// <returns></returns>
        public static void ResizeImage(string path, string name, string extension, string newName, int width, int height)
        {
            Size size = new Size(width, height);
            string img_path = path + name + extension;
            using (Bitmap bmp = new Bitmap(img_path))
            {
                Bitmap reSized = new Bitmap(bmp, size);
                SaveImage(reSized, path, newName, extension);
            }
        }
    }
}
