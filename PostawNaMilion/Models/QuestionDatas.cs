using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostawNaMilion.Models
{
    public class QuestionDatas
    {
        public static Player Player { get; set; }
        public static List<Question> QuestionsList { get; set; }

        public static List<Question> ActiveQuestions { get; set; }
        public static List<Answer> Answers { get; set; }
    }
}
