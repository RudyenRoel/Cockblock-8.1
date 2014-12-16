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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace CockBlock8._1.Main.Instructions
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HelpPageMultiGame : Page
    {
        public HelpPageMultiGame()
        {
            this.InitializeComponent();
            Init();
        }
        private void Init()
        {
            foreach (string[] topic in Introduction.MultiGameTopics())
            { AddTopic(topic[0], topic[1]); }
        }
        private void AddTopic(string topic, string description)
        {
            int topicFontSize = 24;
            int descriptionFontSize = 18;
            int topicLength = 35;
            int rowLength = 50;
            int topicFontSizef = (int)(topicFontSize * 1.5);
            int descriptionFontSizef = (int)(descriptionFontSize * 1.5);
            int topicLines = (topic.Length / topicLength);
            int descriptionLines = (description.Length / rowLength);

            StackPanel sp = new StackPanel();

            // Calculation of the total stackpanel size
            sp.Height = topicFontSizef + (topicFontSizef * topicLines) + descriptionFontSizef + (descriptionFontSizef * descriptionLines);
            //sp.Background = new SolidColorBrush(Colors.DarkGray);
            //sp.Margin = new Thickness(0, 10, 0, 10);

            TextBlock topic_tx = new TextBlock();
            topic_tx.FontSize = topicFontSize;
            topic_tx.TextWrapping = TextWrapping.Wrap;
            topic_tx.Text = topic;
            TextBlock description_tx = new TextBlock();
            description_tx.FontSize = descriptionFontSize;
            description_tx.TextWrapping = TextWrapping.Wrap;
            description_tx.Text = description;
            sp.Children.Add(topic_tx);
            sp.Children.Add(description_tx);
            this.Help_panel.Children.Add(sp);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
