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
        private int _totalLengthHealthRect = 350; // TODO: magic cookie
        private int _totalLengthTimerRect = 540; // TODO: magic cookie
        private BitmapImage CockUp = new BitmapImage();
        private BitmapImage CockDown = new BitmapImage();
        private List<Image> CockList;
        private bool _goingUp;
        private int[] _xCoords;
        public SingleGame()
        {
            this.InitializeComponent();
            _vm.StartSingleGame();
            init();
        }
        private void init()
        {
            _goingUp = true;
            setHealthPlayer1(80);
            setHealthPlayer2(90);
            _xCoords = new int[] { -240, -20, 200 };
            CockUp.UriSource = new Uri("ms-appx:Res/CockUp.png", UriKind.RelativeOrAbsolute);
            CockDown.UriSource = new Uri("ms-appx:Res/CockDown.png", UriKind.RelativeOrAbsolute);
            CockList = new List<Image>();
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
            _vm.NextTurn();
        }

        public void AddShot(int posX)
        {
            Image CockImage = new Image();
            if (_goingUp)
            {
                CockImage.Source = CockUp;
                CockImage.Margin = new Thickness(_xCoords[posX], -130, 0, 0);
            }
            else
            {
                CockImage.Source = CockDown;
                CockImage.Margin = new Thickness(_xCoords[posX], -950, 0, 0);
            }
            CockImage.Width = 20;
            CockImage.Height = 20;
            CockList.Add(CockImage);
            GameGrid.Children.Add(CockImage);
        }

        public void NextFrame()
        {
            if (_goingUp)
            {
                for (int i = 0; i < CockList.Count; i++)
                {
                    if (CockList[i].Margin.Top == -880) //TODO Most magical cookie ever invented
                    {
                        RemoveImage(CockList[i]);
                        _vm.CheckHits(Array.IndexOf(_xCoords, ((int)CockList[i].Margin.Left)));
                        CockList.Remove(CockList[i]);
                        i--;
                    }
                    else
                    {
                        SetMargin(CockList[i], 0, 5, 0, 0);
                    }
                }
            }
            else
            {
                for (int i = 0; i < CockList.Count; i++)
                {
                    if (CockList[i].Margin.Top == -200) //TODO Most magical cookie ever invented
                    {
                        RemoveImage(CockList[i]);
                        _vm.CheckHits(Array.IndexOf(_xCoords, ((int)CockList[i].Margin.Left)));
                        CockList.Remove(CockList[i]);
                        i--;
                    }
                    else
                    {
                        SetMargin(CockList[i], 0, -5, 0, 0);
                    }
                }
            }
        }

        private void RemoveImage(Image cock)
        {
            GameGrid.Children.Remove(cock);
        }
        private void SetMargin(Image obj, double left, double top, double right, double bottom)
        { obj.Margin = new Thickness(obj.Margin.Left + left, obj.Margin.Top - top, obj.Margin.Right + right, obj.Margin.Bottom + bottom); }


        public void SetEnergy(int player, int cannonNumber, int energy)
        {
            Debug.WriteLine("AKA: " + _p1_energy1.Name);
            Debug.WriteLine("Changing energy of: " + "_p" + player + "_energy" + cannonNumber + ".");
            ((TextBlock)this.FindName("_p" + player + "_energy" + cannonNumber)).Text = "" + energy;
        }

        public void SwitchGoingUp()
        {
            _goingUp = !_goingUp;
        }

        public void SetTime(int timePercentage)
        {
            int currentLenth = (int)((double)_totalLengthTimerRect / 100 * timePercentage);
            _CurrentTime_Left_rect.Height = currentLenth;
            _CurrentTime_Right_rect.Height = currentLenth;
        }
    }
}
