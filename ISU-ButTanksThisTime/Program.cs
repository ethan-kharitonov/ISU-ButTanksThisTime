// Author        : Ethan Kharitonov
// File Name     : Program.cs
// Project Name  : ISU-ButTanksThisTime
// Created Date  : 12-19-2019
// Modified Date : 01-19-2020
// Description   : The system entry point to the game.
using System;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// The system entry point to the game.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            using (var game = new TankGame())
            {
                game.Run();
            }
        }
    }
}