using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedFileViewer.Models
{
    /// <summary>
    /// Entry class for database serialization
    /// </summary>
    public class Entry
    {
        public long ID { get; set; }
        /// <summary>
        /// Path of file, web or local
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Name of Image
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Whether or not image is on the internet or local PC
        /// </summary>
        public bool IsLocal { get; set; }
        /// <summary>
        /// What was searched to get the image
        /// </summary>
        public string Searched { get; set; }
        /// <summary>
        /// How many bytes the image contains
        /// </summary>
        public ulong FileSize { get; }
        /// <summary>
        /// The two dimensions of the file (Only cares about first two entries)
        /// </summary>
        public long Height { get; set; }
        public long Width { get; set; }
        /// <summary>
        /// Tags that were applied to the image (May be user defined)
        /// </summary>
        public string[] Tags { get; set; }

        public Entry(string Path, string Name, bool IsLocal, ulong FileSize, string Searched, long Height, long Width)
        {
            this.Path = Path;
            this.Name = Name;
            this.IsLocal = IsLocal;
            this.Searched = Searched;
            this.Height = Height;
            this.Width = Width;
            this.FileSize = FileSize;
        }

        public string[] ToStringArr()
        {
            return new string[] {ID.ToString(), Name, IsLocal.ToString(), Searched, FileSize.ToString(), Height.ToString(), Width.ToString() };
        }

        public override string ToString()
        {
            return $"{ID}, {Name}, {FileSize} bytes, Keyword: {Searched}, H: {Height}, W: {Width}";
        }
    }
}
