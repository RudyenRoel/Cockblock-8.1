using CockBlock8._1.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace CockBlock8._1.Game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SingleGame : CB_Page
    {
        private int _totalLengthRect = 350; // TODO: magic cookie
        private Timer timer;
        public SingleGame()
        {
            this.InitializeComponent();
            _vm.StartSingleGame();
            init();
        }
        private void init()
        {
            setHealthPlayer1(80);
            setHealthPlayer2(90);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        public void setHealthPlayer1(double percentage)
        {
            double currentLengthRect = ((double)_totalLengthRect / 100 * percentage);
            this._TotalHealth1_rect.Width = _totalLengthRect;
            this._CurrentHealth1_rect.Width = currentLengthRect;
            this._CurrentHealth1_tx.Text = ((int)percentage).ToString();
        }
        public void setHealthPlayer2(double percentage)
        {
            double currentLengthRect = ((double)_totalLengthRect / 100 * percentage);
            this._TotalHealth2_rect.Width = _totalLengthRect;
            this._CurrentHealth2_rect.Width = currentLengthRect;
            this._CurrentHealth2_tx.Text = ((int)percentage).ToString();
        }
        private void ShieldCannon1_PointerPressed(object sender, PointerRoutedEventArgs e)
        { _vm.HandleTouchInput(1, 1); }
        private void ShieldCannon2_PointerPressed(object sender, PointerRoutedEventArgs e)
        { }
        private void ShieldCannon3_PointerPressed(object sender, PointerRoutedEventArgs e)
        { }
        private void ShieldCannon4_PointerPressed(object sender, PointerRoutedEventArgs e)
        { }
        private void ShieldCannon5_PointerPressed(object sender, PointerRoutedEventArgs e)
        { }
        private void ShieldCannon6_PointerPressed(object sender, PointerRoutedEventArgs e)
        { }

        private void Next_bn_Click(object sender, RoutedEventArgs e)
        { StartTimer(sender, e); }
        public void StartTimer(object o, RoutedEventArgs sender)
        {
            DispatcherTimer myDispatcherTimer = new DispatcherTimer();
            myDispatcherTimer.Interval = new TimeSpan(16); // 100 Milliseconds 
            myDispatcherTimer.Tick += NextFrame;
            myDispatcherTimer.Start();
        }

        private void NextFrame(object sender, object e)
        {
            this.TestBullet.Margin = new Thickness(TestBullet.Margin.Left, TestBullet.Margin.Top - 1, TestBullet.Margin.Right, TestBullet.Margin.Bottom);
        }
    }
}
