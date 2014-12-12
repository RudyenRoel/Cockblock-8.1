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
            Player player1 = new Player(AMOUNTOFCANNONS / 2);
            Player player2 = new Player(AMOUNTOFCANNONS / 2);
            player2.ChangeState();
            _currentPage.SetImageSource("ShieldCannon1", player1.GetShieldCannons()[0].GetSprite());
            _currentPage.SetImageSource("ShieldCannon2", player1.GetShieldCannons()[1].GetSprite());
            _currentPage.SetImageSource("ShieldCannon3", player1.GetShieldCannons()[2].GetSprite());
            _currentPage.SetImageSource("ShieldCannon4", player2.GetShieldCannons()[0].GetSprite());
            _currentPage.SetImageSource("ShieldCannon5", player2.GetShieldCannons()[1].GetSprite());
            _currentPage.SetImageSource("ShieldCannon6", player2.GetShieldCannons()[2].GetSprite());
        }
    }
}
