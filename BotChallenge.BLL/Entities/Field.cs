using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.BLL.Models
{
    public class Field
    {
        public Field()
        {
            this.MapDescription = "Defeat all enemies test";
            this.MapName = "Test field";
            this.MapPath = "";
            this.Bots = new Dictionary<string, List<Bot>>();
        }

        public String MapPath { get; set; }
        public String MapName { get; set; }
        public String MapDescription { get; set; }
        public Dictionary<String, List<Bot>> Bots { get; set; }
    }
}
