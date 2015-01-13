using CockBlock8._1.Game;
using CockBlock8._1.Main;
using CockBlock8._1.Main.Instructions;
using CockBlock8._1.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace CockBlock8._1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SingleDeviceGameMenu : CB_Page
    {
        public SingleDeviceGameMenu()
        {
            this.InitializeComponent();
            this.Init();
        }
        private void Init()
        {
            Settings.DefaultTextBlockProperties(Settings._DefaultHeaderFontSize, this.Title_tx);
            Settings.DefaultButtonProperties(Settings._DefaultButtonFontSizePhone, this.StartGame_bn, this._Highscore_bn, this.Instructions_bn, this.Back_bn);
            this.Title_tx.Text = "Single Game";
            this.StartGame_bn.Content = "Start Game";
            this.Instructions_bn.Content = Settings._Instructions;
            this._Highscore_bn.Content = "Highscores";
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void StartGame_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(SingleDeviceGame), e); }
        private async void _Highscore_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(HighscorePage), await Settings.SingleDeviceHighscores()); }
        private async void Instructions_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(InstructionPage), await Settings.InstructionPageInformationSingleGame()); }
        internal override Button[] GetButtons()
        { return new Button[] { this.StartGame_bn }; }
        internal override TextBlock[] GetTextBlocks()
        { return new TextBlock[] { this.Title_tx }; }

    }
}
