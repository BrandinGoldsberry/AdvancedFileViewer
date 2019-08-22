﻿using System;
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

        public void OnFetch(object sender, RoutedEventArgs e)
        {
            ImageFetch.GetPixaBayImage(PixImageSearchName.Text, (bool)PixDownloadSearched.IsChecked);
        }

        public void OnSaveQuery(object sender, RoutedEventArgs e)
        {
            QueryManager.SaveQuery((EntryColumn)qSelect.SelectedItem, qText.Text);
            Query Q = new Query{ Column = (EntryColumn)qSelect.SelectedItem, Value = qText.Text };
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
                }
                else
                {
                    Query q = (Query)LoadQuery.SelectedItem;
                    Entry[] entries = QueryManager.LoadQuery(q.Column, q.Value);
                    FrameworkElement toAdd = DynamicTableConstructor.BuildRows(entries);
                    Table.Content = toAdd;
                }
            }

            DeleteID.Items.Clear();
            EditID.Items.Clear();

            foreach (Entry en in DatabaseManager.GetEntries())
            {
                //EditID.Items.Add(e);
                DeleteID.Items.Add(en);
                EditID.Items.Add(en);
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

            foreach (Entry e in DatabaseManager.GetEntries())
            {
                //EditID.Items.Add(e);
                DeleteID.Items.Add(e);
                EditID.Items.Add(e);
            }
        }

        public async void OnEditImage(object sender, RoutedEventArgs e)
        {
            Entry selected = (Entry)EditID.SelectedItem;
            FolderPicker folderPicker = new FolderPicker();
            folderPicker.FileTypeFilter.Add("*");

            StorageFile OpenFile = await StorageFile.GetFileFromPathAsync(selected.Path);
            StorageFolder SaveFolder = await folderPicker.PickSingleFolderAsync();

            StorageFile newFile = await ImageManipulation.RGBRecolor
            (
                OpenFile,
                SaveFolder,
                byte.Parse(((TextBox)FindName("RedEditValue")).Text),
                byte.Parse(((TextBox)FindName("GreenEditValue")).Text),
                byte.Parse(((TextBox)FindName("BlueEditValue")).Text)
            );
            //var props = await newFile.GetBasicPropertiesAsync();
            //var imgProps = await newFile.Properties.GetImagePropertiesAsync();

            //DatabaseManager.SaveNewEntry(new Entry(OpenFile.Path, selected.Name, true, props.Size, selected.Searched, imgProps.Height, imgProps.Width));
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

    }
}
