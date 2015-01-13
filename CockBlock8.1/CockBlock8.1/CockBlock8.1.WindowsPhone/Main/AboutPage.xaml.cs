using CockBlock8._1.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.UI.Popups;
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
        private DispatcherTimer timer;
        private List<string> _easterEggs = new List<string>() { "Happy", "Moody", "Sleepy", "Hungry", "Angry", "Closing" }; // Magic coockie
        private int _EasterEgg_rect_Counter = 0;
        private int TitleFontSize = 72;
        private int AboutFontSize = 18;
        public AboutPage()
        {
            this.InitializeComponent();
            Settings.DefaultTextBlockProperties(this.Title_tx, this.About_tx, this._EasterEgg_tx);
            this.Title_tx.FontSize = TitleFontSize;
            this.About_tx.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
            this.Title_tx.Text = "About";
            SetEasterEgg();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string aboutText = e.Parameter as string;
            this.About_tx.Text = aboutText;
        }
        internal override Button[] GetButtons()
        { return null; }
        internal override TextBlock[] GetTextBlocks()
        { return new TextBlock[] { this.About_tx, this.Title_tx }; }
        private void SetEasterEgg()
        {
            Random random = new Random();
            IEnumerable<int> lowNums = null;
            while (lowNums == null || lowNums.Count() == 0)
            {

                List<int> indexes = new List<int>();
                for (int i = 0; i < 100; i++)
                    indexes.Add(random.Next(0, 400));

                // LINQ
                lowNums =
                   from n in indexes
                   where n < _easterEggs.Count
                   select n;
            }

            string status = "I am " + _easterEggs[lowNums.ToArray()[0]];
            this._EasterEgg_tx.Text = status;
        }

        private async void _EasterEgg_rect_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (_EasterEgg_rect_Counter > 5)
            {
                this._EasterEgg_rect.Visibility = Visibility.Collapsed;
                await ShowCloseMessage();
            }
            else
            { _EasterEgg_rect_Counter++; }
        }
        private async Task ShowCloseMessage()
        {
            this.Close_Feedback_Flyout.Text = "This Application Will Close!";
            this.Close_Flyout.Visibility = Windows.UI.Xaml.Visibility.Visible;
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        { Exit(); }

        private void Close_bn_Flyout_Click(object sender, RoutedEventArgs e)
        { Exit(); }
        private void Exit()
        { Application.Current.Exit(); }
    }
}
