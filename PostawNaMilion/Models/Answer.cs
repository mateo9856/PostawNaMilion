using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostawNaMilion.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerName { get; set; }
        public int QuestionId { get; set; }
        public int MoneyBet { get; set; } = 0;
        public static bool IsValidate { get; set; } = true;
    }
}
