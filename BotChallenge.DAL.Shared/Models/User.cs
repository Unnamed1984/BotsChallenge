using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.DAL.Shared.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
    }
}
