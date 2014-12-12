using CockBlock8._1.Model;
using CockBlock8._1.View;
using CockBlock8._1.Game;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace CockBlock8._1
{
    public class CB_ViewModel
    {
        private CB_Page _currentPage;

        public CB_ViewModel(CB_Page page)
        {
            _currentPage = page;
            switch(page.GetType().ToString())
            {
                //case typeof(SingleGame);
            }
        }

        public void SetText(string name, string text)
        {
            _currentPage.ChangeText(name, text);
        }
    }
}
