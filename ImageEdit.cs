using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using System.IO.Compression;

namespace AdvancedFileViewer
{
    class ImageEdit
    {
        public async void ReSize(uint width, uint length, List<string> loadExtenstionList, List<string> saveExtenstionList)
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };

            foreach (var item in loadExtenstionList)
            {
                fileOpenPicker.FileTypeFilter.Add(item);
            }
            fileOpenPicker.ViewMode = PickerViewMode.Thumbnail;

            var inputFile = await fileOpenPicker.PickSingleFileAsync();

            if (inputFile == null)
            {
                // The user cancelled the picking operation
                return;
            }

            SoftwareBitmap ImageEdit;

            using (IRandomAccessStream stream = await inputFile.OpenAsync(FileAccessMode.Read))
            {
                // Create the decoder from the stream
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);

                // Get the SoftwareBitmap representation of the file
                ImageEdit = await decoder.GetSoftwareBitmapAsync();
            }

            FileSavePicker fileSavePicker = new FileSavePicker();
            fileSavePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            fileSavePicker.FileTypeChoices.Add("All Files", saveExtenstionList);
            foreach (var item in saveExtenstionList)
            {
                fileSavePicker.FileTypeChoices.Add($"{item} file", new List<string>() { item });
            }
            fileSavePicker.SuggestedFileName = "image";

            var outputFile = await fileSavePicker.PickSaveFileAsync();

            if (outputFile == null)
            {
                // The user cancelled the picking operation
                return;
            }

            using (IRandomAccessStream stream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                // Create an encoder with the desired format
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);

                // Set the software bitmap
                encoder.SetSoftwareBitmap(ImageEdit);

                // Set additional encoding parameters, if needed
                encoder.BitmapTransform.ScaledWidth = width;
                encoder.BitmapTransform.ScaledHeight = length;
                encoder.IsThumbnailGenerated = true;

                try
                {
                    await encoder.FlushAsync();
                }
                catch (Exception err)
                {
                    const int WINCODEC_ERR_UNSUPPORTEDOPERATION = unchecked((int)0x88982F81);
                    switch (err.HResult)
                    {
                        case WINCODEC_ERR_UNSUPPORTEDOPERATION:
                            // If the encoder does not support writing a thumbnail, then try again
                            // but disable thumbnail generation.
                            encoder.IsThumbnailGenerated = false;
                            break;
                        default:
                            throw;
                    }
                }

                if (encoder.IsThumbnailGenerated == false)
                {
                    await encoder.FlushAsync();
                }
            }
        }

        public async void ZipContents(string zipName)
        {
            FolderPicker folderPicker = new FolderPicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            folderPicker.FileTypeFilter.Add("*");
            StorageFolder folder = await folderPicker.PickSingleFolderAsync();

            FolderPicker folderEnd = new FolderPicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            folderEnd.FileTypeFilter.Add("*");
            StorageFolder folder2 = await folderEnd.PickSingleFolderAsync();

            SaveFolder(folder.Path, folder2.Path + "\\" + zipName + ".zip");
        }

        private void SaveFolder(string startDir, string zipPath)
        {
            var t = Task.Run(() => ZipFile.CreateFromDirectory(startDir, zipPath));
            t.Wait();
        }

    }
}


