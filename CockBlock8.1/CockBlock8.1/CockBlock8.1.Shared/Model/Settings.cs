using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace CockBlock8._1
{
    public class Settings
    {
        public static SolidColorBrush _DefaultButtonForeground = new SolidColorBrush(Colors.Gray);
        public static SolidColorBrush _DefaultButtonBackground = new SolidColorBrush(Colors.Black);
        public static SolidColorBrush _DefaultTextForeground = new SolidColorBrush(Colors.Gray);
        public readonly static SolidColorBrush _PressedButtonForeground = new SolidColorBrush(Colors.Yellow);
        public readonly static SolidColorBrush _PressedButtonBackground = new SolidColorBrush(Colors.LightBlue);
        public readonly static int _DefaultSmallFontSize = 12;
        public readonly static int _DefaultTextBlockFontSize = 18;
        public readonly static int _DefaultSubHeaderFontSize = 32;
        public readonly static int _DefaultHeaderFontSize = 48;
        public readonly static int _DefaultButtonFontSize = 20;
        public readonly static int _DefaultRadioButtonFontSize = 18;
        public readonly static string _ButtonBackgroundColorKey = "_ButtonBackground";
        public readonly static string _ButtonForegroundColorKey = "_ButtonForeground";
        public readonly static string _TextBlockForegroundColorKey = "_TextBlockForeground";
        public readonly static string _Instructions = "Instructions";
        public readonly static string _SettingsImage = "Settings.png";
        public readonly static string _MapImage = "Earth.png";

        private static string _SingleGameInstructions = null;
        private static string _MultiGameInstructions = null;
        private static string _AboutText = null;
        private static List<string[]> _SingleGameTopics = null;
        private static List<string[]> _MultiGameTopics = null;
        private static Tuple<string[], List<string[]>> _InformationSinglePage = null;
        private static Tuple<string[], List<string[]>> _InformationMultiPage = null;

        // public methods
        public static string About()
        {
            if (_AboutText == null)
            { CreateAbout(); }
            return _AboutText;
        }
        public static Tuple<string[], List<string[]>> InstructionPageInformationSingleGame()
        {
            if (_InformationSinglePage == null)
            { CreateInformationSingleGame(); }
            return _InformationSinglePage;
        }
        public static Tuple<string[], List<string[]>> InstructionPageInformationMultiGame()
        {
            if (_InformationMultiPage == null)
            { CreateInformationMultiGame(); }
            return _InformationMultiPage;
        }

        // private methods
        private static string SingleGame()
        {
            if (_SingleGameInstructions == null)
            { CreateSingleGameInstructions(); }
            return _SingleGameInstructions;
        }
        private static string MultiGame()
        {
            if (_MultiGameInstructions == null)
            { CreateMultiGameInstructions(); }
            return _MultiGameInstructions;
        }
        private static List<string[]> SingleGameTopics()
        {
            if (_SingleGameTopics == null)
            { CreateSingleGameTopics(); }
            return _SingleGameTopics;
        }
        private static List<string[]> MultiGameTopics()
        {
            if (_MultiGameTopics == null)
            { CreateMultiGameTopics(); }
            return _MultiGameTopics;
        }

        // private Creating methods
        private static void CreateSingleGameInstructions()
        {
            _SingleGameInstructions = "";
            SLine("Here are the instructions for the single game");
            SLine("You play it with Two teams, one on the top, and one on the bottem of the screen");
        }
        private static void CreateMultiGameInstructions()
        {
            _MultiGameInstructions = "";
            MLine("Here are the instructions for the multi game");
        }
        private static void CreateSingleGameTopics()
        {
            List<String[]> topics = new List<string[]>();
            topics.Add(topic("12345678901234567890123456789012345678901234567890", "123456789012345678901234567890123456789012345678901234567890"));
            topics.Add(topic("Where can i find the 'many asked questions'?", "You found it!"));
            topics.Add(topic("Bullshit", "Bullshit, Bullshit, bla bla bla"));
            topics.Add(topic("Who did this application can to its idea?", "The idea for the application has started at school. More about this, you'll find in the 'about'-page."));
            topics.Add(topic("Ranom", "Did you know that the skin of a showbear is black?!"));
            topics.Add(topic("Easter egg", "There are some easter eggs in this game. To find the easter eggs, search on the internet"));
            topics.Add(topic("Easter egg", "Did you search on the Internet? If not, go do it now!"));
            topics.Add(topic("Easter egg", "Did you look it up yet? Ow you did? Did you find any this? Offcourse not! 3:)"));
            _SingleGameTopics = topics;
        }
        private static void CreateMultiGameTopics()
        {
            List<String[]> topics = new List<string[]>();
            topics.Add(topic("Why can't I play 'Multi Game'?", "The game is not yet ready to play. It is in proces"));
            topics.Add(topic("Crep", "Here is some crap to test with"));
            topics.Add(topic("Crep", "Here is some crap to test with"));
            topics.Add(topic("Crep", "And there is more"));
            topics.Add(topic("Crep", "And there is much and  much more of it"));
            topics.Add(topic("Some very very very long topic to test the length of the topics with"));
            topics.Add(topic("An other very very very long topic to test. Or is this one even longer then the one above?"));
            topics.Add(topic("Crep"));
            _MultiGameTopics = topics;
        }
        private static void CreateAbout()
        {
            ALine("This project is called 'CockBlock8.1'");
            ALine("");
            ALine("Makers of this project are");
            ALine(" - 'Rudy Tjin-Con-Coen'");
            ALine("\tand");
            ALine(" - 'Roel Suntjens'.");
            ALine("This project is made to prove that we can build a Xaml project with the following specifications:");
            ALine(" - ");
            ALine(" - ");
            ALine("We are greatfull to give you te oppartunity to play our game.");
        }
        private static void CreateInformationSingleGame()
        {
            string[] item1 = new string[] { _Instructions, "Single Game", SingleGame() };
            List<string[]> item2 = new List<string[]>(SingleGameTopics());
            _InformationSinglePage = CreateInformation(item1, item2);
        }
        private static void CreateInformationMultiGame()
        {
            string[] item1 = new string[] { _Instructions, "Multi Game", MultiGame() };
            List<string[]> item2 = new List<string[]>(MultiGameTopics());
            _InformationMultiPage = CreateInformation(item1, item2);
        }

        // Help private methods
        private static Tuple<string[], List<string[]>> CreateInformation(string[] item1, List<string[]> item2)
        { return new Tuple<string[], List<string[]>>(item1, item2); }
        private static void SLine(string toAdd)
        { _SingleGameInstructions += toAdd + "\n"; }
        private static void MLine(string toAdd)
        { _MultiGameInstructions += toAdd + "\n"; }
        private static void ALine(string toAdd)
        { _AboutText += toAdd + "\n"; }
        private static string[] topic(string topicName)
        { return topic(topicName, "Unknown"); }
        private static string[] topic(string topicName, string text)
        { return new string[] { topicName, text }; }

        // Default setting methods
        public static void DefaultTextBlockProperties(params TextBlock[] textblocks)
        { DefaultTextBlockProperties("", textblocks); }
        public static void DefaultTextBlockProperties(int fontSize, params TextBlock[] textblocks)
        { DefaultTextBlockProperties("", fontSize, textblocks); }
        public static void DefaultTextBlockProperties(string text, params TextBlock[] textblocks)
        { DefaultTextBlockProperties(text, _DefaultTextBlockFontSize, textblocks); }
        public static void DefaultTextBlockProperties(string text, int fontSize, params TextBlock[] textblocks)
        {
            foreach (TextBlock tb in textblocks)
            {
                tb.FontSize = fontSize;
                tb.Text = text;
                tb.TextWrapping = Windows.UI.Xaml.TextWrapping.Wrap;
                tb.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                tb.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            }
        }
        public static void DefaultButtonProperties(params Button[] buttons)
        { DefaultButtonProperties("", buttons); }
        public static void DefaultButtonProperties(int fontSize, params Button[] buttons)
        { DefaultButtonProperties("", fontSize, buttons); }
        public static void DefaultButtonProperties(string text, params Button[] buttons)
        { DefaultButtonProperties(text, _DefaultButtonFontSize, buttons); }
        public static void DefaultButtonProperties(string text, int fontSize, params Button[] buttons)
        {
            foreach (Button button in buttons)
            {
                button.FontSize = fontSize;
                button.Content = text;
                button.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                button.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                button.Foreground = Settings._DefaultButtonForeground;
                button.Background = Settings._DefaultButtonBackground;
            }
        }
        public static void DefaultRadioButtonProperties(params RadioButton[] buttons)
        { DefaultRadioButtonProperties("", buttons); }
        public static void DefaultRadioButtonProperties(int fontSize, params RadioButton[] buttons)
        { DefaultRadioButtonProperties("", fontSize, buttons); }
        public static void DefaultRadioButtonProperties(string text, params RadioButton[] buttons)
        { DefaultRadioButtonProperties(text, _DefaultRadioButtonFontSize, buttons); }
        public static void DefaultRadioButtonProperties(string text, int fontSize, params RadioButton[] buttons)
        {
            foreach (RadioButton button in buttons)
            {
                button.FontSize = fontSize;
                button.Content = text;
                button.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                button.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                button.Foreground = Settings._DefaultButtonForeground;
                button.Background = Settings._DefaultButtonBackground;
            }
        }
        public class Countries
        {
            public static string Unknown = "Loading";
            public static string Nederland = "Netherlands";
            public static string Duitsland = "Germany";
            public static string Amerika = "USA";
            public static string Belgie = "Belgium";
            public static string Luxemburg = "Luxembourg";
        }
    }
}
