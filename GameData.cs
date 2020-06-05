using System;
using System.Collections.Generic;

using MillionaireGameQuestions;

namespace MillionaireGameData
{
    public class GameData{
        public const string cLifeline = "L";
        public int gamesPlayed = 0;
        private int gamesWon = 0;
        private decimal moneyWon = 0;
        private List<GameQuestion> questions = new List<GameQuestion>(0) {};

        public void EndGame(bool pWon = false, decimal pMoneyWon = 0){
            //made both parameters optional so in lose scenario can just call EndGame()
            gamesPlayed++;
            if(pWon){
                gamesWon++;
                moneyWon += pMoneyWon;
            }
        }
        public void GameSummary(){
            Console.WriteLine("You've won {0} of {1} games and walked away with ${2}.", gamesWon, gamesPlayed, moneyWon);
        }

        public void AddGameQuestion(string pQuestion, 
                                    string pAnswerA,string pAnswerB, 
                                    string pAnswerC, string pAnswerD, 
                                    char pCorrectAnswer, int pDifficulty = 0){
            questions.Add(new GameQuestion{
                question = pQuestion,
                answerA = pAnswerA,
                answerB = pAnswerB,
                answerC = pAnswerC,
                answerD = pAnswerD,
                difficulty = pDifficulty
            });
            questions[questions.Count].SetAnswer(pCorrectAnswer);
        }

        public void GetRandomGameQuestion(ref int gameQuestion, 
                                    ref string pAnswerA, ref string pAnswerB, 
                                    ref string pAnswerC, ref string pAnswerD, 
                                    ref int pDifficulty){
            if(questions.Count > 0){

            }
        }
    }
}