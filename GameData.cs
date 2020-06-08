using System;
using System.Collections.Generic;

using MillionaireGameQuestions;
using MillionaireGameSettings;

namespace MillionaireGameData
{
    public class GameData{
        public const string cLifeline = "L";
        public int gamesPlayed = 0;
        private int gamesWon = 0;
        private decimal moneyWon = 0;
        private List<GameQuestion> questions = new List<GameQuestion>(0) {};
        public GameSettings settings = new GameSettings();

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

        public void AddGameQuestion(char pCorrectAnswer, 
                                    string pAnswerA,string pAnswerB, 
                                    string pAnswerC, string pAnswerD, 
                                    string pQuestion, 
                                    int pDifficulty = 0, string pPostQuestionTrivia = ""){
            
            questions.Add(new GameQuestion(){
                question = pQuestion,
                answerA = pAnswerA,
                answerB = pAnswerB,
                answerC = pAnswerC,
                answerD = pAnswerD,
                difficulty = pDifficulty,
                postQuestionTrivia = pPostQuestionTrivia
            });
            
            questions[questions.Count-1].SetAnswer(pCorrectAnswer);
        }

        public void ResetGameQuestions(){
            foreach (GameQuestion questionEntry in questions){
                questionEntry.questionAlreadyAsked = false;
            }
        }

        public void GetGameQuestion(ref string pQuestion, 
                                    ref string pAnswerA, ref string pAnswerB, 
                                    ref string pAnswerC, ref string pAnswerD, 
                                    ref int pDifficulty){
            int questionCount = questions.Count;
            if(questionCount > 0){
                foreach (GameQuestion questionEntry in questions){
                    if(!questionEntry.questionAlreadyAsked){
                        pQuestion = questionEntry.question;
                        pAnswerA = questionEntry.answerA;
                        pAnswerB = questionEntry.answerB;
                        pAnswerC = questionEntry.answerC;
                        pAnswerD = questionEntry.answerD;
                        pDifficulty = questionEntry.difficulty;

                        questionEntry.questionAlreadyAsked = true;
                        break;
                    }
                }
            }
            else{
                Console.WriteLine("ERROR: No game questions exist");
            }
        }

        public void SetDefaultQuestions(){
            //Question 1
            AddGameQuestion('c', "Edward", "Ernest", "Entertainment", "Extra", "What does the “E” in Chuck E. Cheese stand for?",  1);
            //Question 2
            AddGameQuestion('a', "Robot Chicken", "Space Ghost", "The Boondocks", "Samurai Jack", "In the opening of this stop motion Adult Swim show, a mad scientist breathes life into a recently killed creature", 2);
            //Question 3
            AddGameQuestion('d', "Kermit the Frog", "Rowlf the Dog", "Scooter", "Gonzo", "On \"The Muppet Show\" the love of this Muppet's life was Camilla the chicken", 3);
            //Question 4
            AddGameQuestion('b', "Yom Kippur", "Rosh Hashanah", "Kwanza", "Hanukkah", "What is the name for the Jewish New Year?", 4);
            //Question 5
            AddGameQuestion('a', "the Rhine", "the Yangtze", "the Ganges", "the Volga", "The Aare River of northern Switzerland joins this river at the German border", 5);
            //Question 6
            AddGameQuestion('c', "6", "7", "0", "13", "How many blue stripes are there on the U.S. flag?", 6);
            //Question 7
            AddGameQuestion('c', "Dragon", "Rabbit", "Hummingbird", "Dog", "Which animal does not appear in the Chinese zodiac?", 7);
            //Question 8
            AddGameQuestion('d', "Dooms", "Dark", "Denmark", "Dunkirk", "What does the “D” in “D-Day” stand for?", 8);
            //Question 9
            AddGameQuestion('b', "The Marauder", "The Black Pearl", "The Black Python", "The Slytherin", "In Pirates of the Caribbean, what was the name of Captain Jack Sparrow’s ship?", 9);
            //Question 10
            AddGameQuestion('a', "Brazil", "China", "Ireland", "Italy", "Which country held the 2016 Summer Olympics?", 10);
            //Question 11
            AddGameQuestion('c', "A bag of lemons", "A handful of roses", "A box of chocolates", "A lollipop", "According to Forrest Gump, \"life was like…\"", 11);
            //Question 12
            AddGameQuestion('d', "Zinc", "Hydrogen", "Fluorine", "Iron", "Fe is the chemical symbol for…", 12);
            //Question 13
            AddGameQuestion('c', "English", "Arabic", "Chinese", "Spanish", "What language is the most spoken worldwide?", 13);
            //Question 14
            AddGameQuestion('d', "Twitter", "Facebook", "YouTube", "Myspace", "Which social media platform came out in 2003?", 14);
            //Question 15
            AddGameQuestion('b', "Hamlet", "Macbeth", "Romeo & Juliet", "Othello", "What is Shakespeare’s shortest tragedy?", 15);
        }
    }
}