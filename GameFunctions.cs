
using System;
using System.Threading; 

namespace MillionaireGameFunctions
{
    public class GameFunctions
    {
        public void Countdown(int pintCountFrom, ref string pstrUserAnswer, string pstrLifeline, ref int pintTimeRemaining){
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