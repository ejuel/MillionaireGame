using System;

using System.Threading; 
using System.Globalization;
using System.Collections.Generic;


namespace WWtBaM
{
    class GameQuestion {
        public int difficulty {get; set;}
        public string question {get; set;}
        public char correctAnswer {get; set;}
        public string answerA {get; set;}
        public string answerB {get; set;}
        public string answerC {get; set;}
        public string answerD {get; set;}


    }
    class GameData{
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
                difficulty = pDifficulty, 
                question = pQuestion,
                
            });
        }
    }
        

    class Program
    {
        
        static void Main(string[] args)
        {
            GameData objGameData = new GameData();

            string userInput = System.String.Empty;

            while (userInput.ToUpper() != "3".ToString().ToUpper()) {
                Console.Clear();
                Console.WriteLine("Who Wants to Be a Millionaire?\n");
                if(objGameData.gamesPlayed > 0){
                    objGameData.GameSummary();
                }
                Console.WriteLine("Here are your choices: \n1. Start New Game\n2. Add Question\n3. Exit");
                userInput = Console.ReadLine().ToString();
                
                switch(userInput){
                    case "1":
                        playGame(ref objGameData);
                        break;
                    case "2":
                        //ToDo: Setting to add questions
                        break;
                }
            }

        }

        static void playGame(ref GameData pobjGameData){

            Boolean playGame = true;

            //int intQuestionNumber = 1;
            int intTimeBank = 0; //Some game modes add time bank to last question
            //ToDo: Add option for game modes (ex: 12 Question, 15 Question, 2010 US amendment, etc.)
            string userInput = System.String.Empty; 
            int intTimeRemaining = 0; 

            while(playGame){

                Countdown(10, ref userInput, GameData.cLifeline, ref intTimeRemaining);
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

        static void Countdown(int pintCountFrom, ref string pstrUserAnswer, string pstrLifeline, ref int pintTimeRemaining){
            int counter = pintCountFrom;

            //Using threading to get user input while countdown timer is running (this took a couple hours to get right)
            string userInput = null;
            Thread ListenForInput = new Thread(new ThreadStart(() => {userInput = Console.ReadLine();} ));
            ListenForInput.Start();
            
            while(counter > 0){

                //Clear bottom row between numbers
                Console.SetCursorPosition(0,Console.CursorTop);
                Console.Write(new String(' ', Console.BufferWidth));
                Console.SetCursorPosition(0,Console.CursorTop-1);

                //Custom messages
                if(counter==7){
                    Console.WriteLine("only 7 seconds left");
                }

                Console.Write(counter--);
                Thread.Sleep(1000);

                if(!ListenForInput.IsAlive){
                    if(userInput.ToUpper() == pstrLifeline.ToUpper()){
                        //lifeline character used, handle outside Countdown function
                        pstrUserAnswer = userInput;
                        break;
                    }

                    Console.WriteLine("you've entered '{0}' is that your final answer? (y/n)", userInput);
                    string yesNo = Console.ReadLine().ToUpper();
                    if(yesNo == "Y" || yesNo == "YES"){
                        pstrUserAnswer = userInput;
                        break;
                    }
                    else{
                        ListenForInput = new Thread(new ThreadStart(() => {userInput = Console.ReadLine();} ));
                        ListenForInput.Start();
                    }
                }
            }
            Console.Clear();
        }
    }
}
