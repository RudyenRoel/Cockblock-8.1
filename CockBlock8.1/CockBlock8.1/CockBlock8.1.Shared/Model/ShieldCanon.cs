using CockBlock8._1.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace CockBlock8._1
{
    class ShieldCanon
    {
        private int _posX;
        private int _posY;
        private int _energy;
        private bool _shieldCanonState;
        private bool _shielded { get; set; }

        private BitmapImage _shieldSprite = new BitmapImage();
        private BitmapImage _canonSprite = new BitmapImage();

        public ShieldCanon(int posX, int posY, int energy, bool shieldCanonState)
        {
            _shielded = false;
            _posX = posX;
            _posY = posY;
            _energy = energy;
            _shieldCanonState = shieldCanonState; //if TRUE shieldcanon = canon
            _shieldSprite.UriSource = new Uri("ms-appx:Res/Shield.png", UriKind.RelativeOrAbsolute);
            _canonSprite.UriSource = new Uri("ms-appx:Res/Canon.png", UriKind.RelativeOrAbsolute);
        }

        public void DoAction()
        {
            if(_shieldCanonState)
            {
                Cock cock = new Cock(_posX, _posY);
                cock.Start();
            }
            else
            {
                _energy--; // TODO adjust for correct speed
                _shielded = true;
            }
        }

        public void ChangeState()
        {
            _shieldCanonState = !_shieldCanonState;
        }

        public BitmapImage GetSprite()
        {
            if(_shieldCanonState)
            {
                return _canonSprite;
            }
            else
            {
                return _shieldSprite;
            }
        }
    }
}
