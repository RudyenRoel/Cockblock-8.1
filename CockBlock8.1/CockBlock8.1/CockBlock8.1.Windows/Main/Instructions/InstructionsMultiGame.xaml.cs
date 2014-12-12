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

namespace CockBlock8._1.Main.Instructions
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InstructionsMultiGame : Page
    {
        public InstructionsMultiGame()
        {
            this.InitializeComponent();
        }

<<<<<<< HEAD:CockBlock8.1/CockBlock8.1/CockBlock8.1.Windows/MainPage.xaml.cs
        public void ChangeText(string name, string text)
        {
            TextBox o = (TextBox)FindName(name);
            o.Text = text;
=======
        private void Back_bn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MultiGameMenu), e);
>>>>>>> origin/master:CockBlock8.1/CockBlock8.1/CockBlock8.1.Windows/Main/Instructions/InstructionsMultiGame.xaml.cs
        }
    }
}
