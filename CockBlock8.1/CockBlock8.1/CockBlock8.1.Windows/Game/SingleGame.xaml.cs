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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace CockBlock8._1.Game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SingleGame : CB_Page
    {
        private int _totalLengthHealthRect = 700; // TODO: magic cookie
        private int _totalLengthTimerRect = 1080; // TODO: magic cookie
        private BitmapImage _CockUp = new BitmapImage();
        private BitmapImage _CockDown = new BitmapImage();
        private BitmapImage _Cock = new BitmapImage();
        private List<Image> _CockList;
        private bool _goingUp;
        private int[] _xCoords;
        private int _maxYCoord;
        private int _startYCoord;
        private int _marginChange;
        private int _distanceToHealth;
        public SingleGame()
        {
            this.InitializeComponent();
            _vm.StartSingleGame();
            init();
        }
        private void init()
        {
            _goingUp = false;
            SwitchGoingUp();
            //setHealthPlayer1(80);
            //setHealthPlayer2(90);
            _xCoords = new int[] { -560, -290, -20, 250, 520 };
            _CockUp.UriSource = new Uri("ms-appx:Res/CockUp.png", UriKind.RelativeOrAbsolute);
            _CockDown.UriSource = new Uri("ms-appx:Res/CockDown.png", UriKind.RelativeOrAbsolute);
            _CockList = new List<Image>();
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
        public void setHealthPlayer1(double percentage)
        {
            double currentLengthRect = ((double)_totalLengthHealthRect / 100 * percentage);
            this._TotalHealth1_rect.Width = _totalLengthHealthRect;
            this._CurrentHealth1_rect.Width = currentLengthRect;
            this._CurrentHealth1_tx.Text = ((int)percentage).ToString();
        }
        public void setHealthPlayer2(double percentage)
        {
            double currentLengthRect = ((double)_totalLengthHealthRect / 100 * percentage);
            this._TotalHealth2_rect.Width = _totalLengthHealthRect;
            this._CurrentHealth2_rect.Width = currentLengthRect;
            this._CurrentHealth2_tx.Text = ((int)percentage).ToString();
        }
        private void ShieldCannon1_PointerPressed(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonPressed(0, 0); }
        private void ShieldCannon2_PointerPressed(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonPressed(0, 1); }
        private void ShieldCannon3_PointerPressed(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonPressed(0, 2); }
        private void ShieldCannon4_PointerPressed(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonPressed(0, 3); }
        private void ShieldCannon5_PointerPressed(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonPressed(0, 4); }
        private void ShieldCannon6_PointerPressed(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonPressed(1, 0); }
        private void ShieldCannon7_PointerPressed(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonPressed(1, 1); }
        private void ShieldCannon8_PointerPressed(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonPressed(1, 2); }
        private void ShieldCannon9_PointerPressed(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonPressed(1, 3); }
        private void ShieldCannon10_PointerPressed(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonPressed(1, 4); }
        private void ShieldCannon1_PointerReleased(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonReleased(0, 0); }
        private void ShieldCannon2_PointerReleased(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonReleased(0, 1); }
        private void ShieldCannon3_PointerReleased(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonReleased(0, 2); }
        private void ShieldCannon4_PointerReleased(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonReleased(0, 3); }
        private void ShieldCannon5_PointerReleased(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonReleased(0, 4); }
        private void ShieldCannon6_PointerReleased(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonReleased(1, 0); }
        private void ShieldCannon7_PointerReleased(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonReleased(1, 1); }
        private void ShieldCannon8_PointerReleased(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonReleased(1, 2); }
        private void ShieldCannon9_PointerReleased(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonReleased(1, 3); }
        private void ShieldCannon10_PointerReleased(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonReleased(1, 4); }

        private void Start_bn_Click(object sender, RoutedEventArgs e)
        {
            _vm.NextTurn();
        }

        public void AddShot(int posX)
        {
            Image CockImage = new Image();
            CockImage.Source = _Cock;
            CockImage.Margin = new Thickness(_xCoords[posX], _startYCoord, 0, 0);
            CockImage.Width = 20;
            CockImage.Height = 20;
            _CockList.Add(CockImage);
            GameGrid.Children.Add(CockImage);
        }

        public void NextFrame()
        {
            for (int i = 0; i < _CockList.Count; i++)
            {
                int index = Array.IndexOf(_xCoords, ((int)_CockList[i].Margin.Left));
                if (_vm.ShieldUp(index) && _CockList[i].Margin.Top == _maxYCoord || !_vm.ShieldUp(index) && _CockList[i].Margin.Top == (_maxYCoord - _distanceToHealth))
                {
                    _vm.CheckHits(index);
                    RemoveCock(_CockList[i]);
                    i--;
                }
                else
                {
                    SetMargin(_CockList[i], 0, _marginChange, 0, 0);
                }
            }
        }

        private void RemoveCock(Image cock)
        {
            GameGrid.Children.Remove(cock);
            _CockList.Remove(cock);
        }

        private void SetMargin(Image obj, double left, double top, double right, double bottom)
        { obj.Margin = new Thickness(obj.Margin.Left + left, obj.Margin.Top - top, obj.Margin.Right + right, obj.Margin.Bottom + bottom); }


        public void SetEnergy(int player, int cannonNumber, int energy)
        {
            ((TextBlock)this.FindName("_p" + player + "_energy" + cannonNumber)).Text = "" + energy;
        }

        public void SwitchGoingUp()
        {
            // TODO Magic cookie mystery paradise
            _goingUp = !_goingUp;
            if (_goingUp)
            {
                _maxYCoord = -2210;
                _marginChange = 20;
                _startYCoord = -130;
                _distanceToHealth = 170;
                _Cock = _CockUp;
            }
            else
            {
                _maxYCoord = -200;
                _marginChange = -20;
                _startYCoord = -2160;
                _distanceToHealth = -170;
                _Cock = _CockDown;
            }
        }

        public void SetTime(int timePercentage)
        {
            int currentLength = (int)((double)_totalLengthTimerRect / 100 * timePercentage);
            _CurrentTime_Left_rect.Height = currentLength;
            _CurrentTime_Right_rect.Height = currentLength;
        }

        internal void GameOver(int player)
        {
            // if player = 1, player1 lost
            // if player = 2, player2 lost
        }
    }
}
