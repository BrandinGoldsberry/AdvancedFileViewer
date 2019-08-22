using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AdvancedFileViewer.Models;
using Microsoft.Data.Sqlite;

namespace AdvancedFileViewer.Managers
{
    public class QueryManager
    {
        public static string QueryFilePath;

        /// <summary>
        /// Taking in a query saving it based on what attributes it has been given as well as how it was searched.
        /// </summary>
        /// <param name="Column"></param>
        /// <param name="Query"></param>
        public static void SaveQuery(EntryColumn Column, string Query)
        {
            //saves file and splits different queries by "|" to sort later
            List<Entry> entries = new List<Entry>();
            try
            {
                using (SqliteConnection db =
                        new SqliteConnection("Filename=ImageDatabase.db"))
                {
                    db.Open();

                    //string tableCommand = $"SELECT * FROM Images WHERE Images.Name = {'"' + ImageName + '"'}";
                    string tableCommand = null;
                    long searchInt = 0;
                    if (long.TryParse(Query, out searchInt))
                    {
                        tableCommand = $"CREATE VIEW IF NOT EXISTS {Column.ToString() + "_" + Query} AS SELECT * FROM Images WHERE {Column.ToString()} = {searchInt}";
                    }
                    else
                    {
                        tableCommand = $"CREATE VIEW IF NOT EXISTS {"[" + Column.ToString() + "_" + Query + "]"} AS SELECT * FROM Images WHERE {Column.ToString()} = {'"' + Query + '"'}";
                    }

                    SqliteCommand sqliteCommand = new SqliteCommand(tableCommand, db);

                    sqliteCommand.ExecuteReader();
                    db.Close();
                }
                //my name jeff
            }
            catch (Exception ex)
            {
                
            }
        }

        public static Entry[] LoadQuery(EntryColumn Column, string Query)
        {
            //saves file and splits different queries by "|" to sort later
            List<Entry> entries = new List<Entry>();
            try
            {
                using (SqliteConnection db =
                        new SqliteConnection("Filename=ImageDatabase.db"))
                {
                    db.Open();

                    //string tableCommand = $"SELECT * FROM Images WHERE Images.Name = {'"' + ImageName + '"'}";
                    string tableCommand = null;
                    long searchInt = 0;
                    if (long.TryParse(Query, out searchInt) && Column != EntryColumn.FileSize)
                    {
                        tableCommand = $"SELECT * FROM {Column.ToString() + "_" + Query}";
                    }
                    else
                    {
                        tableCommand = $"SELECT * FROM {"[" + Column.ToString() + "_" + Query + "]"}";
                    }

                    SqliteCommand sqliteCommand = new SqliteCommand(tableCommand, db);

                    SqliteDataReader data = sqliteCommand.ExecuteReader();
                    while (data.Read())
                    {
                        Entry toAdd = new Entry((string)data["Path"], (string)data["Name"], (long)data["IsLocal"] == 1, ulong.Parse((string)data["FileSize"]), (string)data["Searched"], (long)data["Height"], (long)data["Width"]);
                        toAdd.ID = (long)data["id"];
                        entries.Add(toAdd);
                    }
                    db.Close();
                }

            }
            catch (Exception ex)
            {

            }
            return entries.ToArray();
        }

        public static Query[] ListQueries()
        {
            //saves file and splits different queries by "|" to sort later
            List<Query> entries = new List<Query>();
            try
            {
                using (SqliteConnection db =
                        new SqliteConnection("Filename=ImageDatabase.db"))
                {
                    db.Open();

                    //string tableCommand = $"SELECT * FROM Images WHERE Images.Name = {'"' + ImageName + '"'}";
                    string tableCommand = null;
                    tableCommand = $"SELECT name FROM sqlite_master WHERE type = 'view'";

                    SqliteCommand sqliteCommand = new SqliteCommand(tableCommand, db);

                    SqliteDataReader data = sqliteCommand.ExecuteReader();
                    while(data.Read())
                    {
                        Query toAdd = new Query();
                        toAdd.Column = (EntryColumn)Enum.Parse(typeof(EntryColumn), ((string)data["name"]).Split("_")[0]);
                        toAdd.Value = ((string)data["name"]).Split("_")[1];
                        entries.Add(toAdd);
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return entries.ToArray();
        }

    }
}
