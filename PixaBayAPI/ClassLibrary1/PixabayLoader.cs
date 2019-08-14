using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace PixabayAPI
{
    public class PixabayLoader
    {
        private static string host_site = "pixabay.com";
        private static string site_path = "api/";

        /// <summary>
        /// The URL Builder takes in a search string, breaks the string into individual search terms by splitting on all space characters
        /// and then generates the appropriate Pixabay API URL to be used in order to get a JSON file.
        /// </summary>
        /// <param name="search">String of Search Terms seperated by spaces</param>
        /// <returns>Pixabay URL to obtain JSON</returns>
        public static string searchTermBuilder(string search)
        {
            string searchTerms = "key=13251626-9e47e152399234e0e6b4b9d73&q=";
            string[] terms = search.Split(' ');

            for (int i = 0; i < terms.Length; i++)
            {
                if(i == 0)
                {
                    searchTerms += $"{terms[i]}";
                }
                else
                {
                    searchTerms += $"+{terms[i]}";
                }
            }

            return searchTerms;
        }


        /// <summary>
        /// Takes in the searchTerms built by the URL Builder and uses them to send a web request in order to get the top 20 images for the
        /// search terms provided assuming that many exist. 
        /// </summary>
        /// <param name="searchTerms">The end of the URL including the key and terms the user wishes to search by
        /// built by the URL Builder Method of the Pixabay loader class.</param>
        /// <returns>The JSON Data for these images in string format.</returns>
        public static string GetJSON(string searchTerms)
        {
            WebRequest wrGETURL;
            UriBuilder ur = new UriBuilder();
            ur.Host = host_site;
            ur.Path = site_path;
            ur.Query = searchTerms;
            wrGETURL = WebRequest.Create(ur.Uri);

            Console.WriteLine("Var thing: " + ur.Uri);

            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

            string jsonData = objReader.ReadLine();

            return jsonData;
        }

        /// <summary>
        /// Takes in the string version of the JSON data for the images that were searched for, takes said data and cleans it up, and then
        /// assigns the important informatio to the correct variables in order to create a PixImage object, and then adds that object to a list.
        /// Once all of the objects from the JSON have been made, the list is returned.
        /// </summary>
        /// <param name="jsonString">String version of the JSON data retrieved by PixabayLoaders GetJSON method.</param>
        /// <returns>A list of PixImage objects</returns>
        public static List<PixImage> ParseJSONIntoImageList(string jsonString)
        {
            string[] imageJSONs = jsonString.Split('{');
            List<PixImage> images = new List<PixImage> { };

            for (int i = 2; i < imageJSONs.Length; i++)
            {
                string[] data = imageJSONs[i].Split(new string[] { ",\"" }, StringSplitOptions.None);

                for (int o = 0; o < data.Length; o++)
                {
                    data[o] = data[o].Substring(data[o].IndexOf(':') + 1);
                    data[o] = data[o].Replace("\"", "");
                    data[o] = data[o].Replace("}", "");
                }

                PixImage img = new PixImage(data[0], Int32.Parse(data[1]), Int32.Parse(data[2]), Int32.Parse(data[4]), Int32.Parse(data[5]),
                    data[9], Int32.Parse(data[10]), data[11], data[12], data[14].Split(new string[] { ", " }, StringSplitOptions.None), Int32.Parse(data[18]));

                images.Add(img);
            }

            return images;
        }
    }
}
