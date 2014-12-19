using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace CockBlock8._1.View
{
    public partial class CB_Page : Page
    {
        public CB_ViewModel _vm;

        public CB_Page()
        {
            _vm = new CB_ViewModel(this);
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
        } 

        public void ChangeText(string name, string text)
        {
            TextBlock tb = (TextBlock)FindName(name);
            if (tb != null)
            {
                tb.Text = text;
            }
        }

        public void SetImageSource(string name, BitmapImage image)
        {
            Image i = (Image)FindName(name);
            if (i.GetType() == typeof(Image))
            {
                i.Source = image;
            }
        }
    }
}
