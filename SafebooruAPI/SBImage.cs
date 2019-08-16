using System;
using System.Collections.Generic;
using System.Text;

namespace SafebooruAPI
{
    public class SBImage
    {

        public int Height { get; set; }

        //Score

        public string File_Url { get; set; }

        //Parent ID

        public string Sample_Url { get; set; }
        public int Sample_Width { get; set; }
        public int Sample_Height { get; set; }

        //public string preview_url { get; set; }
        //Ratings

        public string[] Tags { get; set; }
        public int Id { get; set; }
        public int Width { get; set; }

        //Change
        //MD5

        public int Creator_Id { get; set; }

        //HasChildren
        //CreatedAt
        //status

        public string Source { get; set; }

        //hasNotes
        //hasComments
        //previewWidth
        //previewHeight

        /// <summary>
        /// Default Constructor for SBImage in case someone needs to instantiate an object with no values.
        /// </summary>
        public SBImage()
        {

        }

        /// <summary>
        /// Full constructor for SBImage, for when the user has all of the information required from SafeBooru to build the image
        /// </summary>
        /// <param name="height">Height of the original Image</param>
        /// <param name="file_url">Original Image Location</param>
        /// <param name="sample_url">Sample Image Location</param>
        /// <param name="sample_width">Sample Image Width</param>
        /// <param name="sample_height">Sample Image Height</param>
        /// <param name="tags">Tags that SafeBooru had listed for the image</param>
        /// <param name="id">Image ID</param>
        /// <param name="width">Width of the Original Image</param>
        /// <param name="creator_id">ID of the person who created the post</param>
        /// <param name="source">Where the poster found the image if they are not the original creator/poster</param>
        public SBImage(int height, string file_url, string sample_url, int sample_width, int sample_height, string[] tags, int id, int width, int creator_id, string source)
        {
            this.Height = height;
            this.File_Url = file_url;
            this.Sample_Url = sample_url;
            this.Sample_Width = sample_width;
            this.Sample_Height = sample_height;
            this.Tags = tags;
            this.Id = id;
            this.Width = width;
            this.Creator_Id = creator_id;
            this.Source = source;
        }

        /// <summary>
        /// Formatted To String that returns a SBImage object as a string seperated into Main Image info and additional info.
        /// </summary>
        /// <returns>SBImage Object as a string.</returns>
        public override string ToString()
        {
            return $"IMAGE INFO:" +
                $"\n\tFile URL: {File_Url}" +
                $"\n\tW x H: {Width} x {Height}" +
                $"\n\tSource: {Source}" +
                $"\n\nOTHER:" +
                $"\n\tSample URL: {Sample_Url}" +
                $"\n\tSample W x H: {Sample_Width} x {Sample_Height}" +
                $"\n\tTags: {Tags}" +
                $"\n\tID: {Id} | Creator ID: {Creator_Id}";
        }

    }
}
