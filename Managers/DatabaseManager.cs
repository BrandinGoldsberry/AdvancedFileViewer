using AdvancedFileViewer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Data.Sqlite;

namespace AdvancedFileViewer.Managers
{
    public enum EntryColumn { id, Name, IsLocal, Searched, FileSize, Height, Width, Path }

    public class DatabaseManager
    {

        public static Entry[] GetEntries()
        {
            List<Entry> entries = new List<Entry>();
            try
            {
                using (SqliteConnection db =
                        new SqliteConnection("Filename=ImageDatabase.db"))
                {
                    db.Open();

                    //string tableCommand = $"SELECT * FROM Images WHERE Images.Name = {'"' + ImageName + '"'}";
                    string tableCommand = $"SELECT * FROM Images";

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
                Debug.WriteLine(ex.StackTrace);
            }
            return entries.ToArray();
        }

        public static bool SaveNewEntry(Entry e)
        {
            bool retVal = false;
            try
            {
                using (SqliteConnection db =
                        new SqliteConnection("Filename=ImageDatabase.db"))
                {
                    db.Open();

                    string tableCommand = $"INSERT INTO Images (Name, Path, IsLocal, Searched, Height, Width, FileSize) VALUES ({'"' + e.Name + '"'}, {'"' + e.Path + '"'}, {(e.IsLocal ? 1 : 0)}, {'"' + e.Searched + '"'}, {e.Height}, {e.Width}, {e.FileSize.ToString()})";

                    SqliteCommand sqliteCommand = new SqliteCommand(tableCommand, db);

                    var data = sqliteCommand.ExecuteReader();
                    db.Close();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            return retVal;
        }

        public static bool DeleteEntry(Entry e)
        {
            bool retVal = false;
            try
            {
                using (SqliteConnection db =
                        new SqliteConnection("Filename=ImageDatabase.db"))
                {
                    db.Open();

                    string tableCommand = $"DELETE FROM Images WHERE Name = {'"' + e.Name + '"'} AND Path = {'"' + e.Path + '"'} AND IsLocal = {(e.IsLocal ? 1 : 0)} and Searched = {'"' + e.Searched + '"'} and Height = {e.Height} and Width = {e.Width} and FileSize = {e.FileSize.ToString()}";

                    SqliteCommand sqliteCommand = new SqliteCommand(tableCommand, db);

                    var data = sqliteCommand.ExecuteReader();
                    db.Close();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            return retVal;
        }

        public static bool UpdateEntry(Entry Old, Entry New)
        {
            bool retVal = false;
            try
            {
                using (SqliteConnection db =
                        new SqliteConnection("Filename=ImageDatabase.db"))
                {
                    db.Open();

                    string tableCommand = $"DELETE FROM Images WHERE Name = {'"' + Old.Name + '"'} AND Path = {'"' + Old.Path + '"'} AND IsLocal = {(Old.IsLocal ? 1 : 0)} and Searched = {'"' + Old.Searched + '"'} and Height = {Old.Height} and Width = {Old.Width} and FileSize = {Old.FileSize.ToString()}";

                    SqliteCommand sqliteCommand = new SqliteCommand(tableCommand, db);

                    var data = sqliteCommand.ExecuteReader();

                    tableCommand = $"INSERT INTO Images (Name, Path, IsLocal, Searched, Height, Width, FileSize) VALUES ({'"' + New.Name + '"'}, {'"' + New.Path + '"'}, {(New.IsLocal ? 1 : 0)}, {'"' + New.Searched + '"'}, {New.Height}, {New.Width}, {New.FileSize.ToString()})";

                    sqliteCommand = new SqliteCommand(tableCommand, db);

                    data = sqliteCommand.ExecuteReader();

                    db.Close();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            return retVal;
        }

        /// <summary>
        /// Returns all etries that meets the Query of the column
        /// </summary>
        /// <param name="Column">Which column to search by</param>
        /// <param name="Query">What value to search by</param>
        /// <returns></returns>
        public static Entry[] QueryDatabase(EntryColumn Column, string Query)
        {
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
                        tableCommand = $"SELECT * FROM Images WHERE {Column.ToString()} = {searchInt}";
                    }
                    else
                    {
                        tableCommand = $"SELECT * FROM Images WHERE {Column.ToString()} = {'"' + Query + '"'}";
                    }

                    SqliteCommand sqliteCommand = new SqliteCommand(tableCommand, db);

                    SqliteDataReader data = sqliteCommand.ExecuteReader();
                    while (data.Read())
                    {
                        entries.Add(new Entry((string)data["Path"], (string)data["Name"], (long)data["IsLocal"] == 1, ulong.Parse((string)data["FileSize"]), (string)data["Searched"], (long)data["Height"], (long)data["Width"]));
                    }
                    db.Close();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            return entries.ToArray();
        }

        public static void ConnectToDatabase()
        {
            using (SqliteConnection db =
                    new SqliteConnection("Filename=ImageDatabase.db"))
            {
                db.Open();

                string tableCommand = @"CREATE TABLE IF NOT EXISTS Images (
                                        id    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                        Name  TEXT NOT NULL,
	                                    Path  TEXT NOT NULL,
	                                    IsLocal   INTEGER NOT NULL,
	                                    Searched  TEXT,
	                                    Height    INTEGER NOT NULL,
	                                    Width INTEGER NOT NULL,
	                                    FileSize  TEXT NOT NULL
                                        ); ";

                SqliteCommand sqliteCommand = new SqliteCommand(tableCommand, db);

                sqliteCommand.ExecuteReader();
                db.Close();
            }
        }

    }
}
