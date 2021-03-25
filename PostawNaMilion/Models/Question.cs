using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostawNaMilion.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionCategory { get; set; }
        public string QuestionName { get; set; }
        public string CorrectAnswer { get; set; }
        public int QuestionLevel { get; set; }
    }
}