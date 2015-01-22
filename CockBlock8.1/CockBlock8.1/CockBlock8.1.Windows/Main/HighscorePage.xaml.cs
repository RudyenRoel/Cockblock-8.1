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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CockBlock8._1.Main
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HighscorePage : CB_Page
    {
        private List<string[]> _HighscoreList = null;
        private int amountOfHighscores = 0;
        private ScrollViewer _ScrollViewer;
        private StackPanel _Highscores_Panel_Bottom;
        public HighscorePage()
        {
            this.InitializeComponent();
        }
        private void Init()
        {
            _Highscores_Panel_Bottom = new StackPanel();
            _ScrollViewer = new ScrollViewer();
            this._Title_tx.Text = "Score board";
            this._Title_tx.FontSize = Settings._DefaultHeaderFontSize;
            this._Title_tx.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            AddHeader();
            _ScrollViewer.Height = _Highscores_Panel_Top.Height - 100;
            _ScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            _ScrollViewer.VerticalScrollMode = ScrollMode.Auto;

            _ScrollViewer.Content = _Highscores_Panel_Bottom;
            _Highscores_Panel_Top.Children.Add(_ScrollViewer);
            foreach (string[] highscore in _HighscoreList)
            { AddHighscore(_Highscores_Panel_Bottom, highscore[0], highscore[1]); }
        }
        private void AddHeader()
        {
            AddHighscore(_Highscores_Panel_Top, "Name:", "Score:");

            (this._Highscores_Panel_Top.Children[0] as StackPanel).Background = new SolidColorBrush(Colors.DarkCyan);
            ((this._Highscores_Panel_Top.Children[0] as StackPanel).Children[0] as TextBlock).Text = "Nr.";
            for (int i = 0; i < ((this._Highscores_Panel_Top as StackPanel).Children[0] as StackPanel).Children.Count; i++)
            {
                TextBlock tb = (((this._Highscores_Panel_Top as StackPanel).Children[0] as StackPanel).Children[i] as TextBlock);
                tb.FontSize = 16;
                tb.Foreground = new SolidColorBrush(Colors.Black);
                tb.FontStyle = Windows.UI.Text.FontStyle.Italic;
            }
            amountOfHighscores--;
        }
        private void AddHighscore(StackPanel panel, string name, string score)
        {
            try
            {
                if (Int16.Parse(score) <= 0)
                    return;
            }
            catch (FormatException) { Debug.WriteLine("Format Exception: " + score); }
            amountOfHighscores++;
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.Width = 320;
            sp.Height = 50;
            sp.Margin = new Thickness(20, 20, 20, 20);
            sp.Background = new SolidColorBrush(Colors.Salmon);
            TextBlock index_tx = new TextBlock();
            index_tx.Text = amountOfHighscores.ToString();
            TextBlock name_tx = new TextBlock();
            name_tx.Text = name;
            TextBlock score_tx = new TextBlock();
            score_tx.Text = score;
            DefaultTextblockProperties((int)(sp.Width / 2 - 25), index_tx, name_tx, score_tx);
            index_tx.Width = 25;
            sp.Children.Add(index_tx);
            sp.Children.Add(name_tx);
            sp.Children.Add(score_tx);
            Debug.WriteLine("Before: Count: " + panel.Children.Count);
            panel.Children.Add(sp);
            Debug.WriteLine("After: Count: " + panel.Children.Count);
        }
        private void DefaultTextblockProperties(int width, params TextBlock[] tbs)
        {
            foreach (TextBlock tb in tbs)
            {
                tb.Width = width;
                tb.Text = StripText(tb.Text);
                tb.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                tb.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                tb.TextAlignment = TextAlignment.Center;
                tb.FontSize = 14;
                tb.TextWrapping = TextWrapping.Wrap;
            }
        }
        private string StripText(string text)
        {
            int maxLength = 64;
            string striptText = "";
            if (text.Length > maxLength)
            {
                Debug.WriteLine("Strip: " + text);
                string[] words = text.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    if (striptText.Length + words[i].Length <= maxLength)
                    { striptText += " " + words[i]; }
                }
            }
            else { striptText = text; }
            striptText.Trim();
            return striptText;
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _HighscoreList = e.Parameter as List<string[]>;
            Init();
        }

        private void Back_Bn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SingleGameMenu), e);
        }
    }
}