using System;

using System.Threading; 
using System.Globalization;

namespace WWtBaM
{
    class GameData{
        public const string cstrLifeline = "L";
        public decimal gcurMoneyWon = 0;
        public int gintGamesPlayed = 0;
        public int gintGamesWon = 0;
    }
        

    class Program
    {
        
        static void Main(string[] args)
        {
            GameData objGameData = new GameData();

            string fstrUserInput = System.String.Empty;

            while (fstrUserInput.ToUpper() != "3".ToString().ToUpper()) {
                Console.WriteLine("Welcome to Who Wants to Be a Millionaire?");
                Console.WriteLine("Here are your choices: \n1. Start New Game\n2. Add Question\n3. Exit");
                fstrUserInput = Console.ReadLine().ToString();
                
                switch(fstrUserInput){
                    case "1":
                        playGame(ref objGameData.gintGamesPlayed, ref objGameData.gintGamesWon, ref objGameData.gcurMoneyWon);
                        break;
                    case "2":
                        //ToDo: Setting to add questions
                        break;
                }
            }

        }

        static void playGame(ref int pintGamesPlayed, ref int pintGamesWon, ref decimal pcurMoneyWon){
            pintGamesPlayed++;
            //int intQuestionNumber = 1;
            int intTimeBank = 0; //Some game modes add time bank to last question
            //ToDo: Add option for game modes (ex: 12 Question, 15 Question, 2010 US amendment, etc.)
            string fstrUserInput = System.String.Empty; 
            int intTimeRemaining = 0; 
            Countdown(10, ref fstrUserInput, GameData.cstrLifeline, ref intTimeRemaining);
            if(fstrUserInput == GameData.cstrLifeline){
                //lifeline used
            }
            else{
                intTimeBank += intTimeRemaining;
                Console.Clear();
                Console.WriteLine("Your answer was {0}", fstrUserInput);
            }

            pintGamesWon++;
        }

        static void Countdown(int pintCountFrom, ref string pstrUserAnswer, string pstrLifeline, ref int pintTimeRemaining){
            int counter = pintCountFrom;

            //Using threading to get user input while countdown timer is running (this took a couple hours to get right)
            string strInput = null;
            Thread ListenForInput = new Thread(new ThreadStart(() => {strInput = Console.ReadLine();} ));
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
                    if(strInput.ToUpper() == pstrLifeline.ToUpper()){
                        //lifeline character used, handle outside Countdown function
                        pstrUserAnswer = strInput;
                        break;
                    }

                    Console.WriteLine("you've entered '{0}' is that your final answer? (y/n)", strInput);
                    string strYesNo = Console.ReadLine().ToUpper();
                    if(strYesNo == "Y" || strYesNo == "YES"){
                        pstrUserAnswer = strInput;
                        break;
                    }
                    else{
                        ListenForInput = new Thread(new ThreadStart(() => {strInput = Console.ReadLine();} ));
                        ListenForInput.Start();
                    }
                }
            }
            Console.Clear();
        }
    }
}
