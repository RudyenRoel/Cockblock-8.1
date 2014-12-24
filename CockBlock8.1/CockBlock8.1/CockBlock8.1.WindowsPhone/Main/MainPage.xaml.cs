using CockBlock8._1.Main;
using CockBlock8._1.Main.Instructions;
using CockBlock8._1.Model;
using CockBlock8._1.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CockBlock8._1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MainPage : CB_Page
    {
        private string _ApplicationName = "CockBlock 8.1";
        private BitmapImage _SettingsImage = new BitmapImage();
        private int _TitleFontSize = 48;
        private int _AmountOfRectangles = 7 * 28;
        public MainPage()
        {
            this.InitializeComponent();
            Init();
        }
        private void Init()
        {
            this.NavigationCacheMode = NavigationCacheMode.Required;
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            Settings.DefaultTextBlockProperties(Settings._DefaultHeaderFontSize, this.Title_tx);
            this.Title_tx.Text = _ApplicationName;
            _SettingsImage.UriSource = new Uri("ms-appx:Res/" + Settings._SettingsImage, UriKind.RelativeOrAbsolute);
            this.Settings_img.Margin = new Thickness(200, 0, 0, 0);
            this.Settings_img.Source = _SettingsImage;
            this.Settings_img.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            this.Settings_img.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            Settings.DefaultButtonProperties(About_bn, SingleGame_bn, MultiGame_bn, Exit_bn);
            this.About_bn.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            this.SingleGame_bn.Content = "Single Game";
            this.MultiGame_bn.Content = "Multi Game";
            this.About_bn.Content = "About";
            this.Exit_bn.Content = "Exit";
            InitRectangles();
        }
        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
            e.Handled = true;
        }
        private void InitRectangles()
        {
            Random random = new Random();
            for (int i = this.RectangleItems_IC.Items.Count; i < _AmountOfRectangles; i++)
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Frame.BackStack.Clear();
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void Single_Game_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(SingleGameMenu), e); }
        private void Multi_Game_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(MultiGameMenu), e); }
        private void About_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(AboutPage), e); }
        private void Exit_bn_Click(object sender, RoutedEventArgs e)
        { Application.Current.Exit(); }
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (RectangleItems_IC.Items.Count > 0)
            {
                RectangleItems_IC.Items.RemoveAt(0);
                InitRectangles();
            }
        }
        private void Settings_img_PointerPressed(object sender, PointerRoutedEventArgs e)
        { this.Frame.Navigate(typeof(SettingsPage), e); }
        internal override Button[] GetButtons()
        { return new Button[] { this.SingleGame_bn, this.MultiGame_bn, this.About_bn, this.Exit_bn }; }
        internal override TextBlock[] GetTextBlocks()
        { return new TextBlock[] { this.Title_tx }; }
    }
}
