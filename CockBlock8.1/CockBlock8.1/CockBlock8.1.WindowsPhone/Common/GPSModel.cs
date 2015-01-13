using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Services.Maps;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace CockBlock8._1.Model
{
    public class GPSModel
    {
        private static GPSModel _Instace = null;
        private static object pathlock = new object();
        private DateTime LastCheckPosition = new DateTime();
        private static bool _searchingLocation = false;
        private static bool _searchingCountry = false;
        private static bool _done = true; // hot fix
        private bool _locationOff = false;
        private GPSModel()
        {
            GeofenceMonitor.Current.Geofences.Clear();
            GeofenceMonitor.Current.GeofenceStateChanged += OnGeofenceStateChanged;
            CreateGeofence(51.5879, 4.7763, 3700, "Breda");
            CreateGeofence(51.5701, 5.0601, 4000, "Tilburg");
        }
        public static GPSModel Get
        {
            get
            {
                lock (pathlock)
                {
                    if (_Instace == null)
                        _Instace = new GPSModel();
                    return _Instace;
                }
            }
        }


        public async Task<Geoposition> GetCurrentLocation()
        {
            if (!_searchingLocation)
            {
                _searchingLocation = true;
                Debug.WriteLine("GPSMODEL: loading current location");
                Geoposition position = await LoadCurrentLocationAsync();
                _searchingLocation = false;
                Debug.WriteLine("GPSMODEL: done loading current location");
                return position;
            }
            else { Debug.WriteLine("Allready loading Current Location"); }
            return null;
        }
        public async Task<string> GetCurrentCountry(Geopoint point)
        {
            if (!_searchingCountry)
            {
                _searchingCountry = true;
                string country = await FindCorrospondingCountry(point);
                _searchingCountry = false;
                return country;
            }
            else { Debug.WriteLine("Allready loading Current Country"); }
            return null;
        }
        private bool LastGPSPositionIsValid()
        {
            int year = 0;
            int month = 0;
            int day = 0;
            int hour = 0;
            int min = 0;
            int sec = 0;
            DateTime now = new DateTime(year, month, day, hour, min, sec);
            return false;
        }
        public Geopoint GeopositionToPoint(Geoposition position)
        {
            if (position == null) return null;
            return new Geopoint(new BasicGeoposition { Longitude = position.Coordinate.Longitude, Latitude = position.Coordinate.Latitude });
        }
        private async Task<Geoposition> LoadCurrentLocationAsync()
        {
            var locator = new Geolocator();
            if (locator.LocationStatus == PositionStatus.Disabled)
            {
                Debug.WriteLine("LOCATOR IS OFF");
                _locationOff = true;
                return null;
            }
            else
            {
                locator.DesiredAccuracy = PositionAccuracy.Default;
                locator.DesiredAccuracyInMeters = 30;
                Stopwatch watch = new Stopwatch();
                watch.Start();
                var position = await locator.GetGeopositionAsync();
                watch.Stop();
                Debug.WriteLine("Load current location Time: " + watch.Elapsed);
                return position;
            }
        }
        private async Task<string> FindCorrospondingCountry(Geopoint geopoint)
        {
            if (geopoint != null)
            {
                var result = await MapLocationFinder.FindLocationsAtAsync(geopoint);
                string country = result.Locations[0].Address.Country;
                _searchingCountry = false;
                Debug.WriteLine("Country: " + country);
                return country;
            }
            else
            {
                return "Country not found!";
            }
        }
        public static bool IsDone() { return _done; }
        public void SetDone(bool result) { _done = result; }

        private void CreateGeofence(double latitude, double longitude, double radius, string name)
        {
            var id = string.Format(name);
            // Sets the center of the Geofence.
            var position = new BasicGeoposition
            {
                Latitude = latitude,
                Longitude = longitude
            };

            // The Geofence is a circular area centered at (latitude, longitude) point, with the
            // radius in meter.
            var geocircle = new Geocircle(position, radius);

            // Sets the events that we want to handle: in this case, the entrace and the exit
            // from an area of intereset.
            var mask = MonitoredGeofenceStates.Entered | MonitoredGeofenceStates.Exited;

            // Specifies for how much time the user must have entered/exited the area before 
            // receiving the notification.
            var dwellTime = TimeSpan.FromSeconds(1);

            // Creates the Geofence and adds it to the GeofenceMonitor.
            var geofence = new Geofence(id, geocircle, mask, false, dwellTime);
            //GeofenceMonitor.Current.Geofences.Clear();
            GeofenceMonitor.Current.Geofences.Add(geofence);
        }

        private async void OnGeofenceStateChanged(GeofenceMonitor sender, object e)
        {
            var reports = sender.ReadReports();
            Debug.WriteLine("IK BEN IN DE METHODE");

            foreach (var report in reports)
            {
                var state = report.NewState;
                var geofence = report.Geofence;

                GeofenceMonitor.Current.Geofences.Remove(geofence);

                if (state == GeofenceState.Entered)
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
                        async () =>
                        {
                            await MainPagePhone.ShowMessage("You're in " + geofence.Id + "!! The winner will get a bonus of 500 points!");
                        });
                    MainPagePhone.SetScoreBonus(500);
                    // User has entered the area.
                }
                else if (state == GeofenceState.Exited)
                {
                    // User has exited from the area.
                }
            }
        }
    }
}
