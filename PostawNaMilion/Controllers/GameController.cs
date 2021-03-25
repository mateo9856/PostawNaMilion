using Microsoft.AspNetCore.Mvc;
using PostawNaMilion.Models;
using PostawNaMilion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostawNaMilion.Controllers
{
    public class GameController : Controller
    {
        private readonly IQuestionData _questionData;
        private static int QuestLvl = 0;
        public GameController(IQuestionData questionData)
        {
            _questionData = questionData;
        }
        public IActionResult QuestionChoice()
        {
            if(Player.AnswerNumber < 5)
            {
                QuestLvl = 4;
            }   else if(Player.AnswerNumber < 8 && Player.AnswerNumber >= 5)
            {
                QuestLvl = 3;
            } else
            {
                QuestLvl = 2;
            }
            _questionData.GetQuestion(QuestLvl);
            var Model = new QuestionViewModel
            { 
                ActiveQuestions = QuestionDatas.ActiveQuestions
            };
            Answer.IsValidate = true;
            return View(Model);
        }

        public IActionResult SelectedQuestion(int id)
        {
            var quest = _questionData.SelectQuestion(id);
            AnswerViewModel.ID = quest.QuestionId;
            var Model = new AnswerViewModel
            {
                Player = QuestionDatas.Player,
                Question = quest,
                Answers = _questionData.GetAnswers(quest),
                
            };
            if (quest == null)
            {
                return NotFound();
            }
            
            return View(Model);
        }

        public IActionResult CheckAnswer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckAnswer(string[] answer, int[] bet)
        {
            Answer.IsValidate = true;
            var getQuest = _questionData.SelectQuestion(AnswerViewModel.ID);
            int BetMoney = 0;
            foreach(var x in bet)
            {
                BetMoney += x;
            }
            if(BetMoney != Player.Amount)
            {
                Answer.IsValidate = false;
                return RedirectToAction("SelectedQuestion", new { id = AnswerViewModel.ID });
            }

            var checkAnswer = _questionData.CheckAnswer(getQuest.CorrectAnswer, answer, bet);

            if (checkAnswer == true)
            {
                if(Player.AnswerNumber == 8)
                { 
                    return RedirectToAction("FinalResult");
                } else
                {
                    Player.AnswerNumber++;
                    return View(getQuest);
                }
            }
            else
            {
                TempData["ans"] = getQuest.CorrectAnswer;
                QuestLvl = 0;
                _questionData.ResetDatas();
                return RedirectToAction("BadAnswer");
            }

            return Ok();
        }

        public IActionResult BadAnswer(string message)
        {
            return View();
        }
        public IActionResult FinalResult()
        {
            return View();
        }

    }
}
