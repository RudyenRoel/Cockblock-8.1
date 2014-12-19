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
        private const int SHIELDCOSTPERSECOND = 10;
        private double _energy;
        private bool _isCannon;
        private const int DAMAGE = 15; // TODO: Magic cookie
        private Player _player;
        private bool _shielded { get; set; }
        private bool _activated;

        private BitmapImage _shieldSprite = new BitmapImage();
        private BitmapImage _shieldActiveSprite = new BitmapImage();
        private BitmapImage _cannonSprite = new BitmapImage();
        public ShieldCannon(bool up, Player p)
        {
            if (up)
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
            _player = p;
            init();
        }

        private void init()
        {
            _activated = false;
            _shielded = false;
            _energy = INITIALENERGY;
            _isCannon = false;
        }

        public void Update()
        {
            if (_shielded)
            {
                UseEnergy((double)SHIELDCOSTPERSECOND / 60);
            }
        }

        public void ReplenishEnergy(double amount)
        {
            Energy += amount;
        }

        public void UseEnergy(double amount)
        {
            Energy -= amount;
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
                if (_shielded)
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

        public void Activate(bool shootAllowed)
        {
            _activated = true;
            Energy--;
            if (IsCannon() && shootAllowed)
            {
                _player.Shoot(this);
            }
            else
            {
                _shielded = true;
            }
        }
        public void Deactivate()
        {
            Debug.WriteLine("DEACTIVATING ");
            _activated = false;
            _shielded = false;
        }

        public void Hit()
        {
            Debug.WriteLine("Checking if shielded");
            if (!_shielded)
            {
                Debug.WriteLine("NOT SHIELDED!");
                Energy -= DAMAGE;
            }
        }
        public bool IsShielded()
        { return _shielded; }

        public bool Active()
        {
            return _activated;
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
            _player.EnergyChanged(this, (int)_energy);
        }

        public double Energy
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
