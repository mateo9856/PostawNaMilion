using PostawNaMilion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostawNaMilion.ViewModels
{
    public class AnswerViewModel
    {
        public Player Player { get; set; }
        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }
        public static int ID { get; set; }
        public static bool isValidate { get; set; } = true;
        
    }
}
