using CockBlock8._1.Common;
using CockBlock8._1.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace CockBlock8._1.Main.Instructions
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InstructionPage : CB_Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private List<string[]> _HelpPageInformation = null;
        private string[] imagePaths = null;

        public InstructionPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }
        private void Init()
        {
            Settings.DefaultTextBlockProperties(this._Title, this._SubTitle, this._Information_tx);
            Settings.DefaultButtonProperties(this._Help_bn);
            this._Help_bn.Content = "Help";
            this._Title.FontSize = Settings._DefaultHeaderFontSize;
            this._SubTitle.FontSize = Settings._DefaultSubHeaderFontSize;
        }
        private void SetButtons()
        {
            _Images_Panel.Children.Clear();
            foreach (string path in imagePaths)
            {
                Button button = new Button();
                button.Content = path.Substring(0, path.Length - 4);
                button.Click += button_Click;
                _Images_Panel.Children.Add(button);
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();
            Debug.WriteLine("Own name: " + content);
            int index = -1;

            index = FindButtonIndex(content);

            if (index != -1)
                LoadImage(index);
            else
                LoadImage(null);

            ShowFlyout();
        }
        private int FindButtonIndex(string content)
        {
            for (int i = 0; i < _Images_Panel.Children.Count; i++)
            {
                string buttonFound = (_Images_Panel.Children[i] as Button).Content.ToString();
                Debug.WriteLine("Equalizer: " + buttonFound + ", " + content);
                if (buttonFound.Equals(content))
                { return i; }
            }
            return -1;
        }
        private void LoadImage(string name)
        {
            if (name == null)
                name = "Cock.png";
            Debug.WriteLine("Loading Image: " + "Res/" + name);
            BitmapImage img = new BitmapImage(new Uri(@"ms-appx:///Res/" + name));
            _ButtonPressed_Image_img.Source = img;
        }
        private void LoadImage(int index)
        { LoadImage(imagePaths[index]); }


        #region NavigationHelper registration
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Tuple<Tuple<string[], string[]>, List<string[]>> information = (e.Parameter as Tuple<Tuple<string[], string[]>, List<string[]>>);
            Tuple<string[], string[]> instructions = information.Item1;
            string[] instruction = instructions.Item1;
            Init();
            this._SubTitle.Text = instruction[0];
            this._Title.Text = instruction[1];
            this._Information_tx.Text = instruction[2];
            this.imagePaths = instructions.Item2;
            this._HelpPageInformation = information.Item2;
            SetButtons();
            Debug.WriteLine("Instructions: " + instruction[2]);
        }

        #endregion
        private void Help_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(HelpPage), _HelpPageInformation); }

        private void _Close_Flyout_bn_Click(object sender, RoutedEventArgs e)
        { HideFlyout(); }
        private void HideFlyout() { this._Image_Flyout.Visibility = Windows.UI.Xaml.Visibility.Collapsed; }
        private void ShowFlyout() { this._Image_Flyout.Visibility = Windows.UI.Xaml.Visibility.Visible; }
        internal override Button[] GetButtons()
        { return new Button[] { this._Help_bn, this._Close_Flyout_bn }; }
        internal override TextBlock[] GetTextBlocks()
        { return new TextBlock[] { this._Title, this._SubTitle, this._Information_tx }; }
    }
}
