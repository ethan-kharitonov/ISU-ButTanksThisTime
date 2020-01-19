// ***********************************************************************
// Assembly         : ISU-ButTanksThisTime
// Author           : Ethan Kharitonov
// Created          : 01-16-2020
//
// Last Modified By : Ethan Kharitonov
// Last Modified On : 01-19-2020
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Class Timer.
    /// </summary>
    internal class Timer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class.
        /// </summary>
        /// <param name="intervalMilliseconds">The interval milliseconds.</param>
        public Timer(int intervalMilliseconds)
        {
            Interval = intervalMilliseconds;
            TimeLeft = 0;
        }

        /// <summary>
        /// Determines whether [is time up] [the specified game time].
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <returns><c>true</c> if [is time up] [the specified game time]; otherwise, <c>false</c>.</returns>
        public bool IsTimeUp(GameTime gameTime)
        {
            TimeLeft -= gameTime.ElapsedGameTime.Milliseconds;
            return TimeLeft <= 0;
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            TimeLeft = Interval;
        }

        /// <summary>
        /// Gets or sets the time left.
        /// </summary>
        /// <value>The time left.</value>
        private int TimeLeft { get; set; }

        /// <summary>
        /// Gets the interval.
        /// </summary>
        /// <value>The interval.</value>
        private int Interval { get; }
    }
}