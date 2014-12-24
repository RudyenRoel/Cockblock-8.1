using CockBlock8._1.Common;
using CockBlock8._1.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace CockBlock8._1.Main.Instructions
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Instructions : CB_Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private List<string[]> _HelpPageInformation = null;

        public Instructions()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }
        private void Init()
        {
            Settings.DefaultTextBlockProperties(this._Title, this._SubTitle, this._Information_tx);
            Settings.DefaultButtonProperties(this._Help_bn);
            this._Help_bn.Content = "Help";
            this._Title.FontSize = Settings._DefaultHeaderFontSize;
            this._SubTitle.FontSize = Settings._DefaultSubHeaderFontSize;
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Tuple<string[], List<string[]>> information = (e.Parameter as Tuple<string[], List<string[]>>);
            string[] instructions = information.Item1;
            this._SubTitle.Text = instructions[0];
            this._Title.Text = instructions[1];
            this._Information_tx.Text = instructions[2];
            this._HelpPageInformation = information.Item2;
            Init();
        }

        #endregion
        private void Help_bn_Click(object sender, RoutedEventArgs e)
        { this.Frame.Navigate(typeof(HelpPage), _HelpPageInformation); }
        internal override Button[] GetButtons()
        { return new Button[] { this._Help_bn }; }
        internal override TextBlock[] GetTextBlocks()
        { return new TextBlock[] { this._Title, this._SubTitle, this._Information_tx }; }
    }
}
