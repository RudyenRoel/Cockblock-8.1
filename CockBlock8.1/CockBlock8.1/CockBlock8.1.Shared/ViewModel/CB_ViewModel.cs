using CockBlock8._1.Model;
using CockBlock8._1.View;
using CockBlock8._1.Game;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

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
        private string _currentCountry = null;
        private DispatcherTimer myDispatcherTimer;
        private const int FRAMERATE = 16; // TODO: magic cookie : 16 milliseconds, 60 frames per second
        private const int TIMEPERTURNPHONE = 6 * 60; // TODO magic cookie: 6 seconds * 60 frames per second
        private const int TIMEFORSHOOTINGPHONE = 2 * 60; // TODO magic cookie: 2 seconds * 60 frames per second
        private const int TIMEPERTURNTABLET = 6 * 60; // TODO magic cookie: 3 seconds * 60 frames per second
        private const int TIMEFORSHOOTINGTABLET = 2 * 60; // TODO magic cookie: 1 seconds * 60 frames per second
        private const int NEWROUNDENERGY = 5;
        private const int STARTINGHEALTH = 100;
        private const int TIMEBETWEENSHOTS = 5;
        private int _shotTimer;
        private int _turnTimer;
        private int _shootTimer;
        private bool _flagSet;

        public CB_ViewModel(CB_Page page)
        {
            Debug.WriteLine(page.GetType().ToString());
            _currentPage = page;
            _flagSet = false;
            _turnTimer = TIMEPERTURNTABLET;
            _shootTimer = TIMEFORSHOOTINGTABLET;
#if WINDOWS_PHONE_APP
            _turnTimer = TIMEPERTURNPHONE;
            _shootTimer = TIMEFORSHOOTINGPHONE;
#endif
        }

        public void StartSingleGame(int player)
        {
            _turnTimer = TIMEPERTURNTABLET;
            _shootTimer = TIMEFORSHOOTINGTABLET;
            _amountOfCannons = AMOUNTOFCANNONSTABLET;
            _shieldCannonNames = new string[] { "_ShieldCannon1", "_ShieldCannon2", "_ShieldCannon3", "_ShieldCannon4", "_ShieldCannon5", "_ShieldCannon6", "_ShieldCannon7", "_ShieldCannon8", "_ShieldCannon9", "_ShieldCannon10" };
#if WINDOWS_PHONE_APP
            _turnTimer = TIMEPERTURNPHONE;
            _shootTimer = TIMEFORSHOOTINGPHONE;
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
            if(player == 0)
            {
                NextTurn();
            }
            ((SingleDeviceGame)_currentPage).setHealthPlayer1(STARTINGHEALTH);

            ((SingleDeviceGame)_currentPage).setHealthPlayer2(STARTINGHEALTH);
        }

        private void Update(object sender, object e)
        {
            _turnTimer--;
            _shootTimer--;
            _shotTimer++;
#if WINDOWS_PHONE_APP
            if (_currentCountry == null)
            {
                _currentCountry = MainPagePhone._currentCountry;
            }
            else if (_currentPage.GetType() == typeof(SingleDeviceGame) && !_flagSet)
            {
                _flagSet = true;
                SetFlag();
            }
#endif
            foreach (Player p in _players)
            {
                p.Update();
            }
            ((SingleDeviceGame)_currentPage).NextFrame();
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
                ((SingleDeviceGame)_currentPage).SetTime(percentage);
            }
        }

#if WINDOWS_PHONE_APP
        public void SetFlag()
        {
            Debug.WriteLine("Setting background flag, flag found: " + _currentCountry);
            if (_currentCountry == null)
            {
                _currentCountry = MainPagePhone._currentCountry;
            }
            ((SingleDeviceGame)_currentPage).SetBackgroundFlag(Flags.Get.FindFlag(_currentCountry));
        }
#endif
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
                    ((SingleDeviceGame)_currentPage).AddShot(shieldCannonIndex);
                }
                else
                {
                    cannon.Activate();

                    if (cannon.IsCannon() && cannon.ShootingAllowed())
                    {
                        _shotTimer = 0;
                        ((SingleDeviceGame)_currentPage).AddShot(shieldCannonIndex);
                    }
                    else
                    {
                        _currentPage.SetImageSource(_shieldCannonNames[(_amountOfCannons / 2) * playerIndex + shieldCannonIndex], cannon.GetSprite());
                    }
                }
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
            ((SingleDeviceGame)_currentPage).SwitchGoingUp();
        }

#if WINDOWS_PHONE_APP
        public async Task BackgroundCheck()
        {
            string country = "Loading";
            Geoposition position = await GPSModel.Get.GetCurrentLocation();
            Geopoint point = GPSModel.Get.GeopositionToPoint(position);
            try
            {
                country = await GPSModel.Get.GetCurrentCountry(point);
            }
            catch (ArgumentOutOfRangeException) {/* Some Posible function */ }
            Debug.WriteLine("Country: " + country);
            _currentCountry = country;
        }
#endif
        public string GetCountry()
        {
            return _currentCountry;
        }

        public void EnergyChanged(Player p, int cannon, int energy)
        {
            ((SingleDeviceGame)_currentPage).SetEnergy(Array.IndexOf(_players, p) + 1, cannon, energy);
            if (energy <= 0)
            {
                int playerIndex = Array.IndexOf(_players, p);
                _currentPage.SetImageSource(_shieldCannonNames[(_amountOfCannons / 2) * playerIndex + (cannon - 1)], _players[playerIndex].GetShieldCannons()[cannon - 1].GetSprite());
            }
        }

        public void CheckHits(int shieldCannonIndex)
        {
            _players[_currentDefender].CheckHits(shieldCannonIndex);
        }


        internal void HealthChanged(Player p, int health)
        {
            if (Array.IndexOf(_players, p) == 0)
            {
                ((SingleDeviceGame)_currentPage).setHealthPlayer1(health);
            }
            else
            {
                ((SingleDeviceGame)_currentPage).setHealthPlayer2(health);
            }
        }

        internal bool ShieldUp(int shieldCannonIndex)
        {
            return _players[_currentDefender].GetShieldCannons()[shieldCannonIndex].Energy > 0;
        }

        internal void ILost(Player player)
        {
            int winnerIndex = _players.Length - Array.IndexOf(_players, player) - 1;
            ((SingleDeviceGame)_currentPage).GameOver(Array.IndexOf(_players, player) + 1, CalculateScore(winnerIndex));
            StopTimer();
        }

        public void StopTimer()
        {
            if (myDispatcherTimer != null)
            {
                myDispatcherTimer.Stop();
            }
        }
        internal virtual Button[] GetAllButtons() { return null; }

        private int CalculateScore(int winnerIndex)
        {
            int score = 0;
            int energyTotalPlayer0 = 0;
            int energyTotalPlayer1 = 0;
            bool noShields = false;

            foreach (ShieldCannon sc in _players[0].GetShieldCannons())
            {
                energyTotalPlayer0 += (int)sc.Energy;
            }
            foreach (ShieldCannon sc in _players[1].GetShieldCannons())
            {
                energyTotalPlayer1 += (int)sc.Energy;
            }
            Debug.WriteLine("Player 1 has an energy total of: " + energyTotalPlayer0);
            Debug.WriteLine("Player 2 has an energy total of: " + energyTotalPlayer1);

            if (winnerIndex == 0)
            {
                score = energyTotalPlayer0 - energyTotalPlayer1;
                Debug.WriteLine("Player 1 wins, energy difference is: " + score);
                if (energyTotalPlayer1 == 0)
                {
                    noShields = true;
                }
            }
            else
            {
                score = energyTotalPlayer1 - energyTotalPlayer0;
                Debug.WriteLine("Player 2 wins, energy difference is: " + score);
                if (energyTotalPlayer0 == 0)
                {
                    noShields = true;
                }
            }
            Debug.WriteLine("Score: " + score + " ----- Winner health left: " + _players[winnerIndex].GetHealth());
            Debug.WriteLine("Score + " + _players[winnerIndex].GetHealth() * 5 + " (health * 5)");
            score += _players[winnerIndex].GetHealth() * 10;
            if (_players[winnerIndex].GetHealth() == 100)
            {
                Debug.WriteLine("Perfect win! (no health lost) BONUS POINTS: +200");
                score += 200;
            }
            if (noShields)
            {
                Debug.WriteLine("DESTRUCTION! (no energy left for opponent) BONUS POINTS: +500");
                score += 500;
            }
            Debug.WriteLine("Final Score = " + score);
            // TODO: Save Score
            return score;
        }
    }
}
