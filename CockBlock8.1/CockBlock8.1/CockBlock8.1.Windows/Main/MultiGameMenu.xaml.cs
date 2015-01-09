using CockBlock8._1.Game;
using CockBlock8._1.Main.Instructions;
using CockBlock8._1.View;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CockBlock8._1.Main
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MultiGameMenu : CB_Page
    {
        public MultiGameMenu()
        {
            this.InitializeComponent();
        }
        private void Instructions_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(Instructions.Instructions), Settings.InstructionPageInformationMultiGame()); }
        private void Back_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(MainPage), e); }
        private void SearchGame_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(MultiDeviceGame), e); }
        private void SearchGame_bn_PointerEntered(object sender, PointerRoutedEventArgs e)
        { this._SearchGame_tx.Text = "To Search for an existing game"; }
        private void SearchGame_bn_PointerExited(object sender, PointerRoutedEventArgs e)
        { this._SearchGame_tx.Text = ""; }
        private void CreateGame_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(MultiDeviceGame), e); }
        private void CreateGame_bn_PointerEntered(object sender, PointerRoutedEventArgs e)
        { this._CreateGame_tx.Text = "To Create a new game"; }
        private void CreateGame_bn_PointerExited(object sender, PointerRoutedEventArgs e)
        { this._CreateGame_tx.Text = ""; }
    }
}
