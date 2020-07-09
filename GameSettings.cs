// <copyright file="GameSettings.cs" company="ejuel.net">
//     ejuel.net All rights reserved.
// </copyright>
// <author>Me</author>
namespace MillionaireGameSettings
{
    public class GameSettings
    {
        public int minQuestionLength = 10;
        public int maxQuestionLength = 255;
        public int minAnswerLength = 1;
        public int maxAnswerLength = 100;
        public int maxTriviaLength = 255;
        public int minDifficulty = 0;
        public int maxDifficulty = 100;

        //Average word length is 4.7 letters (5.7 if including spaces)
        //Average reading speed is 200 words per minute
        // ( (stringLength)/5.7) * (1000ms) * (60seconds)/(200wpm)
        // simplify math to be:
        // stringLength * (1000*60)/(5.7*200)
        // = stringLength * (60000/1140)
        // = stringLength * 53

        // Reading Speed time delay based on string length so longer questions give user time to read
        public int readingSpeed = 53; //milliseconds per char

        public int delayBetweenStatements = 1500;
    }
}