using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AdvancedFileViewer.Models;

namespace AdvancedFileViewer.Managers
{
    public class QueryManager
    {
        public static string QueryFilePath;

        /// <summary>
        /// Taking in a query saving it based on what attributes it has been given as well as how it was searched.
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="searchBy"></param>
        public static void SaveQuery(EntryColumn attributes, string searchBy)
        {
            //saves file and splits different queries by "|" to sort later
            File.AppendAllText("queries.txt", attributes + "," + searchBy + "|");
        }

        public static string LoadQuery()
        {
            //opens file
            string fileText = File.ReadAllText("queries.txt");
            //returns string encompassing file contents
            return fileText;
        }

    }
}
