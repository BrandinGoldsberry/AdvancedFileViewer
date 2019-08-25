using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace SafebooruAPI
{
    public class SafeBooruLoader
    {
        private static string host_site = "safebooru.org";
        private static string path = "index.php";

        /// <summary>
        /// Takes in a string that contains all of the tags the user wishes to search for seperated by spaces. (i.e. "blonde sword Naruto_Shippuden"). Uses tags to determine the
        /// URL required to query for images with those tags.
        /// </summary>
        /// <param name="tagList">A list of tags the user wishes to search for in string format seperated by a single space</param>
        /// <returns>URL for GetXML query</returns>
        public static string QueryBuilder(string tagList)
        {
            string query = "page=dapi&s=post&q=index&limit=20&tags=";
            string[] tags = tagList.Split(' ');

            for (int i = 0; i < tags.Length; i++)
            {
                if (i == 0)
                {
                    query += $"{tags[i]}";
                }
                else
                {
                    query += $"+{tags[i]}";
                }
            }

            return query;
        }

        /// <summary>
        /// Takes in a string form of the URL built by QueryBuilder retrieves XML for the first 20 images that SafeBooru finds based off of the provded tags, and creates a formatted string version
        /// of the XML.
        /// </summary>
        /// <param name="query">The query is a URL build by QueryBuilder</param>
        /// <returns>Formatted XML for first 20 images from SafeBooru</returns>
        public static string GetXML(string query)
        {
            string websiteURL = $"https://{host_site}/{path}?{query}";
            XmlTextReader reader = new XmlTextReader(websiteURL);

            //Console.WriteLine($"XML Test: {reader}\n\n");
            string XML = "";

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        //Console.Write($"<{reader.Name}");


                        while (reader.MoveToNextAttribute())
                        {
                            //Console.Write($"{reader.Name} = '{reader.Value}' |");
                            XML += $"{reader.Name}={reader.Value}|";
                        }
                        //Console.Write($"\\");
                        XML += "\\";
                        break;
                }
            }
            //Console.WriteLine($"XML: \n\n\n{XML}");

            return XML;
        }

        /// <summary>
        /// Parses the formatted XML from GetXML, gets the information from each image and creates a SBImage object which it then adds to a list it will return at the end.
        /// </summary>
        /// <param name="XML">Formatted XML recieved from GetXML</param>
        /// <returns>A list of 20 SBImage objects created from the XML data for the 20 images SafeBooru Provided.</returns>
        public static List<SBImage> ParseXMLAndCreateImageList(string XML)
        {
            List<SBImage> images = new List<SBImage> { };

            string[] SBImageXMLs = XML.Split('\\');

            for (int i = 1; i < SBImageXMLs.Length - 1; i++)
            {

                string[] imageData = SBImageXMLs[i].Split('|');

                if(imageData.Length >= 24)
                {
                    for (int o = 0; o < imageData.Length; o++)
                    {
                        //Console.WriteLine(imageData[o]);
                        imageData[o] = imageData[o].Substring(imageData[o].IndexOf('=') + 1);
                    }

                    SBImage img = new SBImage(Int32.Parse(imageData[0]), imageData[2], imageData[4], Int32.Parse(imageData[5]), Int32.Parse(imageData[6]),
                        imageData[9].Split(' '), Int32.Parse(imageData[10]), Int32.Parse(imageData[11]), Int32.Parse(imageData[14]), imageData[18]);

                    images.Add(img);
                }
            }

            return images;
        }
    }
}