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
        private double _energy;
        private bool _isCannon;
        private bool _shielded { get; set; }

        private BitmapImage _shieldSprite = new BitmapImage();
        private BitmapImage _shieldActiveSprite = new BitmapImage();
        private BitmapImage _cannonSprite = new BitmapImage();
        public ShieldCannon()
        {
            _shieldSprite.UriSource = new Uri("ms-appx:Res/Shield.png", UriKind.RelativeOrAbsolute);
            _shieldActiveSprite.UriSource = new Uri("ms-appx:Res/ShieldActive.png", UriKind.RelativeOrAbsolute);
            _cannonSprite.UriSource = new Uri("ms-appx:Res/Canon.png", UriKind.RelativeOrAbsolute);
            init();
        }

        private void init()
        {
            _shielded = false;
            _energy = INITIALENERGY;
            _isCannon = false;
        }

        public void ReplenishEnergy(double amount)
        {
            _energy += amount;
        }

        public void UseEnergy(double amount)
        {
            _energy -= amount;
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
                if(_shielded)
                {
                    return _shieldActiveSprite;
                }
                else
                {
                    return _shieldSprite;
                }
            }
        }

        public bool IsCannon()
        {
            return _isCannon;
        }

        public void Activate()
        {
            _shielded = true;
        }
        public void Deactivate()
        {
            _shielded = false;
        }
    }
}
