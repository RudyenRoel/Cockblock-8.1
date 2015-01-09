using CockBlock8._1.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace CockBlock8._1
{
    public static class CB_Model
    {
        private static string _currentCountry;

        public static void init()
        {
            _currentCountry = DetermineCountry().Result;
        }

        private static async Task<string> DetermineCountry()
        {
            return null;
        }

        public static string GetCountry()
        {
            return _currentCountry;
        }
    }
}
