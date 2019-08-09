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

        public static void LoadImage(string path, string name, string extension)
        {
            string img_path = path + name + extension;
            Bitmap bmp = new Bitmap(img_path);
            Image img = Image.FromFile(img_path);
            RGBRecolor(bmp, img, path, name, extension);
            GrayscaleRecolor(bmp, img, path, name, extension);
            //Image myImg = Image.FromFile(path + name + extension);
            //SaveImage(myImg, path, name, extension);
        }

        public static void SaveImage(Image img, string path, string name, string extension)
        {
            img.Save(path + name + "_copy" + extension);
        }

        public static void RGBRecolor(Bitmap bmp, Image img, string path, string name, string extension)
        {
            int width = bmp.Width;
            int height = bmp.Height;
            Bitmap rbmp = new Bitmap(bmp);
            Bitmap gbmp = new Bitmap(bmp);
            Bitmap bbmp = new Bitmap(bmp);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color p = bmp.GetPixel(x, y);
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    rbmp.SetPixel(x, y, Color.FromArgb(a, r, 0, 0));
                    gbmp.SetPixel(x, y, Color.FromArgb(a, 0, g, 0));
                    bbmp.SetPixel(x, y, Color.FromArgb(a, 0, 0, b));
                }
            }

            SaveImage(rbmp, path, name + "_red", extension);
            SaveImage(gbmp, path, name + "_green", extension);
            SaveImage(bbmp, path, name + "_blue", extension);
        }

        public static void GrayscaleRecolor(Bitmap bmp, Image img, string path, string name, string extension)
        {
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

            SaveImage(d, path, name + "_gray", extension);
        }
    }
}
