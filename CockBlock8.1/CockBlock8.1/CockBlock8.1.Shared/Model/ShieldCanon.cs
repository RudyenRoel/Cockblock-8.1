using CockBlock8._1.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace CockBlock8._1
{
    public class ShieldCannon
    {
        private const int INITIALENERGY = 100;
        private int _energy;
        private bool _isCannon;
        private bool _shielded { get; set; }

        private BitmapImage _shieldSprite = new BitmapImage();
        private BitmapImage _cannonSprite = new BitmapImage();
        public ShieldCannon()
        {
            _shieldSprite.UriSource = new Uri("ms-appx:Res/Shield.png", UriKind.RelativeOrAbsolute);
            _cannonSprite.UriSource = new Uri("ms-appx:Res/Canon.png", UriKind.RelativeOrAbsolute);
            init();
        }

        private void init()
        {
            _shielded = false;
            _energy = INITIALENERGY;
            _isCannon = false;
        }

        public void DoAction()
        {
            if (_isCannon)
            {
                //Cock cock = new Cock(_posX, _posY); // TODO check this
                //cock.Start();
            }
            else
            {
                _energy--; // TODO adjust for correct speed
                _shielded = true;
            }
        }

        public void ChangeState()
        {
            _isCannon = !_isCannon;
        }

        public BitmapImage GetSprite()
        {
            if (_isCannon)
            {
                return _cannonSprite;
            }
            else
            {
                return _shieldSprite;
            }
        }
    }
}
