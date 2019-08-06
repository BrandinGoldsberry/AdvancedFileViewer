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
        public int FileSize { get; }
        /// <summary>
        /// The two dimensions of the file (Only cares about first two entries)
        /// </summary>
        public int[] ImageSize { get; }
        /// <summary>
        /// Tags that were applied to the image (May be user defined)
        /// </summary>
        public string[] Tags { get; set; }

        public Entry(string Path, string Name, bool IsLocal, int FileSize, int[] ImageSize, string Searched)
        {
            this.Path = Path;
            this.Name = Name;
            this.IsLocal = IsLocal;
            this.Searched = Searched;
            this.FileSize = FileSize;
            this.ImageSize = ImageSize;
        }
    }
}
