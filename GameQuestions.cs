namespace MillionaireGameQuestions
{
    public class GameQuestion {
        public string question {get; set;}
        public int difficulty {get; set;}
        public char correctAnswer {get; set;}
        private char hiddenCorrectAnswer = ' ';
        public string answerA {get; set;}
        public string answerB {get; set;}
        public string answerC {get; set;}
        public string answerD {get; set;}
        public bool questionAlreadyAsked = false; 
        //ToDo: Create function to hide questions already asked

        public void HideAnswer(){
            if(correctAnswer != ' ' && hiddenCorrectAnswer == ' '){
                hiddenCorrectAnswer = correctAnswer;
                correctAnswer = ' ';
            }
        }
        public bool IsAnswerCorrect(char pUserGuess){
            if(pUserGuess == hiddenCorrectAnswer){
                return true;
            }
            return false;
        }


    }
}