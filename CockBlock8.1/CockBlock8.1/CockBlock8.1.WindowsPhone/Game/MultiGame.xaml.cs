using CockBlock8._1.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace CockBlock8._1.Game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MultiGame : CB_Page
    {
        private int _totalHealthBarWidth = 350;
        public MultiGame()
        {
            this.InitializeComponent();
            Init();
        }
        private void Init()
        {
            DefaultImageProparties(_ShieldCannon1, _ShieldCannon2, _ShieldCannon3);
            DefaultTextBlockProparties(Colors.White, 10, "100", _HealthShieldCannon1, _HealthShieldCannon2, _HealthShieldCannon3);
            DefaultHealthBarProparties(_totalHealthBar, _currentHealthBar);
        }
        private void DefaultHealthBarProparties(params Rectangle[] rects)
        {
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i].Width = _totalHealthBarWidth;
                rects[i].Height = 25;
                if (i % 2 == 0)
                    SetRectangleColor(Colors.White, rects[i]);
                else
                {
                    SetRectangleColor(Colors.Red, rects[i]);
                    rects[i].Width = rects[i].Width - 50;
                }
                rects[i].Stroke = new SolidColorBrush(Colors.Black);
                rects[i].HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
            }
        }

        private void SetRectangleColor(Color color, Rectangle rect)
        { rect.Fill = new SolidColorBrush(color); }
        private void DefaultImageProparties(params Image[] images)
        {
            foreach (Image img in images)
            {
                img.Width = 100;
                img.Height = 100;
                img.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom;
            }
        }
        private void DefaultTextBlockProparties(Color color, int fontSize, string text, params TextBlock[] textblocks)
        {
            foreach (TextBlock tb in textblocks)
            {
                tb.FontSize = fontSize;
                tb.Text = text;
                SetTextBlockColor(color, tb);
            }
        }
        private void SetTextBlockColor(Color color, TextBlock tb) { tb.Foreground = new SolidColorBrush(color); }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void Back_bn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), e);
        }
    }
}
