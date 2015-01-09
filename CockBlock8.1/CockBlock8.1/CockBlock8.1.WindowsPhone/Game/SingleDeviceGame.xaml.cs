using CockBlock8._1.Model;
using CockBlock8._1.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace CockBlock8._1.Game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SingleDeviceGame : CB_Page
    {
        private int _playerWantToReplay; // if 1 : 1 player wants to restart
        private int _playerIndexFirstChoice; // if 1: player 1, if 2: player 2
        private int _totalLengthHealthRect = 350; // TODO: magic cookie
        private int _totalLengthTimerRect = 540; // TODO: magic cookie
        private BitmapImage _CockUp = new BitmapImage();
        private BitmapImage _CockDown = new BitmapImage();
        private BitmapImage _Cock = new BitmapImage();
        private List<Image> _CockList;
        private bool _goingUp, _searchingLocation = false;
        private int[] _xCoords;
        private int _maxYCoord;
        private int _startYCoord;
        private int _marginChange;
        private int _distanceToHealth;
        public SingleDeviceGame()
        {
            this.InitializeComponent();
            Flags.Get.ToString();
            InitBackground();
            init();
        }
        private async Task InitBackground()
        {
            Debug.WriteLine("InitBackground");
            _vm.SetFlag();
        }
        private void init()
        {
            _playerIndexFirstChoice = 0;
            _playerWantToReplay = 0;
            _goingUp = false;
            SwitchGoingUp();
            //setHealthPlayer1(80);
            //setHealthPlayer2(90);
            _xCoords = new int[] { -240, -20, 200 };
            _CockUp.UriSource = new Uri("ms-appx:Res/CockUp.png", UriKind.RelativeOrAbsolute);
            _CockDown.UriSource = new Uri("ms-appx:Res/CockDown.png", UriKind.RelativeOrAbsolute);
            _CockList = new List<Image>();
            _stopShootingText1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            _stopShootingText2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            DefaultShieldCannonImageProparties(_ShieldCannon1, _ShieldCannon2, _ShieldCannon3, _ShieldCannon4, _ShieldCannon5, _ShieldCannon6);
            DefaultHealthBarProparties(Colors.White, _TotalHealth1_rect, _TotalHealth2_rect);
            DefaultHealthBarProparties(Colors.Red, _CurrentHealth1_rect, _CurrentHealth2_rect);
            DefaultTimerBarProparties(Colors.White, _totalLengthTimerRect, _FullTime_Left_rect, _FullTime_Right_rect);
            DefaultTimerBarProparties(Colors.Blue, _CurrentTime_Left_rect, _CurrentTime_Right_rect);
            DefaultEnergyProparties(Colors.White, 10, "100", _p1_energy1_tx, _p1_energy2_tx, _p1_energy3_tx, _p2_energy1_tx, _p2_energy2_tx, _p2_energy3_tx);
            DefaultEnergyProparties(Colors.DarkRed, 24, "100", _CurrentHealth1_tx, _CurrentHealth2_tx);
            FlyoutSettings();
        }
        private void FlyoutSettings()
        {
            SolidColorBrush foreground = Settings._DefaultButtonForeground;
            SolidColorBrush background = Settings._DefaultButtonBackground;
            _Rematch_p1_bn.Foreground = foreground;
            _Rematch_p1_bn.Background = background;
            _Exit_p1_bn.Foreground = foreground;
            _Exit_p1_bn.Background = background;
            _Save_p1_bn.Foreground = foreground;
            _Save_p1_bn.Background = background;
            _p1_Name_tx.Foreground = foreground;
            _p1_Name_tx.Background = background;
            _p1_Name_tx.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            _Save_p1_bn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            _Rematch_p2_bn.Foreground = foreground;
            _Rematch_p2_bn.Background = background;
            _Exit_p2_bn.Foreground = foreground;
            _Exit_p2_bn.Background = background;
            _Save_p2_bn.Foreground = foreground;
            _Save_p2_bn.Background = background;
            _p2_Name_tx.Foreground = foreground;
            _p2_Name_tx.Background = background;
            _p2_Name_tx.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            _Save_p2_bn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            SetFlyoutVisible(false);
        }
        private void DefaultEnergyProparties(Color color, int fontSize, params TextBlock[] textblocks)
        { DefaultEnergyProparties(color, fontSize, "", textblocks); }
        private void DefaultEnergyProparties(Color color, int fontSize, string text, params TextBlock[] textblocks)
        {
            foreach (TextBlock tb in textblocks)
            {
                tb.Text = text;
                tb.FontSize = fontSize;
                SetTextboxColor(color, tb);
            }
        }
        private void DefaultHealthBarProparties(Color color, params Rectangle[] rects)
        {
            foreach (Rectangle rect in rects)
            {
                SetRectangleColor(color, rect);
                rect.Height = 25;
                rect.Stroke = new SolidColorBrush(Colors.Black);
                rect.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
            }
        }
        private void DefaultTimerBarProparties(Color color, params Rectangle[] rects)
        { this.DefaultTimerBarProparties(color, -1, rects); }
        private void DefaultTimerBarProparties(Color color, int height, params Rectangle[] rects)
        {
            foreach (Rectangle rect in rects)
            {
                SetRectangleColor(color, rect);
                rect.Width = 15;
                if (height != -1)
                    rect.Height = height;
            }
        }
        private void SetRectangleColor(Color color, Rectangle rect) { rect.Fill = new SolidColorBrush(color); }
        private void SetTextboxColor(Color color, TextBlock tb) { tb.Foreground = new SolidColorBrush(color); }
        private void DefaultShieldCannonImageProparties(params Image[] images)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].Width = 100;
                images[i].Height = 100;
                if (i < images.Length / 2)
                    images[i].VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                else
                    images[i].VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom;
            }
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
        { _vm.ShieldCannonPressed(1, 0); }
        private void ShieldCannon5_PointerPressed(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonPressed(1, 1); }
        private void ShieldCannon6_PointerPressed(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonPressed(1, 2); }
        private void ShieldCannon1_PointerReleased(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonReleased(0, 0); }
        private void ShieldCannon2_PointerReleased(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonReleased(0, 1); }
        private void ShieldCannon3_PointerReleased(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonReleased(0, 2); }
        private void ShieldCannon4_PointerReleased(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonReleased(1, 0); }
        private void ShieldCannon5_PointerReleased(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonReleased(1, 1); }
        private void ShieldCannon6_PointerReleased(object sender, PointerRoutedEventArgs e)
        { _vm.ShieldCannonReleased(1, 2); }

        private void Start_bn_Click(object sender, RoutedEventArgs e)
        {
            Restart();
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
            ((TextBlock)this.FindName("_p" + player + "_energy" + cannonNumber + "_tx")).Text = "" + energy;
        }

        public void SwitchGoingUp()
        {
            // TODO Magic cookie mystery paradise
            _stopShootingText1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            _stopShootingText2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            _goingUp = !_goingUp;
            if (_goingUp)
            {
                _maxYCoord = -880;
                _marginChange = 5;
                _startYCoord = -130;
                _distanceToHealth = 170;
                _Cock = _CockUp;
            }
            else
            {
                _maxYCoord = -200;
                _marginChange = -5;
                _startYCoord = -950;
                _distanceToHealth = -170;
                _Cock = _CockDown;
            }
        }

        public void SetTime(int timePercentage)
        {
            int currentLength = (int)((double)_totalLengthTimerRect / 100 * timePercentage);
            _CurrentTime_Left_rect.Height = currentLength;
            _CurrentTime_Right_rect.Height = currentLength;
            if(timePercentage <= 0)
            {
                if(_goingUp)
                {
                    _stopShootingText2.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
                else
                {
                    _stopShootingText1.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
            }
        }

        internal void GameOver(int player, int score)
        {
            Debug.WriteLine("Player" + player + " lost!");
            // if player = 1, player1 lost
            // if player = 2, player2 lost
            string win = "You succesfully blocked the cock!";
            string lose = "You have been COCKBLOCKED!";
            if (player == 1)
            {
                _GameOver_p1_tx.Text = lose;
                _GameOver_p2_tx.Text = win;
                _Score_p1_tx.Text = "No score for losers!";
                _Score_p2_tx.Text = "Score: " + score;
                _Save_p2_bn.Visibility = Windows.UI.Xaml.Visibility.Visible;
                _p2_Name_tx.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                _GameOver_p1_tx.Text = win;
                _GameOver_p2_tx.Text = lose;
                _Score_p1_tx.Text = "Score: " + score;
                _Score_p2_tx.Text = "No score for losers!";
                _Save_p1_bn.Visibility = Windows.UI.Xaml.Visibility.Visible;
                _p1_Name_tx.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            SetFlyoutVisible(true);
        }

        private void Restart()
        {
            foreach (Image cock in _CockList)
            {
                GameGrid.Children.Remove(cock);
            }
            init();
            _vm.StartSingleGame();
            // Reset Timer + timer Bars
        }

        private void SetFlyoutVisible(bool result)
        {
            _stopShootingText1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            _stopShootingText2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Visibility vis = Windows.UI.Xaml.Visibility.Collapsed;
            if (result)
                vis = Windows.UI.Xaml.Visibility.Visible;
            this._Flyout_Player_1.Visibility = vis;
            this._Flyout_Player_2.Visibility = vis;
        }
        private void _Save_bn_Click(object sender, RoutedEventArgs e)
        {
            int score = 0;
            if(sender.Equals(_Save_p1_bn))
            {
                if (_p1_Name_tx.Text == "Enter Name")
                {
                    _p1_Name_tx.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    score = Int16.Parse(_Score_p1_tx.Text.Remove(0, 7));
                    Debug.WriteLine("Saving score: " + score + " by Player: " + _p1_Name_tx.Text);
                }
                
            }
            else
            {
                if (_p2_Name_tx.Text == "Enter Name")
                {
                    _p2_Name_tx.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    score = Int16.Parse(_Score_p2_tx.Text.Remove(0, 7));
                    Debug.WriteLine("Saving score: " + score + " by Player: " + _p2_Name_tx.Text);
                }
            }
        }

        private void _Exit_bn_p2_Click(object sender, RoutedEventArgs e)
        {
            if (ExitRematchButtonPressed(2, false))
            { _Exit_p2_bn.Foreground = Settings._PressedButtonForeground; _Exit_p2_bn.Background = Settings._PressedButtonBackground; }
        }
        private void _Rematch_bn_p2_Click(object sender, RoutedEventArgs e)
        {
            if (ExitRematchButtonPressed(2, true))
            { _Rematch_p2_bn.Foreground = Settings._PressedButtonForeground; _Rematch_p2_bn.Background = Settings._PressedButtonBackground; }
        }
        private void _Exit_bn_p1_Click(object sender, RoutedEventArgs e)
        {
            if (ExitRematchButtonPressed(1, false))
            { _Exit_p1_bn.Foreground = Settings._PressedButtonForeground; _Exit_p1_bn.Background = Settings._PressedButtonBackground; }
        }
        private void _Rematch_bn_p1_Click(object sender, RoutedEventArgs e)
        {
            if (ExitRematchButtonPressed(1, true))
            { _Rematch_p1_bn.Foreground = Settings._PressedButtonForeground; _Rematch_p1_bn.Background = Settings._PressedButtonBackground; }
        }
        private bool ExitRematchButtonPressed(int playerIndex, bool rematch)
        {
            if (playerIndex != _playerIndexFirstChoice)
            {
                if (_playerWantToReplay == 0)
                {
                    if (rematch)
                    { _playerWantToReplay = 1; }
                    else
                    { Exit(); }
                    _playerIndexFirstChoice = playerIndex;
                    return true;
                }
                else if (_playerWantToReplay == 1)
                {
                    if (rematch)
                    { Restart(); }
                    else
                    { Exit(); }
                }
            }
            return false;
        }
        private void Exit()
        { this.Frame.Navigate(typeof(MainPage)); }
        public void SetBackgroundFlag(BitmapImage img)
        { this._Flag_Image.Source = img; }
        internal override Button[] GetButtons()
        { return new Button[] { _Exit_p1_bn, _Exit_p2_bn, _Rematch_p1_bn, _Rematch_p2_bn, Start_bn }; }
        internal override TextBlock[] GetTextBlocks()
        { return new TextBlock[] { _CurrentHealth1_tx, _CurrentHealth2_tx, _GameOver_p1_tx, _GameOver_p2_tx, _p1_energy1_tx, _p1_energy2_tx, _p1_energy3_tx, _p2_energy1_tx, _p2_energy2_tx, _p2_energy3_tx }; }
    }
}
