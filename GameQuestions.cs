
using System;
namespace MillionaireGameQuestions
{
    public class GameQuestion {
        //public char correctAnswer {get; set;}
        private char correctAnswer = ' ';
        public string question {get; set;}
        public int difficulty {get; set;}
        public string answerA {get; set;}
        public string answerB {get; set;}
        public string answerC {get; set;}
        public string answerD {get; set;}
        public string postQuestionTrivia {get; set;}
        public bool questionAlreadyAsked = false; 
        //ToDo: Create function to hide questions already asked
        //ToDo: Consider adding function to replace an answer/correctAnswer, or should we just delete a question and re-add it?
        //ToDo: Create function to shuffle possible answers (in case users are always adding setting letter answer)
        public void SetAnswer(char pCorrectAnswer){
            
            if(correctAnswer != ' '){
                correctAnswer = pCorrectAnswer;
            }
        }
        public bool IsAnswerCorrect(char pUserGuess){
            if(pUserGuess == correctAnswer){
                return true;
            }
            return false;
        }

    }
}