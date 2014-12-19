using CockBlock8._1.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CockBlock8._1.Game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SingleGame : CB_Page
    {
        public SingleGame()
        {
            this.InitializeComponent();
        }

        internal void NextFrame()
        {
            throw new NotImplementedException();
        }

        internal void AddShot(int shieldCannonIndex)
        {
            throw new NotImplementedException();
        }

        internal void SwitchGoingUp()
        {
            throw new NotImplementedException();
        }

        internal void SetEnergy(int p, int cannon, int energy)
        {
            throw new NotImplementedException();
        }

        internal void SetTime(int p)
        {
            throw new NotImplementedException();
        }

        internal void setHealthPlayer1(int health)
        {
            throw new NotImplementedException();
        }
    }
}
