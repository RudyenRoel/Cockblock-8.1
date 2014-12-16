using System;
using System.Collections.Generic;
using System.Text;

namespace CockBlock8._1
{
    public class Introduction
    {
        private static string _SingleGameInstructions = null;
        private static string _MultiGameInstructions = null;
        private static List<string[]> _SingleGameTopics = null;
        private static List<string[]> _MultiGameTopics = null;
        private static string _About = null;
        public static string SingleGame()
        {
            if (_SingleGameInstructions == null)
            { CreateSingleGameInstructions(); }
            return _SingleGameInstructions;
        }
        public static string MultiGame()
        {
            if (_MultiGameInstructions == null)
            { CreateMultiGameInstructions(); }
            return _MultiGameInstructions;
        }
        public static List<string[]> SingleGameTopics()
        {
            if (_SingleGameTopics == null)
            { CreateSingleGameTopics(); }
            return _SingleGameTopics;
        }
        public static List<string[]> MultiGameTopics()
        {
            if (_MultiGameTopics == null)
            { CreateMultiGameTopics(); }
            return _MultiGameTopics;
        }
        public static string About()
        {
            if (_About == null)
            { CreateAbout(); }
            return _About;
        }
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
        private static void SLine(string toAdd)
        { _SingleGameInstructions += toAdd + "\n"; }
        private static void MLine(string toAdd)
        { _MultiGameInstructions += toAdd + "\n"; }
        private static void ALine(string toAdd)
        { _About += toAdd + "\n"; }
        private static string[] topic(string topicName)
        { return topic(topicName, "Unknown"); }
        private static string[] topic(string topicName, string text)
        { return new string[] { topicName, text }; }
    }
}
