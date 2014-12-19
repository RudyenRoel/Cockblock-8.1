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
        private bool _shootAllowed;

        private BitmapImage _shieldSprite = new BitmapImage();
        private BitmapImage _shieldActiveSprite = new BitmapImage();
        private BitmapImage _cannonSprite = new BitmapImage();
        private BitmapImage _shieldInactiveSprite = new BitmapImage();
        private BitmapImage _cannonInactiveSprite = new BitmapImage();
        public ShieldCannon(bool up, Player p)
        {
            if (up)
            {
                _shieldSprite.UriSource = new Uri("ms-appx:Res/ShieldUp.png", UriKind.RelativeOrAbsolute);
                _shieldActiveSprite.UriSource = new Uri("ms-appx:Res/ShieldActiveUp.png", UriKind.RelativeOrAbsolute);
                _cannonSprite.UriSource = new Uri("ms-appx:Res/CanonUp.png", UriKind.RelativeOrAbsolute);
                _shieldInactiveSprite.UriSource = new Uri("ms-appx:Res/ShieldUpInactive.png", UriKind.RelativeOrAbsolute);
                _cannonInactiveSprite.UriSource = new Uri("ms-appx:Res/CanonUpInactive.png", UriKind.RelativeOrAbsolute);
            }
            else
            {
                _shieldSprite.UriSource = new Uri("ms-appx:Res/ShieldDown.png", UriKind.RelativeOrAbsolute);
                _shieldActiveSprite.UriSource = new Uri("ms-appx:Res/ShieldActiveDown.png", UriKind.RelativeOrAbsolute);
                _cannonSprite.UriSource = new Uri("ms-appx:Res/CanonDown.png", UriKind.RelativeOrAbsolute);
                _shieldInactiveSprite.UriSource = new Uri("ms-appx:Res/ShieldDownInactive.png", UriKind.RelativeOrAbsolute);
                _cannonInactiveSprite.UriSource = new Uri("ms-appx:Res/CanonDownInactive.png", UriKind.RelativeOrAbsolute);
            }
            _player = p;
            init();
        }

        private void init()
        {
            _shootAllowed = true;
            _activated = false;
            _shielded = false;
            _energy = INITIALENERGY;
            _isCannon = false;
        }

        public void Update()
        {
            if (_shielded && Energy > 0)
            {
                UseEnergy((double)SHIELDCOSTPERSECOND / 60);
            }
        }

        public void ReplenishEnergy(double amount)
        {
            Energy += amount;
            if (Energy > 100)
            {
                Energy = 100;
            }
        }

        public void UseEnergy(double amount)
        {
            Energy -= amount;
        }

        public void ChangeState()
        {
            _isCannon = !_isCannon;
            _shootAllowed = true;
        }

        public BitmapImage GetSprite()
        {
            if (_isCannon)
            {
                if (_shootAllowed && Energy > 0)
                {
                    return _cannonSprite;
                }
                else
                {
                    return _cannonInactiveSprite;
                }
            }
            else
            {
                if (Energy > 0)
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
                else
                {
                    return _shieldInactiveSprite;
                }
            }
        }

        public bool IsCannon()
        {
            return _isCannon;
        }

        public void DisableShooting()
        {
            _shootAllowed = false;
        }

        public void Activate()
        {
            _activated = true;
            if (IsCannon())
            {
                if (_shootAllowed)
                {
                    _player.Shoot(this);
                    Energy--;
                }
            }
            else
            {
                _shielded = true;
                Energy--;
            }
        }
        public void Deactivate()
        {
            _activated = false;
            _shielded = false;
        }

        public void Hit()
        {
            if (Energy <= 0)
            {
                _player.Damaged();
            }
            if (!_shielded)
            {
                Energy -= DAMAGE;
                if (Energy <= 0)
                {
                    Energy = 0;
                }
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


        public bool ShootingAllowed()
        {
            return _shootAllowed;
        }
    }
}
