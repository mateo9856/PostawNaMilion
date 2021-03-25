using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostawNaMilion.Models
{
    public interface IQuestionData
    {
        void ResetDatas();
        void CreateQuestions();
        void GetQuestion(int questLvl);
        List<Answer> GetAnswers(Question question);
        Question SelectQuestion(int QuestionId);
        bool CheckAnswer(string answer, string[] answers, int[] bets);
    }
}
