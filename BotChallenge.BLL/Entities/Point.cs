using BotChallenge.BLL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.BLL.Models
{
    public struct Point
    {
        public PointType Type { get; set; }
        public Int32? ObjectId { get; set; }
    }
}
