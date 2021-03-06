﻿using CockBlock8._1.View;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CockBlock8._1.Main
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutPage : CB_Page
    {
        public AboutPage()
        {
            this.InitializeComponent();
            this.About_tx.TextWrapping = TextWrapping.Wrap;
            this.About_tx.Text = Settings.About().Result;
            this._Back_bn.FontSize = Settings._DefaultButtonFontSizePhone;
        }

        private void _Back_bn_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();
        }
        internal override Button[] GetButtons()
        { return new Button[] { }; }
        internal override TextBlock[] GetTextBlocks()
        { return new TextBlock[] { }; }
    }
}
