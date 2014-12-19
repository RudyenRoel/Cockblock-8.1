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
        private string[] _shieldCannonNames;
        private const int AMOUNTOFCANNONSPHONE = 6; // TODO Settings file
        private const int AMOUNTOFCANNONSTABLET = 10; // TODO Settings file
        private int _amountOfCannons;
        private DispatcherTimer myDispatcherTimer;
        private const int FRAMERATE = 16; // TODO: magic cookie : 16 milliseconds, 60 frames per second
        private const int TIMEPERTURNPHONE = 6 * 60; // TODO magic cookie: 6 seconds * 60 frames per second
        private const int TIMEFORSHOOTINGPHONE = 2 * 60; // TODO magic cookie: 2 seconds * 60 frames per second
        private const int TIMEPERTURNTABLET = 5 * 60; // TODO magic cookie: 3 seconds * 60 frames per second
        private const int TIMEFORSHOOTINGTABLET = 2 * 60; // TODO magic cookie: 1 seconds * 60 frames per second
        private const int NEWROUNDENERGY = 5;
        private const int STARTINGHEALTH = 100;
        private int _turnTimer;
        private int _shootTimer;

        public CB_ViewModel(CB_Page page)
        {
            Debug.WriteLine(page.GetType().ToString());
            _currentPage = page;
            _turnTimer = TIMEPERTURNTABLET;
            _shootTimer = TIMEFORSHOOTINGTABLET;
#if WINDOWS_PHONE_APP
            _turnTimer = TIMEPERTURNPHONE;
            _shootTimer = TIMEFORSHOOTINGPHONE;
#endif
        }

        public void StartSingleGame()
        {
            _amountOfCannons = AMOUNTOFCANNONSTABLET;
            _shieldCannonNames = new string[] { "_ShieldCannon1", "_ShieldCannon2", "_ShieldCannon3", "_ShieldCannon4", "_ShieldCannon5", "_ShieldCannon6", "_ShieldCannon7", "_ShieldCannon8", "_ShieldCannon9", "_ShieldCannon10" };
#if WINDOWS_PHONE_APP
            _amountOfCannons = AMOUNTOFCANNONSPHONE;
            _shieldCannonNames = new string[] { "_ShieldCannon1", "_ShieldCannon2", "_ShieldCannon3", "_ShieldCannon4", "_ShieldCannon5", "_ShieldCannon6" };
#endif

            StopTimer();
            myDispatcherTimer = new DispatcherTimer();
            myDispatcherTimer.Interval = new TimeSpan(FRAMERATE); // 16 Milliseconds 
            myDispatcherTimer.Tick += Update;
            myDispatcherTimer.Start();
            _players = new Player[] { new Player(this, 0, _amountOfCannons / 2), new Player(this, 1, _amountOfCannons / 2) };

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
                    if (sc.ShootingAllowed())
                    {
                        sc.DisableShooting();
                        _currentPage.SetImageSource(_shieldCannonNames[(_amountOfCannons / 2) * (_currentShooter) + (Array.IndexOf(_players[_currentShooter].GetShieldCannons(), sc))], sc.GetSprite());
                    }
                }
                if (_turnTimer <= 0)
                {
                    NextTurn();
                }
            }
            else
            {
                int percentage = (int)(((float)_shootTimer / TIMEFORSHOOTINGTABLET) * 100);
#if WINDOWS_PHONE_APP
                percentage = (int)(((float)_shootTimer / TIMEFORSHOOTINGPHONE) * 100);
#endif
                ((SingleGame)_currentPage).SetTime(percentage);
            }
        }

        private void SetImages(Player player, int playerNumber)
        {
            for (int i = 0; i < (_amountOfCannons / 2); i++)
            {
                _currentPage.SetImageSource(_shieldCannonNames[i + ((_amountOfCannons / 2) * playerNumber)], player.GetShieldCannons()[i].GetSprite());
            }
        }



        public void ShieldCannonPressed(int playerIndex, int shieldCannonIndex)
        {
            ShieldCannon cannon = _players[playerIndex].GetShieldCannons()[shieldCannonIndex];
            if (cannon.Energy > 0 && !cannon.Active())
            {
                cannon.Activate();
                if (cannon.IsCannon() && cannon.ShootingAllowed())
                {
                    ((SingleGame)_currentPage).AddShot(shieldCannonIndex);
                }
                else
                {
                    _currentPage.SetImageSource(_shieldCannonNames[(_amountOfCannons / 2) * playerIndex + shieldCannonIndex], cannon.GetSprite());
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
            cannon.Deactivate();
            if (!cannon.IsCannon())
            {
                _currentPage.SetImageSource(_shieldCannonNames[(_amountOfCannons / 2) * playerIndex + shieldCannonIndex], cannon.GetSprite());
            }
        }

        public void NextTurn()
        {
            _turnTimer = TIMEPERTURNTABLET;
            _shootTimer = TIMEFORSHOOTINGTABLET;
#if WINDOWS_PHONE_APP
            _turnTimer = TIMEPERTURNPHONE;
            _shootTimer = TIMEFORSHOOTINGPHONE;
#endif
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
                _currentPage.SetImageSource(_shieldCannonNames[(_amountOfCannons / 2) * playerIndex + (cannon - 1)], _players[playerIndex].GetShieldCannons()[cannon - 1].GetSprite());
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

        internal void ILost(Player player)
        {
            ((SingleGame)_currentPage).GameOver(Array.IndexOf(_players, player) + 1);
            StopTimer();
        }

        public void StopTimer()
        {
            if (myDispatcherTimer != null)
            {
                myDispatcherTimer.Stop();
            }
        }
    }
}
