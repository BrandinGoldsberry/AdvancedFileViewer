using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace AdvancedFileViewer.Managers
{
    public static class ImageManipulation
    {
        public async static Task<StorageFile> RGBRecolor(string FileType, StorageFile OpenFile, StorageFolder SaveFolder, byte redVal, byte greenVal, byte blueVal)
        {
            ImageProperties properties = await OpenFile.Properties.GetImagePropertiesAsync();
            WriteableBitmap bitmap = new WriteableBitmap((int)properties.Width, (int)properties.Height);
            StorageFile NewFile = null;

            using (IRandomAccessStream fileStream = await OpenFile.OpenAsync(FileAccessMode.Read))
            {
                BitmapDecoder decoder = null;
                switch (FileType)
                {
                    case "png":
                        decoder = await BitmapDecoder.CreateAsync(BitmapDecoder.PngDecoderId, fileStream);
                        break;
                    case "jpeg":
                        decoder = await BitmapDecoder.CreateAsync(BitmapDecoder.JpegDecoderId, fileStream);
                        break;
                    case "jpg":
                        decoder = await BitmapDecoder.CreateAsync(BitmapDecoder.JpegDecoderId, fileStream);
                        break;
                }
                // Scale image to appropriate size
                BitmapTransform transform = new BitmapTransform()
                {
                    ScaledWidth = Convert.ToUInt32(bitmap.PixelWidth),
                    ScaledHeight = Convert.ToUInt32(bitmap.PixelHeight)
                };
                PixelDataProvider pixelData = await decoder.GetPixelDataAsync(
                    BitmapPixelFormat.Bgra8, // WriteableBitmap uses BGRA format
                    BitmapAlphaMode.Straight,
                    transform,
                    ExifOrientationMode.IgnoreExifOrientation, // This sample ignores Exif orientation
                    ColorManagementMode.DoNotColorManage
                );

                // An array containing the decoded image data, which could be modified before being displayed
                byte[] sourcePixels = pixelData.DetachPixelData();


                NewFile = await SaveFolder.CreateFileAsync("Altered_" + OpenFile.Name, CreationCollisionOption.GenerateUniqueName);

                // Open a stream to copy the image contents to the WriteableBitmap's pixel buffer
                using (Stream stream = bitmap.PixelBuffer.AsStream())
                {
                    for (int i = 0; i < sourcePixels.Length; i += 4)
                    {
                        //Debug.WriteLine(sourcePixels[i]);
                        Random r = new Random();
                        int NewBlueVal = (sourcePixels[i] + blueVal) / 2;
                        sourcePixels[i] = (byte)Math.Clamp(NewBlueVal, 0, 255);
                        int NewGreenVal = (sourcePixels[i + 1] + greenVal) / 2;
                        sourcePixels[i + 1] = (byte)Math.Clamp(NewGreenVal, 0, 255);
                        int NewRedVal = (sourcePixels[i + 2] + redVal) / 2;
                        sourcePixels[i + 2] = (byte)Math.Clamp(NewRedVal, 0, 255);
                    }
                    await stream.WriteAsync(sourcePixels, 0, sourcePixels.Length);
                }

                using (IRandomAccessStream newStream = await NewFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, newStream);
                    encoder.SetPixelData
                    (
                        BitmapPixelFormat.Bgra8,
                        BitmapAlphaMode.Ignore,
                        (uint)bitmap.PixelWidth,
                        (uint)bitmap.PixelHeight,
                        96.0,
                        96.0,
                        sourcePixels
                    );
                    await encoder.FlushAsync();
                }
            }
            return NewFile;
        }
    }
}
