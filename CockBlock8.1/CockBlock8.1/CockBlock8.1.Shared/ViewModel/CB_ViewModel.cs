using CockBlock8._1.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace CockBlock8._1
{
    class CB_ViewModel
    {
        private MainPage _currentPage;

        public CB_ViewModel(MainPage page)
        {
            _currentPage = page;
        }

        public void SetText(string name, string text)
        {
            _currentPage.ChangeText(name, text);
        }
    }
}
