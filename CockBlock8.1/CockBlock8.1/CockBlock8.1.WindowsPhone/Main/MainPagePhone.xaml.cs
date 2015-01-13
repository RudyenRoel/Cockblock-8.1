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
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Phone.UI.Input;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
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
    public sealed partial class MainPagePhone : CB_Page
    {
        public static bool firstLocationDone = false;
        private static int scoreBonus = 0;
        private string _ApplicationName = "CockBlock 8.1";
        private BitmapImage _SettingsImage = new BitmapImage();
        private BitmapImage _MapImage = new BitmapImage();
        private int _TitleFontSize = 48;
        private int _AmountOfRectangles = 7 * 28;
        public static string _currentCountry = null;
        public MainPagePhone()
        {
            this.InitializeComponent();
            Init();
            SetMapPageFeedback(""); 
        }
        private void Init()
        {
            ShowMessage("wees gegroet");
            this.NavigationCacheMode = NavigationCacheMode.Required;
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            Settings.DefaultTextBlockProperties(Settings._DefaultHeaderFontSize, this.Title_tx);
            this.Title_tx.Text = _ApplicationName;
            _SettingsImage.UriSource = new Uri("ms-appx:Res/" + Settings._SettingsImage, UriKind.RelativeOrAbsolute);
            _MapImage.UriSource = new Uri("ms-appx:Res/" + Settings._MapImage, UriKind.RelativeOrAbsolute);
            this.Settings_img.Margin = new Thickness(200, 0, 0, 0);
            this.Settings_img.Source = _SettingsImage;
            this.Settings_img.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            this.Settings_img.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            this.Location_img.Margin = new Thickness(0, 0, 200, 0);
            this.Location_img.Source = _MapImage;
            this.Location_img.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            this.Location_img.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            Settings.DefaultButtonProperties(About_bn, SingleGame_bn, MultiGame_bn, Exit_bn);
            this.About_bn.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            this.SingleGame_bn.Content = "Single Game";
            this.MultiGame_bn.Content = "Multi Game";
            this.About_bn.Content = "About";
            this.Exit_bn.Content = "Exit";
            CheckCountry();
            Settings.Countries.All();
        }

        private async void CheckCountry()
        {
            await _vm.BackgroundCheck();
            firstLocationDone = true;
            _currentCountry = _vm.GetCountry();
        }
        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
            e.Handled = true;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.PortraitFlipped | DisplayOrientations.Portrait;
            base.OnNavigatedTo(e);
            Frame.BackStack.Clear();
        }

        private void Single_Game_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(SingleDeviceGameMenu), e); }
        private void Multi_Game_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(MultiDeviceGameMenu), e); }
        private async void About_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(AboutPage), await Settings.About()); }
        private void SetMapPageFeedback(string text)
        { this._MapPage_Feedback_tx.Text = text; }
        private void Exit_bn_Click(object sender, RoutedEventArgs e)
        { Application.Current.Exit(); }
        private void Settings_img_PointerPressed(object sender, PointerRoutedEventArgs e)
        { this.Frame.Navigate(typeof(SettingsPage), e); }
        private void Location_img_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (firstLocationDone)
            {
                SetMapPageFeedback("");
                this.Frame.Navigate(typeof(MapPage), e);
            }
            else
            { SetMapPageFeedback("Wait a moment please, searching current location"); }
        }
        public static void SetScoreBonus(int bonus)
        { if (firstLocationDone) { scoreBonus = bonus; } }
        public static int GetScoreBonus()
        { return scoreBonus; }
        internal override Button[] GetButtons()
        { return new Button[] { this.SingleGame_bn, this.MultiGame_bn, this.About_bn, this.Exit_bn }; }
        internal override TextBlock[] GetTextBlocks()
        { return new TextBlock[] { this.Title_tx }; }

        public async static Task ShowMessage(string message)
        {
            MessageDialog dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }

        delegate void ChangeOpponentNameDel(string name);
    }
}
