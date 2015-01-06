using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;

namespace CockBlock8._1.Model
{
    public class GPSModel
    {
        private DateTime LastCheckPosition = new DateTime();
        private bool _searchingLocation = false;
        private bool _searchingCountry = false;
        public GPSModel()
        {
        }
        public async Task<Geoposition> GetCurrentLocation()
        {
            if (!_searchingLocation)
            {
                _searchingLocation = true;
                Geoposition position = await LoadCurrentLocationAsync();
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
            locator.DesiredAccuracy = PositionAccuracy.Default;
            locator.DesiredAccuracyInMeters = 30;

            Stopwatch watch = new Stopwatch();
            watch.Start();
            var position = await locator.GetGeopositionAsync();
            watch.Stop();
            _searchingLocation = false;
            Debug.WriteLine("Load current location Time: " + watch.Elapsed);
            return position;
        }
        private async Task<string> FindCorrospondingCountry(Geopoint geopoint)
        {
            var result = await MapLocationFinder.FindLocationsAtAsync(geopoint);
            string country = result.Locations[0].Address.Country;
            _searchingCountry = false;
            Debug.WriteLine("Country: " + country);
            return country;
        }
    }
}
