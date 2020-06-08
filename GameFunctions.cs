
using System;
using System.Threading; 

namespace MillionaireGameFunctions
{
    public class GameFunctions
    {
        public void Countdown(int pCountFrom, ref string pUserAnswer, string pLifeline, ref int pTimeRemaining){
            int counter = pCountFrom;

            //Using threading to get user input while countdown timer is running (this took a while to get right)
            string userInput = null;
            Thread ListenForInput = new Thread(new ThreadStart(() => {userInput = Console.ReadLine();} ));
            ListenForInput.Start();
            
            while(counter > 0){

                //Clear bottom row between countdown numbers
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
                    if(userInput.ToUpper() == pLifeline.ToUpper()){
                        //lifeline character used, handle outside Countdown function
                        pUserAnswer = userInput;
                        break;
                    }

                    Console.WriteLine("you've entered '{0}' is that your final answer? (y/n)", userInput);
                    string yesNo = Console.ReadLine().ToUpper();
                    if(yesNo == "Y" || yesNo == "YES"){
                        pUserAnswer = userInput;
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
        public void SpinningWheel(ref bool pKeepSpinning, double pDelaySpinningWheelSeconds, int pLoadingLocationLeft, int pLoadingLocationTop){
            /*
                Created this while toying with countdown and threads
            //SpinningWheel() is an alternative to printing console.log("Loading...") if a function could take a long time to complete
            //Example usage of spinning wheel:
                bool runSpinningWheel = true;
                Thread spinningWheelThread = new Thread(new ThreadStart(() => {objGameFunctions.SpinningWheel(ref runSpinningWheel, 0.5 ,Console.CursorLeft-1,Console.CursorTop);}));
                //Note: Because of 0.5 parameter, if YourCustomFunction completes in less than 0.5 seconds then spinning wheel will not occur
                spinningWheelThread.Start();
                YourCustomFunction(); 
                runSpinningWheel = false;
            */

            //ToDo: Add option to display percent complete if several functions will be called, could gradually update display as:
            //      |----------00%----------| [|]
            //      |#####-----25%----------| [/]
            //      |##########95%#########-| [-]
            
            Thread.Sleep(Convert.ToInt32(1000 * pDelaySpinningWheelSeconds)); //in case task completes before loading window is required   
            int originalCursorPositionLeft;
            int originalCursorPositionTop;
            char loadingState = '|';

            while(pKeepSpinning){
                switch(loadingState){
                    case '|':
                        loadingState = '/';
                        break;
                    case '/':
                        loadingState = '-';
                        break;
                    case '-':
                        loadingState = '|';
                        break;
                }
                //Save current cursor position before updating loading wheel
                originalCursorPositionLeft = Console.CursorLeft;
                originalCursorPositionTop = Console.CursorTop;
                Console.SetCursorPosition(pLoadingLocationLeft,pLoadingLocationTop);
                Console.Write(loadingState);
                Console.SetCursorPosition(originalCursorPositionLeft,originalCursorPositionTop);
                Thread.Sleep(500);   
            }
        }
    }
}