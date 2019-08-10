using System;
using System.Collections.Generic;
using System.Text;

namespace PixabayAPI
{
    public class PixImage
    {
        #region Vars
        public string LargeImageURL { get; set; }
        public int webformatHeight { get; set; }
        public int webformatWidth { get; set; }

        //public int likes { get; set; }

        public int imageWidth { get; set; }
        public int id { get; set; }

        //public int user_id { get; set; }
        //public int views { get; set; }
        //public int comments { get; set; }

        public string pageURL { get; set; }
        public int imageHeight { get; set; }
        public string webformatURL { get; set; }
        public string type { get; set; }

        //public int previewHeight { get; set; }

        public string[] tags { get; set; }

        //public int downloads { get; set; }
        //public string user { get; set; }
        //public int favourites { get; set; }

        public int imageSize { get; set; }

        //public int previewWidth { get; set; }
        //public string userImageURL { get; set; }
        //public string previewURL { get; set; }
        #endregion



        /// <summary>
        /// Default constructor, takes in nothing and adjusts nothing, here for testing purposes or if someone ever needed to instantiate
        /// an object with out providing data.
        /// </summary>
        public PixImage()
        {

        }

        /// <summary>
        /// PixImage constuctor that takes in and sets all of the data the user may need. 
        /// </summary>
        /// <param name="LargeImageURL">URL that would take you to exclusively the image</param>
        /// <param name="webformatHeight">Height for webpage formatted version of the image</param>
        /// <param name="webformatWidth">Width for the webpage formatted version of the image</param>
        /// <param name="imageWidth">The true width of the image itself</param>
        /// <param name="id">The Pixabay ID for the image</param>
        /// <param name="pageURL">URL that takes you to the pixabay page that contains the image</param>
        /// <param name="imageHeight">The true height of the image</param>
        /// <param name="webformatURL">The URL for the webpage formatted version of the image</param>
        /// <param name="type">Type of Image (Photo, Vector, etc.)</param>
        /// <param name="tags">Tags the website has provided for the image</param>
        /// <param name="imageSize">The size of the image</param>
        public PixImage(string LargeImageURL, int webformatHeight, int webformatWidth, int imageWidth, int id, string pageURL, int imageHeight, string webformatURL,
            string type, string[] tags, int imageSize)
        {
            this.LargeImageURL = LargeImageURL;
            this.webformatHeight = webformatHeight;
            this.webformatWidth = webformatWidth;
            this.imageWidth = imageWidth;
            this.id = id;
            this.pageURL = pageURL;
            this.imageHeight = imageHeight;
            this.webformatURL = webformatURL;
            this.type = type;
            this.tags = tags;
            this.imageSize = imageSize;
        }

        /// <summary>
        /// ToString method for the PixImage object. Takes all of the information that makes up the image, separates it into 3 categories,
        /// and displays it in a formatted string format.
        /// </summary>
        /// <returns>Formatted string form of image data</returns>
        public override string ToString()
        {
            return $"URLs\n\nImage URL: {LargeImageURL}\n" +
                $"Page URL: {pageURL}\n" +
                $"WebFormat URL: {webformatURL}" +
                $"\n\n---------------------------------\n\n" +
                $"Size Info\n\nWebformat W x H: {webformatWidth} x {webformatHeight}\n" +
                $"Image W x H: {imageWidth} x {imageHeight}\n" +
                $"Image Size: {imageSize}" +
                $"\n\n---------------------------------\n\n" +
                $"Other\n\nID: {id}\n" +
                $"Type: {type}\n" +
                $"Tags: {tags}";
        }
    }
}
