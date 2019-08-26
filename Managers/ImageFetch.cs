using AdvancedFileViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixabayAPI;
using SafebooruAPI;
using System.Net;
using Windows.Storage;
using Windows.Web.Http;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage.FileProperties;

namespace AdvancedFileViewer.Managers
{
    public static class ImageFetch
    {

        public async static Task GetPixaBayImage(string Search, bool download)
        {
            string search = PixabayLoader.searchTermBuilder(Search);
            string JSON = PixabayLoader.GetJSON(search);
            List<PixImage> Images = PixabayLoader.ParseJSONIntoImageList(JSON);
            if(download)
            {
                StorageFolder basefolder = await StorageFolder.GetFolderFromPathAsync(Windows.ApplicationModel.Package.Current.InstalledLocation.Path);
                StorageFolder folder = await basefolder.CreateFolderAsync("images\\PixaBayDownloads", CreationCollisionOption.OpenIfExists);
                int count = 0;
                foreach(PixImage image in Images)
                {
                    if(count == 0)
                        DatabaseManager.SaveNewEntry(new Entry(folder.Path+ "\\" + Search + ".jpg", Search + "(" + count.ToString() + ")", true, (ulong)image.imageSize, Search, image.imageHeight, image.imageWidth));
                    else
                        DatabaseManager.SaveNewEntry(new Entry(folder.Path+ "\\" + Search + " (" + count.ToString() + ")" + ".jpg", Search + "(" + count.ToString() + ")", true, (ulong)image.imageSize, Search, image.imageHeight, image.imageWidth));
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

        public async static Task GetSafeBooruImage(string Search, bool Download)
        {
            string query = SafeBooruLoader.QueryBuilder(Search);
            string xml = SafeBooruLoader.GetXML(query);
            List<SBImage> Images = SafeBooruLoader.ParseXMLAndCreateImageList(xml);
            if (Download)
            {
                StorageFolder basefolder = await StorageFolder.GetFolderFromPathAsync(Windows.ApplicationModel.Package.Current.InstalledLocation.Path);
                StorageFolder folder = await basefolder.CreateFolderAsync("images\\SafeBooruDownloads", CreationCollisionOption.OpenIfExists);
                int count = 0;
                foreach (SBImage image in Images)
                {
                    Uri source = new Uri(image.File_Url);
                    string fileType = image.File_Url.Substring(image.File_Url.Length - 3);
                    fileType = fileType == "peg" ? "Jpeg" : fileType;

                    StorageFile destinationFile = await folder.CreateFileAsync(
                       Search + "." + fileType, CreationCollisionOption.GenerateUniqueName);

                    BackgroundDownloader downloader = new BackgroundDownloader();
                    DownloadOperation downloaded = downloader.CreateDownload(source, destinationFile);
                    await downloaded.StartAsync();

                    BasicProperties Properties = await destinationFile.GetBasicPropertiesAsync();
                    
                    if (count == 0)
                        DatabaseManager.SaveNewEntry(new Entry(destinationFile.Path, Search + "(" + count.ToString() + ")", true, (ulong)Properties.Size, Search, image.Height, image.Width));
                    else
                        DatabaseManager.SaveNewEntry(new Entry(destinationFile.Path, Search + "(" + count.ToString() + ")", true, (ulong)Properties.Size, Search, image.Height, image.Width));

                    count++;
                }
            }
            else
            {
                foreach (SBImage image in Images)
                {
                    DatabaseManager.SaveNewEntry(new Entry(image.File_Url, Search, false, 0, Search, image.Height, image.Width));
                }
            }
        }
    }
}
