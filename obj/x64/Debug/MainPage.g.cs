﻿#pragma checksum "C:\New folder\AdvancedFileViewer\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "ED564F3CA66DCF4944F2B5CE23A0866C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdvancedFileViewer
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // MainPage.xaml line 12
                {
                    this.TopLevel = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3: // MainPage.xaml line 13
                {
                    this.rootPivot = (global::Windows.UI.Xaml.Controls.Pivot)(target);
                }
                break;
            case 4: // MainPage.xaml line 97
                {
                    this.Table = (global::Windows.UI.Xaml.Controls.ScrollViewer)(target);
                }
                break;
            case 5: // MainPage.xaml line 90
                {
                    this.ImageSelect = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.ImageSelect).SelectionChanged += this.ImageSelect_SelectionChanged;
                }
                break;
            case 6: // MainPage.xaml line 91
                {
                    this.PreviewImage = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 7: // MainPage.xaml line 92
                {
                    this.FileLocationOpen = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.FileLocationOpen).Click += this.FileLocationOpen_Click;
                }
                break;
            case 8: // MainPage.xaml line 79
                {
                    this.ImageToDelete = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 9: // MainPage.xaml line 81
                {
                    this.DeleteID = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 10: // MainPage.xaml line 84
                {
                    this.ImageDelete = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.ImageDelete).Click += this.ImageDelete_Click;
                }
                break;
            case 11: // MainPage.xaml line 60
                {
                    this.EditType = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.EditType).SelectionChanged += this.EditType_SelectionChanged;
                }
                break;
            case 12: // MainPage.xaml line 64
                {
                    this.EditID = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 13: // MainPage.xaml line 67
                {
                    this.EditInputs = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 14: // MainPage.xaml line 70
                {
                    this.EditImageSave = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.EditImageSave).Click += this.OnEditImage;
                }
                break;
            case 15: // MainPage.xaml line 71
                {
                    this.Sam = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 16: // MainPage.xaml line 50
                {
                    this.NewImageName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 17: // MainPage.xaml line 52
                {
                    this.NewImagePath = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 18: // MainPage.xaml line 53
                {
                    this.NewImagePathPick = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.NewImagePathPick).Click += this.NewImagePathPick_Click;
                }
                break;
            case 19: // MainPage.xaml line 54
                {
                    this.NewImageSave = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.NewImageSave).Click += this.NewImageSave_Click;
                }
                break;
            case 20: // MainPage.xaml line 41
                {
                    this.SafeBooruSearchName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 21: // MainPage.xaml line 43
                {
                    this.SafeBooruDownloadSearched = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                }
                break;
            case 22: // MainPage.xaml line 44
                {
                    this.SafeBooruFetch = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.SafeBooruFetch).Click += this.SafeBooruFetch_Click;
                }
                break;
            case 23: // MainPage.xaml line 32
                {
                    this.PixImageSearchName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 24: // MainPage.xaml line 34
                {
                    this.PixDownloadSearched = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                }
                break;
            case 25: // MainPage.xaml line 35
                {
                    this.PixImageFetch = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.PixImageFetch).Click += this.OnFetch;
                }
                break;
            case 26: // MainPage.xaml line 16
                {
                    this.NewQueryDrop = (global::Microsoft.Toolkit.Uwp.UI.Controls.Expander)(target);
                }
                break;
            case 27: // MainPage.xaml line 25
                {
                    this.LoadQuery = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.LoadQuery).SelectionChanged += this.LoadQuery_SelectionChanged;
                }
                break;
            case 28: // MainPage.xaml line 26
                {
                    this.RefreshDatabase = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.RefreshDatabase).Click += this.RefreshDatabase_Click;
                }
                break;
            case 29: // MainPage.xaml line 19
                {
                    this.qSelect = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 30: // MainPage.xaml line 20
                {
                    this.qText = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 31: // MainPage.xaml line 21
                {
                    this.Query = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Query).Click += this.OnSaveQuery;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

