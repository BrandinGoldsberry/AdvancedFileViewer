using AdvancedFileViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using Windows.System;
using Windows.Storage;

namespace AdvancedFileViewer.Managers
{
    public class DynamicTableConstructor
    {
        private static Entry[] allEntries;

        public static void OnRefresh()
        {
            DatabaseManager.ConnectToDatabase();
            allEntries = DatabaseManager.GetEntries();
        }

        public static Entry[] GetEntries()
        {
            return allEntries;
        }

        public static void SortEntries()
        {

        }

        public static FrameworkElement BuildRows()
        {
            int columnAmt = 7;
            double fontSize = 40;
            Grid Table = new Grid();
            Table.Name = "DataGrid";
            for (int i = 0; i < columnAmt; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(1, GridUnitType.Auto);
                Table.ColumnDefinitions.Add(column);
            }

            RowDefinition infoRow = new RowDefinition();
            infoRow.Height = new GridLength(1, GridUnitType.Star);
            Table.RowDefinitions.Add(infoRow);

            foreach (EntryColumn entryColumn in Enum.GetValues(typeof(EntryColumn)))
            {
                if (entryColumn != EntryColumn.Path)
                {
                    Rectangle holder = new Rectangle();
                    Viewbox vb = new Viewbox();
                    TextBlock cell = new TextBlock();

                    holder.Stroke = new SolidColorBrush(Color.FromArgb(255, 230, 230, 230));
                    holder.StrokeThickness = 0.5;
                    cell.Text = entryColumn.ToString();
                    cell.FontSize = fontSize;
                    cell.Margin = new Thickness(10);

                    Grid.SetRow(holder, 0);
                    Grid.SetColumn(holder, (int)entryColumn);
                    Grid.SetRow(cell, 0);
                    Grid.SetColumn(cell, (int)entryColumn);


                    Table.Children.Add(holder);
                    Table.Children.Add(cell);

                }
            }

            int count = 1;
            foreach (Entry r in allEntries)
            {
                RowDefinition contentRow = new RowDefinition();
                contentRow.Height = new GridLength(1, GridUnitType.Star);
                Table.RowDefinitions.Add(contentRow);

                string[] data = r.ToStringArr();

                for (int i = 0; i < columnAmt; i++)
                {
                    Rectangle holder = new Rectangle();
                    Viewbox vb = new Viewbox();
                    TextBlock cell = new TextBlock();


                    cell.DoubleTapped += async (sender, e) =>
                    {
                        StorageFile toOpen = await StorageFile.GetFileFromPathAsync(r.Path);
                        await Launcher.LaunchFileAsync(toOpen);
                    };

                    holder.Stroke = new SolidColorBrush(Color.FromArgb(255, 230, 230, 230));
                    holder.StrokeThickness = 0.5;
                    if(i == 4)
                    {
                        ulong fileSize = ulong.Parse(data[i]);
                        string ByteString = null;
                        fileSize = ConvertToBytes(fileSize, out ByteString);
                        cell.Text = fileSize.ToString() + " " + ByteString;
                    }
                    else
                    {
                        cell.Text = data[i];
                    }
                    cell.FontSize = fontSize;
                    cell.Margin = new Thickness(10);

                    Grid.SetRow(holder, count);
                    Grid.SetColumn(holder, i);
                    Grid.SetRow(cell, count);
                    Grid.SetColumn(cell, i);


                    Table.Children.Add(holder);
                    Table.Children.Add(cell);
                }

                count++;
            }

            Table.HorizontalAlignment = HorizontalAlignment.Center;
            return Table;
        }

        public static FrameworkElement BuildRows(Entry[] entries)
        {
            int columnAmt = 7;
            double fontSize = 40;
            Grid Table = new Grid();
            Table.Name = "DataGrid";
            for (int i = 0; i < columnAmt; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(1, GridUnitType.Auto);
                Table.ColumnDefinitions.Add(column);
            }

            RowDefinition infoRow = new RowDefinition();
            infoRow.Height = new GridLength(1, GridUnitType.Star);
            Table.RowDefinitions.Add(infoRow);

            foreach (EntryColumn entryColumn in Enum.GetValues(typeof(EntryColumn)))
            {
                if (entryColumn != EntryColumn.Path)
                {
                    Rectangle holder = new Rectangle();
                    Viewbox vb = new Viewbox();
                    TextBlock cell = new TextBlock();

                    holder.Stroke = new SolidColorBrush(Color.FromArgb(255, 230, 230, 230));
                    holder.StrokeThickness = 0.5;
                    cell.Text = entryColumn.ToString();
                    cell.FontSize = fontSize;
                    cell.Margin = new Thickness(10);

                    Grid.SetRow(holder, 0);
                    Grid.SetColumn(holder, (int)entryColumn);
                    Grid.SetRow(cell, 0);
                    Grid.SetColumn(cell, (int)entryColumn);


                    Table.Children.Add(holder);
                    Table.Children.Add(cell);

                }
            }

            int count = 1;
            foreach (Entry r in entries)
            {
                RowDefinition contentRow = new RowDefinition();
                contentRow.Height = new GridLength(1, GridUnitType.Star);
                Table.RowDefinitions.Add(contentRow);

                string[] data = r.ToStringArr();

                for (int i = 0; i < columnAmt; i++)
                {
                    Rectangle holder = new Rectangle();
                    Viewbox vb = new Viewbox();
                    TextBlock cell = new TextBlock();

                    holder.Stroke = new SolidColorBrush(Color.FromArgb(255, 230, 230, 230));
                    holder.StrokeThickness = 0.5;
                    if (i == 4)
                    {
                        ulong fileSize = ulong.Parse(data[i]);
                        string ByteString = null;
                        fileSize = ConvertToBytes(fileSize, out ByteString);
                        cell.Text = fileSize.ToString() + " " + ByteString;
                    }
                    else
                    {
                        cell.Text = data[i];
                    }
                    cell.FontSize = fontSize;
                    cell.Margin = new Thickness(10);

                    Grid.SetRow(holder, count);
                    Grid.SetColumn(holder, i);
                    Grid.SetRow(cell, count);
                    Grid.SetColumn(cell, i);


                    Table.Children.Add(holder);
                    Table.Children.Add(cell);
                }

                count++;
            }

            Table.HorizontalAlignment = HorizontalAlignment.Center;
            return Table;
        }

        private static ulong ConvertToBytes(ulong input, out string ByteString)
        {
            ulong ret = input;
            int count = 0;
            while(ret > 1024)
            {
                ret /= 1024;
                count++;
            }
            switch(count)
            {
                case 0:
                    ByteString = "B";
                    break;
                case 1:
                    ByteString = "Kb";
                    break;
                case 2:
                    ByteString = "Mb";
                    break;
                case 3:
                    ByteString = "Gb";
                    break;
                default:
                    ByteString = "Too Big";
                    break;
            }
            return ret;
        }
    }
}
