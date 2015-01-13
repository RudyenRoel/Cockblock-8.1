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
using CockBlock8._1.Main.Instructions;
using CockBlock8._1.Game;
using CockBlock8._1.View;
using CockBlock8._1.Main;
namespace CockBlock8._1


    // The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MultiDeviceGameMenu : CB_Page
    {
        public MultiDeviceGameMenu()
        {
            this.InitializeComponent();
            Init();
        }

        private void Init()
        {
            Settings.DefaultButtonProperties(Settings._DefaultButtonFontSizePhone, this.SearchGame_bn, this.CreateGame_bn, this.Instructions_bn,this.Highscore_bn, this.Back_bn);
            Settings.DefaultTextBlockProperties(Settings._DefaultSmallFontSize, this.Title_tx, this.SearchGame_tx, this.CreateGame_tx);
            this.Title_tx.Text = "Multi Game";
            this.Title_tx.FontSize = Settings._DefaultHeaderFontSize;
            this.SearchGame_bn.Content = "Search Game";
            this.CreateGame_bn.Content = "Create Game";
            this.Instructions_bn.Content = Settings._Instructions;
            this.Highscore_bn.Content = "Highscores";
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
        private void SearchGame_bn_Click(object sender, RoutedEventArgs e)
        { }
        private void CreateGame_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(MultiDeviceGame), e); }
        private async void Highscore_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(HighscorePage), await Settings.MultiDeviceHighscores()); }
        private async void Instruction_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(InstructionPage), await Settings.InstructionPageInformationMultiGame()); }
        private void SearchGame_bn_PointerEntered(object sender, PointerRoutedEventArgs e)
        { this.SearchGame_tx.Text = "Search for existing game"; }
        private void SearchGame_bn_PointerExited(object sender, PointerRoutedEventArgs e)
        { this.SearchGame_tx.Text = ""; }
        private void CreateGame_bn_PointerEntered(object sender, PointerRoutedEventArgs e)
        { this.CreateGame_tx.Text = "Create a new game"; }
        private void CreateGame_bn_PointerExited(object sender, PointerRoutedEventArgs e)
        { this.CreateGame_tx.Text = ""; }
        internal override Button[] GetButtons()
        { return new Button[] { this.Back_bn, this.Highscore_bn, this.SearchGame_bn, this.CreateGame_bn }; }
        internal override TextBlock[] GetTextBlocks()
        { return new TextBlock[] { this.CreateGame_tx, this.SearchGame_tx, this.Title_tx }; }

    }
}
