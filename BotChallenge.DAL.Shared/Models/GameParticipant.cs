using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.DAL.Shared.Models
{
    public class GameParticipant
    {
        public string GameParticipantId { get; set; }
        public string GameId { get; set; }
        public string UserId { get; set; }
        public bool? IsWinner { get; set; }
        public string UserCode { get; set; }
    }
}
