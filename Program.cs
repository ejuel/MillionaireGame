using System;
using System.Threading; 
using System.Globalization;
using System.Collections.Generic;

using MillionaireGameData;
using MillionaireGameFunctions;

namespace MillionaireGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //ToDo: Consider putting all this in a navigation object with states
            GameData objGameData = new GameData();

            string userInput = System.String.Empty;

            while (userInput != "3") {
                Console.Clear();
                Console.WriteLine("Who Wants to Be a Millionaire?\n");
                if(objGameData.gamesPlayed > 0){
                    objGameData.GameSummary();
                }
                Console.WriteLine("Here are your choices: \n1. Start New Game\n2. Add Question\n3. Exit");
                userInput = Console.ReadLine();
                
                switch(userInput){
                    case "1":
                        playGame(ref objGameData);
                        break;
                    case "2":
                        //ToDo: Setting to add questions
                        Console.WriteLine("Who Wants to Be a Millionaire?\n");
                        break;
                }
            }

        }

        static void playGame(ref GameData pobjGameData){

            GameFunctions objGameFunctions = new GameFunctions();
            Boolean playGame = true;

            //int intQuestionNumber = 1;
            int intTimeBank = 0; //Some game modes add time bank to last question
            //ToDo: Add option for game modes (ex: 12 Question, 15 Question, 2010 US amendment, etc.)
            string userInput = System.String.Empty; 
            int intTimeRemaining = 0; 

            while(playGame){
                
                objGameFunctions.Countdown(10, ref userInput, GameData.cLifeline, ref intTimeRemaining);
                if(userInput.ToUpper() == GameData.cLifeline.ToUpper()){
                    Console.WriteLine("lifeline used");
                    pobjGameData.EndGame();
                }
                else{
                    intTimeBank += intTimeRemaining;
                    Console.WriteLine("Your answer was {0}", userInput);
                    pobjGameData.EndGame(true, 100);
                }

                Console.WriteLine("Play new game? (y/n)");
                string yesNo = Console.ReadLine().ToUpper();
                if(yesNo != "Y" && yesNo != "YES"){
                    playGame = false;
                }
            }
        }    
    }
}
