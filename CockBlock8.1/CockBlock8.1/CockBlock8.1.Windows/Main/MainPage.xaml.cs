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
        internal override Button[] GetButtons()
        { return new Button[] { }; }
        internal override TextBlock[] GetTextBlocks()
        { return new TextBlock[] { }; }
    }
}
