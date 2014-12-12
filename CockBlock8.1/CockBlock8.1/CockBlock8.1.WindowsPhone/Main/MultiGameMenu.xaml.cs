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
<<<<<<< HEAD
using CockBlock8._1.Game;
=======
using CockBlock8._1.View;
>>>>>>> origin/master
namespace CockBlock8._1


    // The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556
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

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        { }
        private void SearchGame_bn_Click(object sender, RoutedEventArgs e)
        { }
        private void CreateGame_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(MultiGame), e); }
        private void Back_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(MainPage), e); }
        private void Instruction_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(InstructionsMultiGame), e); }
        private void SearchGame_bn_PointerEntered(object sender, PointerRoutedEventArgs e)
        { this.SearchGame_tx.Text = "Search for existing game"; }
        private void SearchGame_bn_PointerExited(object sender, PointerRoutedEventArgs e)
        { this.SearchGame_tx.Text = ""; }
        private void CreateGame_bn_PointerEntered(object sender, PointerRoutedEventArgs e)
        { this.CreateGame_tx.Text = "Create a new game"; }
        private void CreateGame_bn_PointerExited(object sender, PointerRoutedEventArgs e)
        { this.CreateGame_tx.Text = ""; }
    }
}
