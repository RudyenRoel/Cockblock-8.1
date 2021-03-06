﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Text;

namespace CockBlock8._1.Model
{
    public class Player
    {
        private int _health;
        private ShieldCannon[] _shieldCannons;
        private int _id;
        private CB_ViewModel _vm;
        private bool _lost;
        public Player(CB_ViewModel vm, int ID, int amountOfCanons)
        {
            _vm = vm;
            _shieldCannons = new ShieldCannon[amountOfCanons];
            _id = ID;
            init();
        }

        public void Update()
        {
            foreach (ShieldCannon sc in _shieldCannons)
            {
                sc.Update();
            }
        }
        private void init()
        {
            _health = 100;
            _lost = false;
            for (int i = 0; i < _shieldCannons.Length; i++)
            {
                if (_id == 0)
                {
                    _shieldCannons[i] = new ShieldCannon(false, this);
                }
                else
                {
                    _shieldCannons[i] = new ShieldCannon(true, this);
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

        public void EnergyChanged(ShieldCannon s, int energy)
        {
            _vm.EnergyChanged(this, Array.IndexOf(_shieldCannons, s) + 1, energy);
        }

        public void CheckHits(int shieldCannonIndex)
        {
            _shieldCannons[shieldCannonIndex].Hit();
        }

        internal void Damaged()
        {
            _health -= 15;
            if (_health < 0)
            {
                _health = 0;
                if(!_lost)
                {
                    _vm.ILost(this);
                    _lost = true;
                }
            }
            _vm.HealthChanged(this, _health);
        }

        public int GetHealth()
        {
            return _health;
        }
    }
}
