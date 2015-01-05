using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace CockBlock8._1
{
    public class Flags
    {
        private string[] _Flag_Names = null;
        private Dictionary<string, BitmapImage> _FlagImages = null;
        private static Flags _Flags = null;
        private Flags()
        { Init(); }
        private void Init()
        {
            Debug.WriteLine("Init Flags");
            _Flag_Names = new string[] { "Loading", "Holland" };
            _FlagImages = new Dictionary<string, BitmapImage>();
            LoadAllFlags();
        }
        public static Flags Get
        {
            get
            {
                if (_Flags == null)
                    _Flags = new Flags();
                return _Flags;
            }
        }
        private void LoadAllFlags()
        {
            foreach (string name in _Flag_Names)
            {
                _FlagImages.Add(name, LoadImage(name));
            }
        }
        public BitmapImage GetFlag(double lattitude, double longitude)
        { return _FlagImages[Search(lattitude, longitude)]; }
        private BitmapImage LoadImage(string name)
        {
            Debug.WriteLine("Loading Image: " + name + ".png");
            return new BitmapImage(new Uri("ms-appx:Res/Flags/" + name + ".png", UriKind.RelativeOrAbsolute));
        }

        private string Search(double lat, double lon)
        {
            Debug.WriteLine("Search Coordinates: " + lat + ", " + lon);
            int lat1 = (int)Math.Round(lat);
            int lon1 = (int)Math.Round(lon);
            Debug.WriteLine("Search Convert: " + lat1 + ", " + lon);
            string name = "Loading";
            if (Equals(lat, 51) && Equals(lon, 4)) { name = "Holland"; }

            return name;
        }
        public override string ToString()
        {
            return "Amount of Flags (" + _FlagImages.Count + ")";
        }
    }
}
