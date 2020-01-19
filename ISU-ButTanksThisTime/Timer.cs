using Microsoft.Xna.Framework;

namespace ISU_ButTanksThisTime
{
    internal class Timer
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

        public void Reset()
        {
            TimeLeft = Interval;
        }

        private int TimeLeft { get; set; }

        private int Interval { get; }
    }
}