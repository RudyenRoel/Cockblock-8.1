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
        public AboutPage()
        {
            this.InitializeComponent();
            this.About_tx.TextWrapping = TextWrapping.Wrap;
            this.About_tx.Text = GetAboutText();
        }
        private string GetAboutText()
        {
            string text = "";
            text += Line(Line("This project is called 'CockBlock8.1'"));
            text += Line("Makers of this project are");
            text += Line(" - 'Rudy Tjin-Con-Coen'");
            text += Line("\tand");
            text += Line(Line(" - 'Roel Suntjens'."));
            text += Line("This project is made to prove that we can build a Xaml project with the following specifications:");
            text += Line(" - ");
            text += Line(" - ");
            text += Line("We are greatfull to give you te oppartunity to play our game.");
            return text;
        }
        private string Line(string text)
        { return text + "\n"; }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
