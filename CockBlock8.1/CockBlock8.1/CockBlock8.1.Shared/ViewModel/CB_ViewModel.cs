using CockBlock8._1.Model;
using CockBlock8._1.View;
using CockBlock8._1.Game;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;

namespace CockBlock8._1
{
    public class CB_ViewModel
    {
        private CB_Page _currentPage;
        private Player[] _players;
        private string[] _shieldCannonNames = new string[] { "ShieldCannon1", "ShieldCannon2", "ShieldCannon3", "ShieldCannon4", "ShieldCannon5", "ShieldCannon6" };
        private const int AMOUNTOFCANNONS = 6; // TODO Settings file

        public CB_ViewModel(CB_Page page)
        {
            _currentPage = page;
            Debug.WriteLine(page.GetType().ToString());
        }

        public void StartSingleGame()
        {
            _players = new Player[]{new Player(0, AMOUNTOFCANNONS / 2), new Player(1, AMOUNTOFCANNONS / 2)};
            
            _players[1].ChangeState();
            SetImages(_players[0], 0);
            SetImages(_players[1], 1);
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
            if (cannon.IsCannon())
            {
                cannon.UseEnergy(0.5);
            }
            else
            {
                cannon.Activate();
                _currentPage.SetImageSource(_shieldCannonNames[3*playerIndex+shieldCannonIndex], cannon.GetSprite());
            }
        }
        public void ShieldCannonReleased(int playerIndex, int shieldCannonIndex)
        {
            ShieldCannon cannon = _players[playerIndex].GetShieldCannons()[shieldCannonIndex];
            if (cannon.IsCannon())
            {
                cannon.UseEnergy(0.5);
            }
            else
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
        }
    }
}
