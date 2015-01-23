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
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace CockBlock8._1.Main.Instructions
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HelpPage : CB_Page
    {
        private int _TitleFontSize = 72; // TODO: magic cookie
        private int _SubTitleFontSize = 32; // TODO: magic cookie
        private int _TopicFontSize = 24; // TODO: magic cookie
        private int _DescriptionFontSize = 18; // TODO: magic cookie
        private List<string[]> _Instruction = new List<string[]>();
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public HelpPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            Init();
        }

        private void Init()
        {
            Settings.DefaultTextBlockProperties(_Title_tx, _F_A_Q_tx);
            _Title_tx.FontSize = _TitleFontSize;
            _F_A_Q_tx.FontSize = _SubTitleFontSize;
            _Title_tx.Text = "Help";
            _F_A_Q_tx.Text = "Frequently Asked Questions";
            _F_A_Q_tx.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
        }
        private void AddTopic(string topic, string description)
        {
            int topicLength = 32;
            int rowLength = 39;
            double factor = 1.3;
            int topicFontSizef = (int)(_TopicFontSize * factor);
            int descriptionFontSizef = (int)(_DescriptionFontSize * factor);
            int topicLines = (topic.Length / topicLength);
            int descriptionLines = (description.Length / rowLength);

            StackPanel sp = new StackPanel();

            // Calculation of the total stackpanel size
            sp.Height = topicFontSizef + (topicFontSizef * topicLines) + descriptionFontSizef + (descriptionFontSizef * descriptionLines);
            sp.Background = new SolidColorBrush(Colors.DarkRed);
            sp.Margin = new Thickness(0, 5, 0, 5);


            TextBlock topic_tx = new TextBlock();
            topic_tx.FontSize = _TopicFontSize;
            topic_tx.TextWrapping = TextWrapping.Wrap;
            topic_tx.Text = topic;

            TextBlock description_tx = new TextBlock();
            description_tx.FontSize = _DescriptionFontSize;
            description_tx.TextWrapping = TextWrapping.Wrap;
            description_tx.Text = description;
            sp.Children.Add(topic_tx);
            sp.Children.Add(description_tx);

            StackPanel sp2 = new StackPanel();
            sp2.Background = new SolidColorBrush(Colors.DarkSalmon);
            sp2.Margin = new Thickness(0, 10, 0, 0);
            sp2.Children.Add(sp);

            this.Help_panel.Children.Add(sp2);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (this._Instruction.Count <= 0)
            {
                this._Instruction = (e.Parameter as List<string[]>);
                foreach (string[] topic in _Instruction)
                { AddTopic(topic[0], topic[1]); }
            }
        }

        private void _Back_bn_Click(object sender, RoutedEventArgs e)
        {
            navigationHelper.GoBack();
            //if (this.Frame.CanGoBack) { this.Frame.GoBack(); } 
        }
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        { throw new NotImplementedException(); }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        { throw new NotImplementedException(); }

    }
}
