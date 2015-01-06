using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Services.Maps;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace CockBlock8._1
{
    public class Flags
    {
        private List<string> _Flag_Names = null;
        private Dictionary<string, BitmapImage> _FlagImages = null;
        private static Flags _Flags = null;
        private Flags()
        { Init(); }
        private void Init()
        {
            Debug.WriteLine("Init Flags");
            _Flag_Names = new List<string>();
            AddFlagNames(Settings.Countries.Unknown);
            AddFlagNames(Settings.Countries.Nederland, Settings.Countries.Belgie, Settings.Countries.Luxemburg);
            AddFlagNames(Settings.Countries.Duitsland, Settings.Countries.Amerika);
            _FlagImages = new Dictionary<string, BitmapImage>();
            LoadAllFlags();
        }
        private void AddFlagNames(params string[] names)
        { foreach (string name in names) { _Flag_Names.Add(name); } }
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
        private BitmapImage LoadImage(string name)
        {
            Debug.WriteLine("Loading Image: " + name + ".png");
            BitmapImage img = new BitmapImage(new Uri("ms-appx:Res/Flags/" + name + ".png", UriKind.RelativeOrAbsolute));
            return img;
        }
        public override string ToString()
        {
            return "Amount of Flags (" + _FlagImages.Count + ")";
        }

        public BitmapImage FindFlag(string country)
        {
            Debug.WriteLine("Find Flag: " + (country == null ? "Null" : country));
            try
            {
                if (country == null) { throw new KeyNotFoundException(); }
                BitmapImage img = _FlagImages[country];
                return img;
            }
            catch (KeyNotFoundException)
            {
                Debug.WriteLine("Could not find: " + country);
                return _FlagImages[Settings.Countries.Unknown];
            }
        }
    }
}
