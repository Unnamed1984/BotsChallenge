using BotChallenge.BLL.Models;
using BotChallenge.Models;
using BotChallenge.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BotChallenge.Controllers
{
    public class GameController : Controller
    {
        public static List<Game> Games { get; set; }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [NonAction]
        public Game FindFreeGame()
        {
            return null;
        }

        [HttpGet]
        public ActionResult Waiting()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Game()
        {
            String login = Request.Cookies.Get("Login").Value;
            GameState gState = GameManager.GetGameStateForPlayer(login);
            return View(gState);
        }
    }
}