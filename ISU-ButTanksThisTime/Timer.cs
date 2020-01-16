using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_ButTanksThisTime
{
    class Timer
    {
        public Timer(int intervalMilliseconds)
        {
            Interval = intervalMilliseconds;
            TimeLeft = 0;
        }

        public bool IsTimeUp(GameTime gameTime)
        {
            TimeLeft -= gameTime.ElapsedGameTime.Milliseconds;

            return TimeLeft <= 0;
        }

        public void Reset() => TimeLeft = Interval;

        public int TimeLeft { get; private set; }

        public int Interval { get; set; }
    }
}
