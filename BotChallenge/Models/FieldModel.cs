using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.Models
{
    public class FieldModel
    {
        public String MapPath { get; set; }
        public String MapName { get; set; }
        public String MapDescription { get; set; }
        public Dictionary<String, List<BotModel>> Bots { get; set;}
    }
}
