using MillionaireGameData;
using MillionaireGameFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MillionaireTerminal
{
    internal class ProgramTerminal
    {
        public void TerminalMain()
        {
            //ToDo: Consider putting all this in a navigation object with states

            GameData objGameData = new GameData();

            // Load default game questions 
            objGameData.SetDefaultQuestions();

            string userInput = string.Empty;

            while (userInput != "3")
            {
                Console.Clear();
                
                Console.WriteLine("Who Wants to Be a Millionaire?\n");

                if (objGameData.gamesPlayed > 0)
                {
                    objGameData.GameSummary();
                }
                Console.WriteLine("Here are your choices: \n1. Start New Game\n2. Add Question\n3. Exit");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        playGame(ref objGameData);
                        break;
                    case "2":
                        //ToDo: Setting to add questions
                        Console.WriteLine("Who Wants to Be a Millionaire?\n");
                        AddQuestion(ref objGameData);
                        break;
                }
            }
        }

        private static void playGame(ref GameData pobjGameData)
        {
            //Currently run from terminal, if using with a GUI then replace Program.cs and add new class to GameFunctions.cs

            TerminalGameFunctions objGameFunctions = new TerminalGameFunctions();
            bool playGame = true;

            //int intQuestionNumber = 1;
            int intTimeBank = 0; //Some game modes add time bank to last question
                                 //ToDo: Add option for game modes (ex: 12 Question, 15 Question, 2010 US amendment, etc.)
            string userInput = string.Empty;
            int intTimeRemaining = 0;

            while (playGame)
            {

                //objGameFunctions.Countdown(10, ref userInput, GameData.cLifeline, ref intTimeRemaining);
                if (userInput.ToUpper() == GameData.cLifeline.ToUpper())
                {
                    Console.WriteLine("lifeline used");
                    pobjGameData.EndGame();
                }
                else
                {
                    intTimeBank += intTimeRemaining;
                    Console.WriteLine("Your answer was {0}", userInput);
                    pobjGameData.EndGame(true, 100);
                }

                Console.WriteLine("Play new game? (y/n)");
                string yesNo = Console.ReadLine().ToUpper();
                if (yesNo != "Y" && yesNo != "YES")
                {
                    playGame = false;
                }
            }
        }

        private static void AddQuestion(ref GameData pobjGameData)
        {
            string question = "";
            int difficulty = 0;
            string answerA = "";
            string answerB = "";
            string answerC = "";
            string answerD = "";
            char correctAnswer = ' ';
            string postQuestionTrivia = "";

            bool addQuestion = true;
            string userInput = "";

            while (addQuestion)
            {
                //Enter Question
                while (userInput.Length < pobjGameData.settings.minQuestionLength || userInput.Length > pobjGameData.settings.maxQuestionLength)
                {
                    Console.WriteLine("Please enter a question (10 character minimum):"); //ToDo: if storing questi
                    userInput = Console.ReadLine().ToUpper();
                    if (userInput[userInput.Length - 1] != '?')
                    {
                        Console.WriteLine("Your question must end with a questionmark.");
                        userInput = "";
                    }
                    else if (userInput.Length > pobjGameData.settings.maxQuestionLength)
                    {
                        Console.WriteLine("Question length cannot be longer than {0}, you entered a length of {1}",
                                            pobjGameData.settings.maxQuestionLength, userInput.Length);
                    }
                }
                question = userInput;

                userInput = "";
                List<char> answerOptions = new List<char> { 'A', 'B', 'C', 'D' };
                foreach (char option in answerOptions)
                {

                    while (userInput.Length < pobjGameData.settings.minAnswerLength || userInput.Length > pobjGameData.settings.maxAnswerLength)
                    {
                        Console.WriteLine("Please enter option {0}: ", option);
                        userInput = Console.ReadLine().ToUpper();
                        if (userInput.Length < pobjGameData.settings.minAnswerLength || userInput.Length > pobjGameData.settings.maxAnswerLength)
                        {
                            Console.WriteLine("must be between {0} and {1} character long", pobjGameData.settings.minAnswerLength, pobjGameData.settings.maxAnswerLength);
                        }
                    }

                    switch (option)
                    {
                        case 'A':
                            answerA = userInput;
                            break;
                        case 'B':
                            answerB = userInput;
                            break;
                        case 'C':
                            answerC = userInput;
                            break;
                        case 'D':
                            answerD = userInput;
                            break;
                    }
                    userInput = "";
                }

                // ToDo:  ***** Finish adding questions process *****


                //Print Question with answers
                //Ask which answer is the correct answer

                //Optional: Ask question difficulty (1-100), else 0
                //Optional: Post question trivia

                //Populate 
                pobjGameData.AddGameQuestion(correctAnswer, answerA, answerB, answerC, answerD, question, difficulty, postQuestionTrivia);

                Console.WriteLine("Add another question? (y/n)");
                userInput = Console.ReadLine().ToUpper();
                if (userInput.ToUpper() != "Y" && userInput.ToUpper() != "YES")
                {
                    addQuestion = false;
                }
            }
        }
    }
}