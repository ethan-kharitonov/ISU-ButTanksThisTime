﻿// Author        : Ethan Kharitonov
// File Name     : Timer.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : Simplifies measuring time intervals.
using Microsoft.Xna.Framework;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// Simplifies measuring time intervals.
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
            //updates the time that is left on the timer
            TimeLeft -= gameTime.ElapsedGameTime.Milliseconds;

            //returs wether the time is up
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