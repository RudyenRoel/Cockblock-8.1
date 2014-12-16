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
        private Player[] _players;
        private string[] _shieldCannonNames = new string[] { "ShieldCannon1", "ShieldCannon2", "ShieldCannon3", "ShieldCannon4", "ShieldCannon5", "ShieldCannon6" };
        private const int AMOUNTOFCANNONS = 6; // TODO Settings file
        private DispatcherTimer myDispatcherTimer;
        private const int FRAMERATE = 16; // TODO: magic cookie

        public CB_ViewModel(CB_Page page)
        {
            _currentPage = page;
            Debug.WriteLine(page.GetType().ToString());
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
            _players = new Player[]{new Player(this, 0, AMOUNTOFCANNONS / 2), new Player(this, 1, AMOUNTOFCANNONS / 2)};
            
            _players[1].ChangeState();
            SetImages(_players[0], 0);
            SetImages(_players[1], 1);
            BitmapImage testBullet = new BitmapImage();
            testBullet.UriSource = new Uri("ms-appx:Res/Shield.png", UriKind.RelativeOrAbsolute);
        }

        private void Update(object sender, object e)
        {
            foreach(Player p in _players)
            {
                p.Update();
            }
            ((SingleGame)_currentPage).NextFrame();
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
            cannon.Activate();
            _currentPage.SetImageSource(_shieldCannonNames[3 * playerIndex + shieldCannonIndex], cannon.GetSprite());
            if(cannon.IsCannon())
            {
                ((SingleGame)_currentPage).AddShot(shieldCannonIndex);
            }
        }

        public void ShieldCannonReleased(int playerIndex, int shieldCannonIndex)
        {
            ShieldCannon cannon = _players[playerIndex].GetShieldCannons()[shieldCannonIndex];
            if (!cannon.IsCannon())
            {
                cannon.Deactivate();
                _currentPage.SetImageSource(_shieldCannonNames[3 * playerIndex + shieldCannonIndex], cannon.GetSprite());
            }
        }

        public void NextTurn()
        {
            _players[0].ChangeState();
            _players[1].ChangeState();
            SetImages(_players[0], 0);
            SetImages(_players[1], 1);
            ((SingleGame)_currentPage).SwitchGoingUp();
        }

        public void EnergyChanged(Player p, int cannon, int energy)
        {
            // TODO Make neat, make events handle properly
            ((SingleGame)_currentPage).SetEnergy(Array.IndexOf(_players, p) + 1, cannon, energy);
        }

        internal void ShootCock(Player p, int cannon)
        {
            int x = Array.IndexOf(_players, p) + 1;
            int y = cannon + 1;
            new Cock(x, y); 
        }
    }
}
