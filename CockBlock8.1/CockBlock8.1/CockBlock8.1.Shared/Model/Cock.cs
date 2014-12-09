using System;
using System.Collections.Generic;
using System.Text;

namespace CockBlock8._1.Model
{
    class Cock
    {
        private int _posX;
        private int _posY;

        public Cock(int posX, int posY) {
            _posX = posX;
            _posY = posY;
        }

        public void TestMethod() { }

        public void Start()
        {
            int endPos;
            if(_posY == 0) // TODO change to bottom shieldcanons
            {
                endPos = 100; // TODO change to top shieldcanons
                while (_posY < endPos)
                {
                    _posY++; // TODO adjust for correct speed
                }
            }
            else
            {
                endPos = 0; // TODO change to bottom shieldcanons
                while (_posY > endPos)
                {
                    _posY--; // TODO adjust for correct speed
                }
            }
        }
    }
}
