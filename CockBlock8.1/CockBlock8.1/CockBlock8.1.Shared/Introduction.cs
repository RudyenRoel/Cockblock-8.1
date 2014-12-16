using System;
using System.Collections.Generic;
using System.Text;

namespace CockBlock8._1
{
    public class Introduction
    {
        private static string _SingleGame = null;
        private static string _MultiGame = null;
        public static string SingleGame()
        {
            if (_SingleGame == null)
            {
                _SingleGame = "";
                SLine("Here are the instructions for the single game");
                SLine("You play it with Two teams, one on the top, and one on the bottem of the screen");
            }
            return _SingleGame;
        }
        public static string MultiGame()
        {
            if (_MultiGame == null)
            {
                _MultiGame = "";
                MLine("Here are the instructions for the multi game");
            }
            return _MultiGame;
        }
        private static void SLine(string toAd)
        { _SingleGame += toAd + "\n"; }
        private static void MLine(string toAd)
        { _MultiGame += toAd + "\n"; }
        public static List<string[]> SingleGameTopics()
        {
            List<String[]> topics = new List<string[]>();
            topics.Add(topic("Where can i find the 'many asked questions'?", "You found it!"));
            topics.Add(topic("Bullshit", "Bullshit, Bullshit, bla bla bla"));
            topics.Add(topic("Who did this application can to its idea?", "The idea for the application has started at school. More about this, you'll find in the 'about'-page."));
            topics.Add(topic("Ranom", "Did you know that the skin of a showbear is black?!"));
            topics.Add(topic("Easter egg", "There are some easter eggs in this game. To find the easter eggs, search on the internet"));
            topics.Add(topic("Easter egg", "Did you search on the Internet? If not, go do it now!"));
            topics.Add(topic("Easter egg", "Did you look it up yet? Ow you did? Did you find any this? Offcourse not! 3:)"));
            return topics;
        }
        public static List<string[]> MultiGameTopics()
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
            return topics;
        }
        private static string[] topic(string topicName)
        { return topic(topicName, "Unknown"); }
        private static string[] topic(string topicName, string text)
        { return new string[] { topicName, text }; }
    }
}
