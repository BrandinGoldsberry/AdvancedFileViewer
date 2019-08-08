using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PixabayAPI
{
    public class PixabayLoader
    {
        private static string MainAPI_URL = "https://pixabay.com/api/?key=";
        private static string APIKey = "";

        /// <summary>
        /// The URL Builder takes in a search string, breaks the string into individual search terms by splitting on all space characters
        /// and then generates the appropriate Pixabay API URL to be used in order to get a JSON file.
        /// </summary>
        /// <param name="search">String of Search Terms seperated by spaces</param>
        /// <returns>Pixabay URL to obtain JSON</returns>
        public static string URLBuilder(string search)
        {
            string URL = $"{MainAPI_URL}{APIKey}&q=";

            foreach (string term in search.Split(' '))
            {
                URL = URL + $"+{term}";
            }

            return URL;
        }

        public static PixImage GetImage(string Search)
        {
            PixImage retVal = new PixImage();

            return retVal;
        }

        public static string GetJSON(string URL)
        {
            string retVal = "";

            return retVal;
        }

        public static string[] ParseJSON(string JSON)
        {
            string[] retVal = new String[] { };

            return retVal;
        }
    }
}
