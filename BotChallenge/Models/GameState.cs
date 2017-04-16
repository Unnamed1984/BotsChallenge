using AutoMapper;
using BotChallenge.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Models
{
    public class GameState
    {
        private IMapper _mapper;

        public GameState()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Bot, BotModel>();
                cfg.CreateMap<BotModel, Bot>();
            });

            _mapper = config.CreateMapper();
        }

        public GameState(Game game, String login): this()
        {
            this.Players = game.Players.Select(p => p.Name).ToList();

            var bots = new Dictionary<string, List<BotModel>>();
            foreach (var bot in game.Field.Bots)
            {
                bots.Add(bot.Key, _mapper.Map<List<BotModel>>(bot.Value));
            }

            this.FieldState = new FieldModel()
            {
                MapDescription = game.Field.MapDescription,
                MapName = game.Field.MapName,
                MapPath = game.Field.MapPath,
                Bots = bots
            };
            this.CurrentLogin = login;
        }

        public List<String> Players { get; set; }
        public FieldModel FieldState { get; set; }
        public String CurrentLogin { get; set; }
    }
}
