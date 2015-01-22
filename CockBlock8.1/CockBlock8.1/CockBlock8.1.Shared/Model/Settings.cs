using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace CockBlock8._1
{
    public class Settings
    {
        private static readonly string _ApplicationVersion = "0.4.0";
        public static SolidColorBrush _DefaultButtonForeground = new SolidColorBrush(Colors.Gray);
        public static SolidColorBrush _DefaultButtonBackground = new SolidColorBrush(Colors.Black);
        public static SolidColorBrush _DefaultTextForeground = new SolidColorBrush(Colors.Gray);
        public readonly static SolidColorBrush _PressedButtonForeground = new SolidColorBrush(Colors.Yellow);
        public readonly static SolidColorBrush _PressedButtonBackground = new SolidColorBrush(Colors.LightBlue);
        public readonly static int _DefaultSmallFontSize = 12;
        public readonly static int _DefaultTextBlockFontSize = 18;
        public readonly static int _DefaultSubHeaderFontSize = 32;
        public readonly static int _DefaultHeaderFontSize = 48;
        public readonly static int _DefaultButtonFontSizePhone = 20;
        public readonly static int _DefaultButtonFontSizeTablet = 32;
        public readonly static int _DefaultRadioButtonFontSize = 18;
        public readonly static int _MaxAmountOfHighscores = 10;
        public readonly static string _HighscoresSingleKey = "_Highscores_Single_";
        public readonly static string _HighscoresMultiKey = "_Highscores_Multi_";
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
        private static Tuple<Tuple<string[], string[]>, List<string[]>> _InformationSinglePage = null;
        private static Tuple<Tuple<string[], string[]>, List<string[]>> _InformationMultiPage = null;

        // public methods
        public async static Task<string> About()
        {
            if (_AboutText == null)
            { await CreateAbout(); }
            return _AboutText;
        }
        public async static Task<Tuple<Tuple<string[], string[]>, List<string[]>>> InstructionPageInformationSingleGame()
        {
            if (_InformationSinglePage == null)
            { await CreateInformationSingleGame(); }
            return _InformationSinglePage;
        }
        public async static Task<Tuple<Tuple<string[], string[]>, List<string[]>>> InstructionPageInformationMultiGame()
        {
            if (_InformationMultiPage == null)
            { await CreateInformationMultiGame(); }
            return _InformationMultiPage;
        }
        public async static Task<List<string[]>> SingleDeviceHighscores()
        { return await GetSingleDeviceHighscores(); }
        public async static Task<List<string[]>> MultiDeviceHighscores()
        { return await GetMultiDeviceHighscores(); }

        // private methods
        private async static Task<string> SingleGame()
        {
            if (_SingleGameInstructions == null)
            { await CreateSingleGameInstructions(); }
            return _SingleGameInstructions;
        }
        private async static Task<string> MultiGame()
        {
            if (_MultiGameInstructions == null)
            { await CreateMultiGameInstructions(); }
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
        private async static Task<List<string[]>> GetSingleDeviceHighscores()
        { return LoadHighscores(_HighscoresSingleKey); }
        private async static Task<List<string[]>> GetMultiDeviceHighscores()
        { return LoadHighscores(_HighscoresMultiKey); }
        // private Creating methods
        private static List<string[]> LoadHighscores(string key)
        {
            List<string[]> list = new List<string[]>();
            for (int i = 0; i < _MaxAmountOfHighscores; i++)
#if WINDOWS_PHONE_APP
            { list.Add((string[])(ApplicationData.Current.LocalSettings.Values[key + i])); }
#else
            { list.Add((string[])(Application.Current.Resources[key + i])); }
#endif
            return list;
        }
        private async static Task CreateSingleGameInstructions()
        {
            _SingleGameInstructions = "Loading...";
            _SingleGameInstructions = await LoadFromTextFile("single device instructinos.txt");
        }
        private async static Task CreateMultiGameInstructions()
        {
            _MultiGameInstructions = "Loading...";
            _MultiGameInstructions = await LoadFromTextFile("multi device instructions.txt");
        }
        private static void CreateSingleGameTopics()
        {
            List<String[]> topics = new List<string[]>();
            topics.Add(topic("Where can i find the 'Frequently Asked Questions'?", "You found it!"));
            topics.Add(topic("How do I shoot?", "By tapping on the cannon icon after pressing 'Start' to start the game."));
            topics.Add(topic("How do I block the cock?", "By tapping on the shield icon after pressing 'Start' to start the game."));
            topics.Add(topic("What is a cock?", "A 'cock' is an male chicken. This means that you are shooting birds in game."));
            topics.Add(topic("Can I do more with this application besides shooting and blocking?", "You can customize the buttons and text color and if you press the 'earth' icon you can see where you are right now."));
            topics.Add(topic("How did this application come to be?", "The idea for the application started at school. You can find more about the application in the 'about'-page."));
            _SingleGameTopics = topics;
        }
        private static void CreateMultiGameTopics()
        {
            List<String[]> topics = new List<string[]>();
            topics.Add(topic("Why can't I play a 'Multi Device Game'?", "This mode is not yet ready to play. It's in development"));
            _MultiGameTopics = topics;
        }
        private async static Task CreateAbout()
        {
            _AboutText = "Loading...";
            _AboutText = await LoadFromTextFile("about.txt");
        }
        private static async Task<string> LoadFromTextFile(string filename)
        {
            string text = "";
            Debug.WriteLine("Load From Text File: " + filename);
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Res/Text/" + filename));

            using (StreamReader reader = new StreamReader(await file.OpenStreamForReadAsync()))
            {
                text = reader.ReadToEnd();
            }

            return text;
        }
        private async static Task CreateInformationSingleGame()
        {
            string[] instructions1 = new string[] { _Instructions, "Single Game", await SingleGame() };
            string[] instructions2 = new string[] { "SG 1.png", "SG 2.png" }; // Add here your Images
            Tuple<string[], string[]> item1 = new Tuple<string[], string[]>(instructions1, instructions2);
            List<string[]> item2 = new List<string[]>(SingleGameTopics());
            _InformationSinglePage = CreateInformation(item1, item2);
        }
        private async static Task CreateInformationMultiGame()
        {
            Debug.WriteLine("Creating Information Multi Game");
            string[] instructions1 = new string[] { _Instructions, "Multi Game", await MultiGame() };
            string[] instructions2 = new string[] { "SG1 .png", "SG2.png" }; // Add here your Images
            Tuple<string[], string[]> item1 = new Tuple<string[], string[]>(instructions1, instructions2);
            List<string[]> item2 = new List<string[]>(MultiGameTopics());
            _InformationMultiPage = CreateInformation(item1, item2);
        }

        // Help private methods
        private static Tuple<Tuple<string[], string[]>, List<string[]>> CreateInformation(Tuple<string[], string[]> item1, List<string[]> item2)
        { return new Tuple<Tuple<string[], string[]>, List<string[]>>(item1, item2); }
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
        { DefaultButtonProperties(text, _DefaultButtonFontSizePhone, buttons); }
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
            public static string Argentinie = "Argentina";
            public static string Oosterijk = "Austria";
            public static string Belgie = "Belgium";
            public static string Bolivie = "Bolivia";
            public static string Brazilie = "Brazil";
            public static string Canada = "Canada";
            public static string Chicilie = "Chile";
            public static string Columbia = "Colombia";
            public static string Cuba = "Cuba";
            public static string Tsjechie = "Czech Republic";
            public static string Denemarken = "Denmark";
            public static string Ecuador = "Ecuador";
            public static string Estonia = "Estonia";
            public static string Finland = "Finland";
            public static string Frankrijk = "France";
            public static string Frans_Guiana = "Frensh Guiana";
            public static string Duitsland = "Germany";
            public static string Griekeland = "Greece";
            public static string Groenland = "Greenland";
            public static string Guyana = "Guyana";
            public static string Hongarije = "Hungary";
            public static string Ijsland = "Iceland";
            public static string Ierland = "Ireland";
            public static string Italie = "Italy";
            public static string Latvia = "Latvia";
            public static string Lithuania = "Lithuania";
            public static string Luxemburg = "Luxembourg";
            public static string Mexico = "Mexico";
            public static string Nederland = "Netherlands";
            public static string Noorwegen = "Norway";
            public static string Paraguay = "Paraguay";
            public static string Peru = "Peru";
            public static string Polen = "Poland";
            public static string Portugal = "Portugal";
            public static string Romenie = "Romania";
            public static string Rusland = "Russia";
            public static string Slowakije = "Slovakia";
            public static string Slovenie = "Slovania";
            public static string Spanje = "Spain";
            public static string Suriname = "Suriname";
            public static string Zweden = "Sweden";
            public static string Zwitserland = "Switzerland";
            public static string Oekraine = "Ukraine";
            public static string Engeland = "United Kingdom";
            public static string Amerika = "USA";
            public static string States = "United States";
            public static string Venezuela = "Venezuela";
            public static string[] WestEurope()
            {
                return new string[] { Nederland, Belgie, Luxemburg, Frankrijk, Spanje, Portugal, Italie, Engeland, Ierland, Ijsland };
            }
            public static string[] CenterEurope()
            {
                return new string[] { Duitsland, Denemarken, Oosterijk, Zwitserland, Italie, Tsjechie, Hongarije, Slowakije, Romenie };
            }
            public static string[] EastEurope()
            {
                return new string[] { Romenie, Polen, Griekeland, Lithuania, Latvia, Estonia, Noorwegen, Zweden, Finland };
            }
            public static string[] NorthAmerica()
            {
                return new string[] { Canada, Amerika, States, Mexico, Groenland };
            }
            public static string[] CenterAmerica()
            {
                return new string[] { };
            }
            public static string[] SouthAmerica()
            {
                return new string[] { Brazilie, Chicilie, Argentinie, Paraguay, Bolivie, Peru, Ecuador, Columbia, Venezuela, Guyana, Suriname, Frans_Guiana };
            }
            public static string[] Europe()
            {
                string[][] array = new string[][] { WestEurope(), CenterEurope(), EastEurope() };
                string[] europe = dubbleArrayToArray(array);
                return europe;
            }
            public static string[] America()
            {
                string[][] array = new string[][] { NorthAmerica(), CenterAmerica(), SouthAmerica() };
                string[] america = dubbleArrayToArray(array);
                return america;
            }
            public static string[] All()
            {
                string[][] countries = new string[][] { Europe(), America() };
                string[] totalArray = dubbleArrayToArray(countries);

                #region Debug
                foreach (string country in totalArray)
                { Debug.WriteLine("All Countries: " + country); }
                #endregion

                return totalArray;
            }
            private static string[] dubbleArrayToArray(string[][] countries)
            {
                List<string> list = new List<String>(); foreach (string[] array in countries)
                { foreach (string name in array) { list.Add(name); } }

                string[] totalArray = new string[list.Count];
                for (int i = 0; i < list.Count; i++)
                { totalArray[i] = list[i]; }
                return totalArray;
            }
        }
    }
}
