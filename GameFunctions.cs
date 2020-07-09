// <copyright file="GameFunctions.cs" company="ejuel.net">
//     ejuel.net All rights reserved.
// </copyright>
// <author>Me</author>

namespace MillionaireGameFunctions
{
    using MillionaireGameData;
    using System;
    using System.Threading;

    public class TerminalGameFunctions
    {
        public void Countdown(int pCountFrom, ref string pUserAnswer, string pLifeline, ref int pTimeRemaining)
        {
            int counter = pCountFrom;

            //Using threading to get user input while countdown timer is running (this took a while to get right)
            string userInput = null;
            Thread ListenForInput = new Thread(new ThreadStart(() => { userInput = Console.ReadLine(); }));
            ListenForInput.Start();

            while (counter > 0)
            {
                //Clear bottom row between countdown numbers
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.BufferWidth));
                Console.SetCursorPosition(0, Console.CursorTop);

                //Custom messages based on time
                if (counter == 7)
                {
                    Console.WriteLine("only 7 seconds left...");
                }

                Console.Write(counter--);
                Thread.Sleep(1000);

                if (!ListenForInput.IsAlive)
                {
                    if (userInput.ToUpper() == pLifeline.ToUpper())
                    {
                        //lifeline character used, handle outside Countdown function
                        pUserAnswer = userInput;
                        break;
                    }

                    Console.WriteLine("you've entered '{0}' is that your final answer? (y/n)", userInput);
                    string yesNo = Console.ReadLine().ToUpper();
                    if (yesNo == "Y" || yesNo == "YES")
                    {
                        pUserAnswer = userInput;
                        break;
                    }
                    else
                    {
                        ListenForInput = new Thread(new ThreadStart(() => { userInput = Console.ReadLine(); }));
                        ListenForInput.Start();
                    }
                }
            }
            if (ListenForInput.IsAlive)
            {
                //Have to join thread since it's waiting for input
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.BufferWidth));
                Console.SetCursorPosition(0, Console.CursorTop);

                Console.WriteLine("You're walking away with...\n(press enter to continue)");
                ListenForInput.Join();
                pUserAnswer = "Walk";
            }
            Console.Clear();
        }

        public void SpinningWheel(ref bool pKeepSpinning, double pDelaySpinningWheelSeconds, int pLoadingLocationLeft, int pLoadingLocationTop, ref double pPercentComplete)
        {
            /* Created this while toying with countdown and threads

            //SpinningWheel() is an alternative to printing console.log("Loading...") if a function could take a long time to complete
            //Example usage of spinning wheel:
                bool runSpinningWheel = true;
                double percentage = -1;
                Thread spinningWheelThread = new Thread(new ThreadStart(() => {objGameFunctions.SpinningWheel(ref runSpinningWheel, 0.5 ,Console.CursorLeft-1,Console.CursorTop, ref percentage);}));
                //Note: Because of 0.5 parameter, if YourCustomFunction completes in less than 0.5 seconds then spinning wheel will not occur
                spinningWheelThread.Start();
                YourCustomFunction();
                runSpinningWheel = false;
            */
            Thread.Sleep(Convert.ToInt32(1000 * pDelaySpinningWheelSeconds)); //in case task completes before loading window is required

            int originalCursorPositionLeft;
            int originalCursorPositionTop;
            string loadingString = "|----------00%----------|";
            char loadingState = '|';
            double percentComplete = 0;

            while (pKeepSpinning && pPercentComplete < 100)
            {
                switch (loadingState)
                {
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

                if (pPercentComplete >= 0 && pPercentComplete <= 100)
                {
                    if (percentComplete < pPercentComplete)
                    {
                        //Need to update loadingString
                        percentComplete = pPercentComplete;
                        char[] loadingStringArray = loadingString.ToCharArray();

                        if (percentComplete < 10)
                        {
                            loadingStringArray[12] = percentComplete.ToString()[0];
                        }
                        else
                        {
                            loadingStringArray[11] = percentComplete.ToString()[0];
                            loadingStringArray[12] = percentComplete.ToString()[1];
                        }

                        for (int i = 1; i * 5 <= percentComplete; i++)
                        {
                            //Start at 1 to skip the first character of loadingString
                            if (i <= 10)
                            {
                                loadingStringArray[i] = '#';
                            }
                            else
                            {
                                //Skip over the "##%" section in middle
                                loadingStringArray[i + 3] = '#';
                            }
                        }
                        loadingString = new string(loadingStringArray);

                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, Console.CursorTop - 1);

                        Console.Write(loadingString + " [{0}]", loadingState);
                    }
                }
                else
                {
                    //Save current cursor position before updating loading wheel
                    originalCursorPositionLeft = Console.CursorLeft;
                    originalCursorPositionTop = Console.CursorTop;
                    Console.SetCursorPosition(pLoadingLocationLeft, pLoadingLocationTop);
                    Console.Write(loadingState);
                    Console.SetCursorPosition(originalCursorPositionLeft, originalCursorPositionTop);
                }
                Thread.Sleep(500); //Time between refreshes
            }
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine("Done...");
        }

        public void PrintQuestion(ref GameData pobjGameData, string pQuestion = "", string pOptionA = "", string pOptionB = "", string pOptionC = "", string pOptionD = "")
        {
            //ToDo: set up answer alignment to spread across screen
            // Idea A: Write both lines with blank answers, then go back and fill in
            Console.WriteLine(pQuestion);
            Thread.Sleep(pQuestion.Length * pobjGameData.settings.readingSpeed);

            Console.WriteLine("\nIs the answer: ");
            Console.WriteLine("<{0,25}><{0,25}>", "");
            Console.WriteLine("<{0,25}><{0,25}>", "");
            Thread.Sleep(pobjGameData.settings.delayBetweenStatements);

            Console.SetCursorPosition(1, Console.CursorTop - 2);
            Console.Write(" A: " + pOptionA);
            Thread.Sleep(pOptionA.Length * pobjGameData.settings.readingSpeed);
            Thread.Sleep(pobjGameData.settings.delayBetweenStatements);

            Console.SetCursorPosition(28, Console.CursorTop);
            Console.Write(" B: " + pOptionB + "\n");
            Thread.Sleep(pOptionB.Length * pobjGameData.settings.readingSpeed);
            Thread.Sleep(pobjGameData.settings.delayBetweenStatements);

            Console.SetCursorPosition(1, Console.CursorTop);
            Console.Write(" C: " + pOptionC);
            Thread.Sleep(pOptionC.Length * pobjGameData.settings.readingSpeed);
            Thread.Sleep(pobjGameData.settings.delayBetweenStatements);

            Console.SetCursorPosition(28, Console.CursorTop);
            Console.Write(" D: " + pOptionD + "\n");
            Thread.Sleep(pOptionD.Length * pobjGameData.settings.readingSpeed);
            Thread.Sleep(pobjGameData.settings.delayBetweenStatements);
        }
    }
}