using CockBlock8._1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace CockBlock8._1
{
    public class ShieldCannon
    {
        private const int INITIALENERGY = 100;
        //private double _energy;
        private bool _isCannon;
        private const int DAMAGE = 15; // TODO: Magic cookie
        private bool _shielded { get; set; }

        private BitmapImage _shieldSprite = new BitmapImage();
        private BitmapImage _shieldActiveSprite = new BitmapImage();
        private BitmapImage _cannonSprite = new BitmapImage();
        public ShieldCannon(bool up)
        {
            if(up)
            {
                _shieldSprite.UriSource = new Uri("ms-appx:Res/ShieldUp.png", UriKind.RelativeOrAbsolute);
                _shieldActiveSprite.UriSource = new Uri("ms-appx:Res/ShieldActiveUp.png", UriKind.RelativeOrAbsolute);
                _cannonSprite.UriSource = new Uri("ms-appx:Res/CanonUp.png", UriKind.RelativeOrAbsolute);
            }
            else
            {
                _shieldSprite.UriSource = new Uri("ms-appx:Res/ShieldDown.png", UriKind.RelativeOrAbsolute);
                _shieldActiveSprite.UriSource = new Uri("ms-appx:Res/ShieldActiveDown.png", UriKind.RelativeOrAbsolute);
                _cannonSprite.UriSource = new Uri("ms-appx:Res/CanonDown.png", UriKind.RelativeOrAbsolute);
            }
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
            _energy--;
            _shielded = true;
        }
        public void Deactivate()
        {
            _shielded = false;
        }

        public void Hit()
        {
            if(!_shielded)
            {
                _energy -= DAMAGE;
            }
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            // TODO fire event to ViewModel
        }

        public double _energy
        {
            get { return _energy; }
            set
            {
                if (value != _energy)
                {
                    _energy = value;
                    OnPropertyChanged("_energy");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
