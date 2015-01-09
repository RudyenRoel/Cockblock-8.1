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
    public sealed partial class SingleGameMenu : CB_Page
    {
        public SingleGameMenu()
        {
            this.InitializeComponent();
        }

        private void Start_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(SingleDeviceGame), e); }

        private void Instruction_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(Instructions.Instructions), Settings.InstructionPageInformationSingleGame()); }

        private void Back_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(MainPage), e); }
    }
}
