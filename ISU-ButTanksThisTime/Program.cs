using System;

namespace ISU_ButTanksThisTime
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        public static bool IsDesignTime { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //IsDesignTime = args.Length > 0 && args[1] == "design";

            using (var game = new Game1())
                game.Run();
        }
    }
}