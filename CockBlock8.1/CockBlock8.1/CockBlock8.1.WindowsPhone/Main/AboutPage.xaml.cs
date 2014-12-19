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
    public sealed partial class AboutPage : Page
    {
        private int TitleFontSize = 72;
        private int AboutFontSize = 18;
        public AboutPage()
        {
            this.InitializeComponent();
            DefaultTextBlockProparties(this.Title_tx, this.About_tx);
            this.Title_tx.FontSize = TitleFontSize;
            this.Title_tx.Text = "About";
            this.About_tx.Text = Introduction.About();
        }
        private void DefaultTextBlockProparties(params TextBlock[] textblocks)
        {
            foreach (TextBlock tb in textblocks)
            {
                tb.TextWrapping = TextWrapping.Wrap;
                tb.FontSize = AboutFontSize;
                tb.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                tb.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
    }
}
