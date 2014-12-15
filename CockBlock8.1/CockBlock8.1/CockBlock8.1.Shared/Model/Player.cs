using System;
using System.Collections.Generic;
using System.Text;

namespace CockBlock8._1.Model
{
    public class Player
    {
        private int _health;
        private ShieldCannon[] _shieldCannons;
        private int _id;
        public Player(int ID, int amountOfCanons)
        {
            _shieldCannons = new ShieldCannon[amountOfCanons];
            _id = ID;
            init();
        }

        private void init()
        {
            _health = 100;
            for (int i = 0; i < _shieldCannons.Length; i++)
            {
                if(_id == 0)
                {
                    _shieldCannons[i] = new ShieldCannon(false);
                }
                else
                {
                    _shieldCannons[i] = new ShieldCannon(true);
                }
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

        public int GetId()
        {
            return _id;
        }
    }
}
