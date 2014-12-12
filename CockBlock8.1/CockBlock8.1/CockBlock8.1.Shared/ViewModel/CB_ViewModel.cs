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
        private const int AMOUNTOFCANNONS = 6; // TODO Settings file

        public CB_ViewModel(CB_Page page)
        {
            _currentPage = page;
            Debug.WriteLine(page.GetType().ToString());
            /*
            switch(page.GetType().ToString())
            {
                case "CockBlock8._1.Game.SingleGame":
                    StartSingleGame();
                    break;
            }
             * */
        }

        public void StartSingleGame()
        {
            _players = new Player[]{new Player(AMOUNTOFCANNONS / 2), new Player(AMOUNTOFCANNONS / 2)};
            
            _players[1].ChangeState();
            _currentPage.SetImageSource("ShieldCannon1", _players[0].GetShieldCannons()[0].GetSprite());
            _currentPage.SetImageSource("ShieldCannon2", _players[0].GetShieldCannons()[1].GetSprite());
            _currentPage.SetImageSource("ShieldCannon3", _players[0].GetShieldCannons()[2].GetSprite());
            _currentPage.SetImageSource("ShieldCannon4", _players[1].GetShieldCannons()[0].GetSprite());
            _currentPage.SetImageSource("ShieldCannon5", _players[1].GetShieldCannons()[1].GetSprite());
            _currentPage.SetImageSource("ShieldCannon6", _players[1].GetShieldCannons()[2].GetSprite());
        }

        public void HandleTouchInput(int playerIndex, int shieldCannonIndex)
        {
            ShieldCannon cannon = _players[playerIndex].GetShieldCannons()[shieldCannonIndex];
            if(cannon.IsCannon())
            {
                cannon.UseEnergy(0.5);
            }
            else
            {
                cannon.UseEnergy(0.1);
                cannon.Activate();
            }
        }
    }
}
