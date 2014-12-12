using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace CockBlock8._1.View
{
    public partial class CB_Page : Page
    {
        public virtual void ChangeText(string name, string text)
        {
            TextBlock tb = (TextBlock)FindName(name);
            if (tb != null)
            {
                tb.Text = text;
            }
        }

        public virtual void SetImageSource(string name, BitmapImage image)
        {
            Image i = (Image)FindName(name);
            if (i.GetType() == typeof(Image))
            {
                i.Source = image;
            }
        }
    }
}
