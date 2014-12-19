using CockBlock8._1.Model;
using CockBlock8._1.View;
using CockBlock8._1.Game;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;
using Windows.UI.Xaml;

namespace CockBlock8._1
{
    public class CB_ViewModel
    {
        private CB_Page _currentPage;
        private int _currentShooter;
        private int _currentDefender;
        private Player[] _players;
        private string[] _shieldCannonNames = new string[] { "_ShieldCannon1", "_ShieldCannon2", "_ShieldCannon3", "_ShieldCannon4", "_ShieldCannon5", "_ShieldCannon6" };
        private const int AMOUNTOFCANNONS = 6; // TODO Settings file
        private DispatcherTimer myDispatcherTimer;
        private const int FRAMERATE = 16; // TODO: magic cookie : 16 milliseconds, 60 frames per second
        private const int TIMEPERTURN = 6 * 60; // TODO magic cookie: 10 seconds * 60 frames per second
        private const int TIMEFORSHOOTING = 2 * 60; // TODO magic cookie: 7 seconds * 60 frames per second
        private const int NEWROUNDENERGY = 5;
        private const int STARTINGHEALTH = 100;
        private int _turnTimer;
        private int _shootTimer;

        public CB_ViewModel(CB_Page page)
        {
            Debug.WriteLine(page.GetType().ToString());
            _currentPage = page;
            _turnTimer = TIMEPERTURN;
            _shootTimer = TIMEFORSHOOTING;
        }

        public void StartSingleGame()
        {
            if (myDispatcherTimer == null)
            {
                myDispatcherTimer = new DispatcherTimer();
                myDispatcherTimer.Interval = new TimeSpan(FRAMERATE); // 16 Milliseconds 
                myDispatcherTimer.Tick += Update;
                myDispatcherTimer.Start();
            }
            _players = new Player[] { new Player(this, 0, AMOUNTOFCANNONS / 2), new Player(this, 1, AMOUNTOFCANNONS / 2) };

            _players[0].ChangeState();
            SetImages(_players[0], 0);
            _players[0].ChangeState();
            SetImages(_players[0], 0);
            SetImages(_players[1], 1);
            _players[1].ChangeState();
            SetImages(_players[1], 1);
            _currentDefender = 0;
            _currentShooter = 1;
            ((SingleGame)_currentPage).setHealthPlayer1(STARTINGHEALTH);
            ((SingleGame)_currentPage).setHealthPlayer2(STARTINGHEALTH);
        }

        private void Update(object sender, object e)
        {
            _turnTimer--;
            _shootTimer--;

            foreach (Player p in _players)
            {
                p.Update();
            }
            ((SingleGame)_currentPage).NextFrame();
            if (_shootTimer <= 0)
            {
                foreach (ShieldCannon sc in _players[_currentShooter].GetShieldCannons())
                {
                    sc.DisableShooting();
                    _currentPage.SetImageSource(_shieldCannonNames[3 * (_currentShooter) + (Array.IndexOf(_players[_currentShooter].GetShieldCannons(), sc))], sc.GetSprite());
                }
                if (_turnTimer <= 0)
                {
                    NextTurn();
                }
            }
            else
            {
                ((SingleGame)_currentPage).SetTime((int)(((float)_shootTimer / TIMEFORSHOOTING) * 100));
            }
        }

        private void SetImages(Player player, int playerNumber)
        {
            _currentPage.SetImageSource(_shieldCannonNames[0 + 3 * playerNumber], player.GetShieldCannons()[0].GetSprite());
            _currentPage.SetImageSource(_shieldCannonNames[1 + 3 * playerNumber], player.GetShieldCannons()[1].GetSprite());
            _currentPage.SetImageSource(_shieldCannonNames[2 + 3 * playerNumber], player.GetShieldCannons()[2].GetSprite());
        }



        public void ShieldCannonPressed(int playerIndex, int shieldCannonIndex)
        {
            ShieldCannon cannon = _players[playerIndex].GetShieldCannons()[shieldCannonIndex];
            if (cannon.Energy > 0 && !cannon.Active())
            {
                Debug.WriteLine("ACTIVATE");
                cannon.Activate();
                _currentPage.SetImageSource(_shieldCannonNames[3 * playerIndex + shieldCannonIndex], cannon.GetSprite());
                if (cannon.IsCannon() && cannon.ShootingAllowed())
                {
                    ((SingleGame)_currentPage).AddShot(shieldCannonIndex);
                }
                else
                {
                    Debug.WriteLine("Shield! " + playerIndex + ", " + shieldCannonIndex);
                }
            }
            else
            {
                Debug.WriteLine("ShieldCannonPressed! else");
                //TODO: Image fade
            }
        }

        public void ShieldCannonReleased(int playerIndex, int shieldCannonIndex)
        {
            ShieldCannon cannon = _players[playerIndex].GetShieldCannons()[shieldCannonIndex];
            Debug.WriteLine("DEACTIVATE");
            cannon.Deactivate();
            if (!cannon.IsCannon())
            {
                _currentPage.SetImageSource(_shieldCannonNames[3 * playerIndex + shieldCannonIndex], cannon.GetSprite());
            }
        }

        public void NextTurn()
        {
            _turnTimer = TIMEPERTURN;
            _shootTimer = TIMEFORSHOOTING;
            foreach (Player p in _players)
            {
                foreach (ShieldCannon sc in p.GetShieldCannons())
                {
                    sc.ReplenishEnergy(NEWROUNDENERGY);
                }
            }
            _players[0].ChangeState();
            _players[1].ChangeState();
            SetImages(_players[0], 0);
            SetImages(_players[1], 1);
            int temp = _currentDefender;
            _currentDefender = _currentShooter;
            _currentShooter = temp;
            ((SingleGame)_currentPage).SwitchGoingUp();
        }

        public void EnergyChanged(Player p, int cannon, int energy)
        {
            // TODO Make neat, make events handle properly
            ((SingleGame)_currentPage).SetEnergy(Array.IndexOf(_players, p) + 1, cannon, energy);
            if (energy <= 0)
            {
                int playerIndex = Array.IndexOf(_players, p);
                _currentPage.SetImageSource(_shieldCannonNames[3 * playerIndex + (cannon - 1)], _players[playerIndex].GetShieldCannons()[cannon - 1].GetSprite());
            }
        }

        internal void ShootCock(Player p, int cannon)
        {
            int x = Array.IndexOf(_players, p) + 1;
            int y = cannon + 1;
            //new Cock(x, y); // TODO Remove Cock
        }

        public void CheckHits(int shieldCannonIndex)
        {
            Debug.WriteLine("Checking player ");
            _players[_currentDefender].CheckHits(shieldCannonIndex);
        }


        internal void HealthChanged(Player p, int health)
        {
            if (Array.IndexOf(_players, p) == 0)
            {
                ((SingleGame)_currentPage).setHealthPlayer1(health);
            }
            else
            {
                ((SingleGame)_currentPage).setHealthPlayer2(health);
            }
        }

        internal bool ShieldUp(int shieldCannonIndex)
        {
            return _players[_currentDefender].GetShieldCannons()[shieldCannonIndex].Energy > 0;
        }
    }
}
