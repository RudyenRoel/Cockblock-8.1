using CockBlock8._1.Main;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CockBlock8._1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : CB_Page
    {
        private int AmountOfRectangles = 7 * 91;
        public static string _currentCountry;
        public MainPage()
        {
            this.InitializeComponent();
            Init();
        }
        private void Init()
        {
            InitRectangles();
        }
        private void InitRectangles()
        {
            Random random = new Random();
            for (int i = this.RectangleItems_IC.Items.Count; i < AmountOfRectangles; i++)
            {
                AddRectangle(200, (byte)random.Next(100, 255), (byte)random.Next(100, 255), (byte)random.Next(100, 255));
            }
        }
        private void AddRectangle(byte a, byte r, byte g, byte b)
        {
            Shape rect = new Windows.UI.Xaml.Shapes.Rectangle();
            rect.Height = 10;
            rect.Width = 10;
            rect.Margin = new Thickness(2, 2, 2, 2);
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromArgb(a, r, g, b);
            rect.Fill = brush;
            RectangleItems_IC.Items.Add(rect);
        }
        private void SingleGame_bn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SingleGameMenu), e);
        }
        private void MultiGame_bn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MultiGameMenu), e);
        }
        private void About_bn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AboutPage), e);
        }
        private void Exit_bn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void RectangleItems_bn_Click(object sender, RoutedEventArgs e)
        {
            if (this.RectangleItems_IC.Items.Count > 0)
            {
                this.RectangleItems_IC.Items.RemoveAt(0);
                InitRectangles();
            }
        }

    }
}
