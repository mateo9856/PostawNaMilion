using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostawNaMilion.Models
{
    public class Player
    {
        [Required(ErrorMessage = "Enter your surname...")]
        [StringLength(50)]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Enter your name...")]
        [StringLength(50)]
        public string Name { get; set; }
        public int Points { get; set; }
        public static int Amount { get; set; } = 1000000;
        public static int AnswerNumber = 1;
        public bool IsUsedMoreTime { get; set; } = false;
    }
}
