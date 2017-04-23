using System;
using Bots.Models;

namespace Bots.Core
{
    public abstract class Bot
    {
        protected int x = 0;
        protected int y = 0;

        protected int maxX = 90;
        protected int maxY = 90;

        public int X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        public double distanceTo(Bot b2)
        {
            return 10;
        }

        public abstract Step NextStep(Field f);
    }
}