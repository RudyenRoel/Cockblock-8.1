using System;
using System.Collections.Generic;
using System.Text;

namespace CockBlock8._1.Model
{
    public class Player
    {
        private int _health;
        private ShieldCannon[] _shieldCannons;
        public Player(int amountOfCanons)
        {
            _shieldCannons = new ShieldCannon[amountOfCanons];
            init();
        }

        private void init()
        {
            _health = 100;
            for (int i = 0; i < _shieldCannons.Length; i++)
            {
                _shieldCannons[i] = new ShieldCannon();
            }
        }

        public void ChangeState()
        {
            foreach (ShieldCannon s in _shieldCannons)
            {
                s.ChangeState();
            }
        }

        public ShieldCannon[] GetShieldCannons()
        {
            return _shieldCannons;
        }
    }
}
