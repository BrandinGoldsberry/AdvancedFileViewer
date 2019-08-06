using System;
using System.Collections.Generic;
using System.Text;

namespace PixabayAPI
{
    public class PixImage
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Searched { get; set; }
        public int FileSize { get; set; }

        private int[] imageSize;
        public int[] ImageSize
        {
            get { return imageSize; }
            set { imageSize = value; }
        }

        public string tag { get; set; }

        public PixImage()
        {

        }
    }
}
