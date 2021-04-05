using System;

namespace ArenaFighter3
{
    class Program
    {
        private static Battle _battle = new Battle();
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Arena Fighter\n\nPress any key to start game!");
            Console.ReadKey(false);
            StartGame();
        }

        public static void StartGame()
        {
            DeleteLogs.DeleteAllLogs();
            _battle.NewBattle();
        }
    }
}
