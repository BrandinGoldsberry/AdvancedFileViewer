using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AdvancedFileViewer.Managers;
using PixabayAPI;
using Windows.UI.ViewManagement;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using System.Threading.Tasks;
using AdvancedFileViewer.Models;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.Drawing;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Popups;
using Windows.System;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AdvancedFileViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private Entry NewImageEntry;

        public MainPage()
        {
            this.InitializeComponent();
            //Random r = new Random();
            //for(int i = 0; i < 100; i++)
            //{
            //    DatabaseManager.SaveNewEntry(new Entry("T", "K", false, r.Next(1000), "HHH", r.Next(1000), r.Next(1000)));
            //}
            OnProgramOpen();
        }

        public static void OnSort()
        {

        }

        public async void OnFetch(object sender, RoutedEventArgs e)
        {
            MessageDialog warning = new MessageDialog("Do not close out of the program while images download in background", "Important");
            warning.Commands.Add(new UICommand(
                "Continue"
                ));
            await warning.ShowAsync();
            await ImageFetch.GetPixaBayImage(PixImageSearchName.Text, (bool)PixDownloadSearched.IsChecked);
            DynamicTableConstructor.OnRefresh();
            OnLoadQuery();
        }

        public void OnSaveQuery(object sender, RoutedEventArgs e)
        {
            QueryManager.SaveQuery((EntryColumn)qSelect.SelectedItem, qText.Text);
            Query Q = new Query { Column = (EntryColumn)qSelect.SelectedItem, Value = qText.Text };
            LoadQuery.Items.Add(Q);
        }

        public void OnNewFile()
        {

        }

        public void OnLoadQuery()
        {
            var selection = LoadQuery.SelectedValue;
            if (selection != null)
            {
                if (selection.Equals("All"))
                {
                    FrameworkElement toAdd = DynamicTableConstructor.BuildRows();
                    Table.Content = toAdd;
                    Table.UpdateLayout();

                    DeleteID.Items.Clear();
                    EditID.Items.Clear();
                    ImageSelect.Items.Clear();

                    foreach (Entry en in DatabaseManager.GetEntries())
                    {
                        //EditID.Items.Add(e);
                        if (en.IsLocal)
                        {
                            DeleteID.Items.Add(en);
                            EditID.Items.Add(en);
                        }
                        if (en.Path.Substring(en.Path.Length - 3) != "gif")
                        {
                            ImageSelect.Items.Add(en);
                        }
                    }
                }
                else
                {
                    Query q = (Query)LoadQuery.SelectedItem;
                    Entry[] entries = QueryManager.LoadQuery(q.Column, q.Value);
                    FrameworkElement toAdd = DynamicTableConstructor.BuildRows(entries);
                    Table.Content = toAdd;

                    DeleteID.Items.Clear();
                    EditID.Items.Clear();
                    ImageSelect.Items.Clear();

                    foreach (Entry en in entries)
                    {
                        //EditID.Items.Add(e);
                        if (en.IsLocal)
                        {
                            DeleteID.Items.Add(en);
                            EditID.Items.Add(en);
                        }
                        ImageSelect.Items.Add(en);
                    }
                }
            }


        }

        public static void OnProgramExit()
        {

        }

        public void OnProgramOpen()
        {
            LoadQuery.Items.Add("All");
            foreach (Query s in QueryManager.ListQueries())
            {
                LoadQuery.Items.Add(s);
            }
            DynamicTableConstructor.OnRefresh();
            Table.Content = DynamicTableConstructor.BuildRows();
            LoadQuery.SelectedIndex = 0;
            qSelect.Items.Add(EntryColumn.FileSize);
            qSelect.Items.Add(EntryColumn.Height);
            qSelect.Items.Add(EntryColumn.IsLocal);
            qSelect.Items.Add(EntryColumn.Name);
            qSelect.Items.Add(EntryColumn.Path);
            qSelect.Items.Add(EntryColumn.Searched);
            qSelect.Items.Add(EntryColumn.Width);
            qSelect.Width = 300;
            qSelect.Height = 50;
            qSelect.HorizontalAlignment = HorizontalAlignment.Left;
            qSelect.FontSize = 30;

            //EditType.Items.Add("Color Tint");

            //foreach (Entry e in DatabaseManager.GetEntries())
            //{
            //    //EditID.Items.Add(e);
            //    if (e.IsLocal)
            //    {
            //        DeleteID.Items.Add(e);
            //        EditID.Items.Add(e);
            //    }
            //    ImageSelect.Items.Add(e);
            //}
        }

        public async void OnEditImage(object sender, RoutedEventArgs e)
        {
            Entry selected = (Entry)EditID.SelectedItem;
            FolderPicker folderPicker = new FolderPicker();
            folderPicker.FileTypeFilter.Add(".png");
            folderPicker.FileTypeFilter.Add(".jpg");
            folderPicker.FileTypeFilter.Add(".jpeg");

            StorageFile OpenFile = await StorageFile.GetFileFromPathAsync(selected.Path);
            StorageFolder SaveFolder = await folderPicker.PickSingleFolderAsync();

            string fileType = selected.Path.Split(".")[1];

            StorageFile newFile = await ImageManipulation.RGBRecolor
            (
                fileType,
                OpenFile,
                SaveFolder,
                byte.Parse(((TextBox)FindName("RedEditValue")).Text),
                byte.Parse(((TextBox)FindName("GreenEditValue")).Text),
                byte.Parse(((TextBox)FindName("BlueEditValue")).Text)
            );
            var props = await newFile.GetBasicPropertiesAsync();
            var imgProps = await newFile.Properties.GetImagePropertiesAsync();

            DatabaseManager.SaveNewEntry(new Entry(newFile.Path, "Color Tinted " + selected.Name, true, props.Size, selected.Searched, imgProps.Height, imgProps.Width));
        }

        private void LoadQuery_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnLoadQuery();
        }

        private void NewImageSave_Click(object sender, RoutedEventArgs e)
        {
            DatabaseManager.SaveNewEntry(NewImageEntry);
            DynamicTableConstructor.OnRefresh();
            OnLoadQuery();
        }

        private async void NewImagePathPick_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".gif");

            StorageFile file = await picker.PickSingleFileAsync();
            var baseProp = await file.GetBasicPropertiesAsync();
            var imageProp = await file.Properties.GetImagePropertiesAsync();

            NewImagePath.Text = file.Path;

            NewImageEntry = new Entry(NewImagePath.Text, NewImageName.Text, true, baseProp.Size, NewImageName.Text, imageProp.Height, imageProp.Width);
        }

        private void EditType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditInputs.Children.Clear();
            if (EditType.SelectedIndex == 0)
            {
                EditInputs.Children.Add(new TextBlock()
                {
                    Text = "Red Edit Value",
                });

                EditInputs.Children.Add(new TextBox()
                {
                    Name = "RedEditValue"
                });

                EditInputs.Children.Add(new TextBlock()
                {
                    Text = "Green Edit Value",
                });

                EditInputs.Children.Add(new TextBox()
                {
                    Name = "GreenEditValue"
                });

                EditInputs.Children.Add(new TextBlock()
                {
                    Text = "Blue Edit Value",
                });

                EditInputs.Children.Add(new TextBox()
                {
                    Name = "BlueEditValue"
                });

                EditInputs.UpdateLayout();
            }
        }

        private async void ImageDelete_Click(object sender, RoutedEventArgs e)
        {
            Entry toDelete = DeleteID.SelectedValue as Entry;
            StorageFile deleteFile = await StorageFile.GetFileFromPathAsync(toDelete.Path);
            await deleteFile.DeleteAsync();
            DatabaseManager.DeleteEntry(toDelete);
            DynamicTableConstructor.OnRefresh();
            OnLoadQuery();
        }

        private void RefreshDatabase_Click(object sender, RoutedEventArgs e)
        {
            DynamicTableConstructor.OnRefresh();
            OnLoadQuery();
        }

        private async void ImageSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Entry toOpen = ImageSelect.SelectedValue as Entry;
            if (toOpen.Path.Substring(toOpen.Path.Length - 3) != "gif")
            {
                if (toOpen.IsLocal)
                {
                    StorageFile ImageFile = null;
                    BitmapImage bitmap = null;
                    ImageFile = await StorageFile.GetFileFromPathAsync(toOpen.Path);
                    bitmap = new BitmapImage();
                    var source = await ImageFile.OpenReadAsync();
                    await bitmap.SetSourceAsync(source);
                    PreviewImage.Source = bitmap;
                }
                else
                {
                    PreviewImage.Source = new BitmapImage(new Uri(toOpen.Path, UriKind.Absolute));
                }

                PreviewImage.Height = 600;
                PreviewImage.Width = 600;
            }
            else
            {
                MessageDialog warning = new MessageDialog("Displaying Gifs Not Supported at this time", "Warning");
                warning.Commands.Add(new UICommand(
                    "Ok"
                    ));
            }
        }

        private async void FileLocationOpen_Click(object sender, RoutedEventArgs e)
        {
            Entry toOpen = ImageSelect.SelectedValue as Entry;
            if (toOpen != null)
            {
                int idLast = toOpen.Path.LastIndexOf("\\");
                string FolderPath = null;
                if(idLast != -1)
                {
                    FolderPath = toOpen.Path.Substring(0, idLast);
                }

                StorageFolder openLoc = await StorageFolder.GetFolderFromPathAsync(FolderPath);
                await Launcher.LaunchFolderAsync(openLoc);
            }
        }

        private async void SafeBooruFetch_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog warning = new MessageDialog("Do not close out of the program while images download in background", "Important");
            warning.Commands.Add(new UICommand(
                "Continue"
                ));
            await warning.ShowAsync();
            await ImageFetch.GetSafeBooruImage(SafeBooruSearchName.Text, (bool)SafeBooruDownloadSearched.IsChecked);
            DynamicTableConstructor.OnRefresh();
            OnLoadQuery();
        }
    }
}
