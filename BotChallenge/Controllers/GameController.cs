using BotChallenge.BLL.Models;
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
        public ActionResult RegisterForGame()
        {
            return View();
        }


        [HttpPost]
        public ActionResult RegisterForGame(Player p)
        {
            return null;
        }

        [NonAction]
        public Game FindFreeGame()
        {
            return null;
        }
    }
}