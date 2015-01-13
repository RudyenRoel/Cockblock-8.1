using CockBlock8._1.Common;
using CockBlock8._1.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace CockBlock8._1.Main
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private Geopoint _LastValidPosition = null;
        private Geopoint c;
        private bool loadingMapLocation = false;

        public MapPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            Init();
        }

        #region Initialisation
        private void Init()
        {
            MyMap.MapServiceToken = "asdasdasdas";
            this._Title_tx.Text = "Your Current Location";
            this._Country_tx.Text = "Country:";
            this._Coordinates_tx.Text = "Coordinates:";
            UpdateCurrentLocation();
        }
        private async Task UpdateCurrentLocation()
        {
            if (!loadingMapLocation)
            {
                loadingMapLocation = true;
                SetRefreshFeedback("Loading Location...");
                var position = await GPSModel.Get.GetCurrentLocation();
                SetRefreshFeedback("Loading Location....");
                Geopoint point = GPSModel.Get.GeopositionToPoint(position);
                string country = await GPSModel.Get.GetCurrentCountry(point);
                Debugging(point, country);
                country = (country == null ? "Unknown" : country);
                SetCountry(country);
                point = (point == null ? new Geopoint(new BasicGeoposition { Longitude = 0, Latitude = 0 }) : point);
                SetRefreshFeedback("Going to Location");
                SetCoordinates(point.Position.Longitude, point.Position.Latitude);
                if (point.Position.Latitude != 0 && point.Position.Longitude != 0)
                {
                    c = null;
                    _LastValidPosition = point;
                    await CenterView();
                    await SetCenterLocation(point);
                }
                _Refresh_Feedback_tx.Text = "";
                loadingMapLocation = false;
            }
        }
        private void SetRefreshFeedback(string text)
        { _Refresh_Feedback_tx.Text = text; }
        private async Task SetCenterLocation(Geopoint point)
        {
            MyMap.Center = point;
            c = MyMap.Center;
            Debug.WriteLine("C = set to " + c.Position.Longitude);
        }

        private void Debugging(Geopoint point, string country)
        {
            if (point == null)
                Debug.WriteLine("Allready Loading Current Position");
            else
                Debug.WriteLine("Calculating Position");
            if (country == null)
                Debug.WriteLine("Allready Loading Current Country");
            else
                Debug.WriteLine("Calculation Country");
        }
        private void SetCountry(string country)
        { this._CurrentCountry_tx.Text = country; }
        private void SetCoordinates(double x, double y)
        { this._CurrentCoordinates_tx.Text = x + ", " + y; }

        #endregion

        #region NavigationHelper registration
        public NavigationHelper NavigationHelper
        { get { return this.navigationHelper; } }
        public ObservableDictionary DefaultViewModel
        { get { return this.defaultViewModel; } }
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        { }
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        { }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        { this.navigationHelper.OnNavigatedTo(e); }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        { this.navigationHelper.OnNavigatedFrom(e); }

        #endregion
        #region Buttons
        private void _Refresh_bn_Click(object sender, RoutedEventArgs e)
        { UpdateCurrentLocation(); }
        private void _Zoom_In_bn_Click(object sender, RoutedEventArgs e)
        { ZoomIn(); }
        private void _Zoom_Out_bn_Click(object sender, RoutedEventArgs e)
        { ZoomOut(); }
        #endregion

        private async Task CenterView()
        {
            Debug.WriteLine("Center View!");
            if (_LastValidPosition != null)
            { await MyMap.TrySetViewAsync(_LastValidPosition, 10D); }
            else { Debug.WriteLine("Center view position = null"); }
        }
        private async void ZoomIn()
        {
            SetRefreshFeedback("Zooming in...");
            await MyMap.TrySetViewAsync(_LastValidPosition, 17D);
            SetRefreshFeedback("");
        }
        private async void ZoomOut()
        {
            SetRefreshFeedback("Zooming out...");
            await MyMap.TrySetViewAsync(_LastValidPosition, 10D);
            SetRefreshFeedback("");
        }
        private void MyMap_CenterChanged(Windows.UI.Xaml.Controls.Maps.MapControl sender, object args)
        {
            //CenterView();
            if (c != null)
            {
                Debug.WriteLine("Center changed! c = " + c.Position.Longitude + " center = " + MyMap.Center.Position.Longitude);
                MyMap.Center = c;
            }
        }
    }
}
