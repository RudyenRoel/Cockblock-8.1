using CockBlock8._1.View;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace CockBlock8._1.Main
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutPage : CB_Page
    {
        private int TitleFontSize = 72;
        private int AboutFontSize = 18;
        public AboutPage()
        {
            this.InitializeComponent();
            Settings.DefaultTextBlockProperties(this.Title_tx, this.About_tx);
            this.Title_tx.FontSize = TitleFontSize;
            this.About_tx.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            this.Title_tx.Text = "About";
            this.About_tx.Text = Settings.About();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
        internal override Button[] GetButtons()
        { return null; }
        internal override TextBlock[] GetTextBlocks()
        { return new TextBlock[] { this.About_tx, this.Title_tx }; }
    }
}
