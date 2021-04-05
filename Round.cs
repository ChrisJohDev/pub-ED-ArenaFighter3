using System;
using System.Collections.Generic;
using System.Text;

namespace ArenaFighter3
{
    public static class Round
    {
        private static int _numberOfRounds = 0;
        private static Random _generator = new Random();
        private static RoundResult _result = new RoundResult();
        private static Logger _logWriter = new Logger();


        public static int NumberOfRounds
        {
            get { return _numberOfRounds; }
        }
        public static Logger RoundsLogger
        {
            get { return _logWriter; }
        }

        public static RoundResult RollTheDice()
        {
            do
            {
                _result.UserScore = _generator.Next(1, 6);
                _result.ComputerScore = _generator.Next(1, 6);
            }
            while (_result.UserScore == _result.ComputerScore);

            _numberOfRounds++;
            _result.RoundNumber = _numberOfRounds;

            // Write log entry!
            _logWriter.LogRound(_result);

            return _result;
        }

        public static void ResetNumberOfRounds()
        {
            _numberOfRounds = 0;
        }

        public static Logger LogWriter
        {
            get { return _logWriter; }
        }

        public struct RoundResult
        {

            public int UserScore { get; set; }
            public int ComputerScore { get; set; }
            public int RoundNumber { get; set; }

            public FightWinnerResult Winner
            {
                get
                {
                    if (ComputerScore > UserScore)
                    {
                        return FightWinnerResult.Computer;
                    }
                    else
                    {
                        return FightWinnerResult.User;
                    }
                }
            }
            public int DamageToInflict
            {
                get
                {
                    return Math.Abs(UserScore - ComputerScore);
                }
            }
        }
    }

    public enum FightWinnerResult
    {
        User,
        Computer
    }
}
