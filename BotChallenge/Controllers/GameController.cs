using AutoMapper;
using BotChallenge.BLL.Entities.DTO;
using BotChallenge.BLL.Logic;
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
        private BotsCompiler compiler = new BotsCompiler();
        private IMapper _mapper;

        public GameController()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CompilationResultDTO, CompilationResultModel>();
            });

            _mapper = config.CreateMapper();
        }

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

        [HttpPost]
        public JsonResult CompileBot(String code)
        {
            CompilationResultDTO result =  compiler.CompileBot(code);

            return Json(_mapper.Map<CompilationResultModel>(result));
        }

        [HttpPost]
        public JsonResult CompileBots(String[] code, int botsCount)
        {
            CompilationResultDTO result = compiler.CompileBotsCode(code, botsCount);

            return Json(_mapper.Map<CompilationResultModel>(result));
        }
    }
}