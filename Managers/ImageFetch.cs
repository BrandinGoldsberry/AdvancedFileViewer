using AdvancedFileViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixabayAPI;
using System.Net;
using Windows.Storage;
using Windows.Web.Http;
using Windows.Networking.BackgroundTransfer;

namespace AdvancedFileViewer.Managers
{
    public static class ImageFetch
    {

        public async static void GetPixaBayImage(string Search, bool download)
        {
            string search = PixabayLoader.searchTermBuilder(Search);
            string JSON = PixabayLoader.GetJSON(search);
            List<PixImage> Images = PixabayLoader.ParseJSONIntoImageList(JSON);
            if(download)
            {
                StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(Windows.ApplicationModel.Package.Current.InstalledLocation.Path + "\\images\\PixaBayDownloads");
                int count = 0;
                foreach(PixImage image in Images)
                {
                    if(count == 0)
                        DatabaseManager.SaveNewEntry(new Entry(folder.Path+ "\\" + Search + ".jpg", Search + " (" + count.ToString() + ")" + ".jpg", true, (ulong)image.imageSize, Search, image.imageHeight, image.imageWidth));
                    else
                        DatabaseManager.SaveNewEntry(new Entry(folder.Path+ "\\" + Search + " (" + count.ToString() + ")" + ".jpg", Search + "(" + count.ToString() + ")" + ".jpg", true, (ulong)image.imageSize, Search, image.imageHeight, image.imageWidth));
                    Uri source = new Uri(image.LargeImageURL);

                    StorageFile destinationFile = await folder.CreateFileAsync(
                       Search + ".jpg", CreationCollisionOption.GenerateUniqueName);

                    BackgroundDownloader downloader = new BackgroundDownloader();
                    DownloadOperation downloaded = downloader.CreateDownload(source, destinationFile);
                    await downloaded.StartAsync();
                    count++;
                }
            }
            else
            {
                foreach (PixImage image in Images)
                {
                    DatabaseManager.SaveNewEntry(new Entry(image.LargeImageURL, Search, false, (ulong)image.imageSize, Search, image.imageHeight, image.imageWidth));
                }
            }
        }
    }
}
