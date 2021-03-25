using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PostawNaMilion.Models;
using PostawNaMilion.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PostawNaMilion.Controllers
{
    public class HomeController : Controller
    {//In classes change connection string to your Windows Authentication in SQL Server
        private readonly IPlayerData _playerData;
        private readonly IQuestionData _questionData;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IPlayerData playerData, IQuestionData questionData)
        {
            _logger = logger;
            _playerData = playerData;
            _questionData = questionData;
        }

        public IActionResult Index()
        {
            _playerData.ResetData();
            return View();
        }

        public IActionResult AddToScore()
        {
            _playerData.AddToDatabase();
            _playerData.ResetData();
            return RedirectToAction("Index");
        }

        public IActionResult CreateAndStart()
        {
            return View(QuestionDatas.Player);
        }
        [HttpPost]
        public IActionResult Index(Player player)
        {
            
            if (ModelState.IsValid)
            {   
                QuestionDatas.Player = _playerData.CreateNewPlayer(player);
                _questionData.CreateQuestions();
                return RedirectToAction("CreateAndStart");
            }
            return View();
        }
        public IActionResult ScoreTable()
        {
            var Model = new ScoresViewModel {
                Players = _playerData.GetTopPlayersScore()
            };
            return View(Model);
        }
        public IActionResult EndWithoutScore()
        {
            _questionData.ResetDatas();
            _playerData.ResetData();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
