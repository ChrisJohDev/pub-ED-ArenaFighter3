using System;
using System.Collections.Generic;
using System.Text;

namespace ArenaFighter3
{
    public class Battle
    {
        Character userFighter;
        Character computerFighter;
        private Round.RoundResult _result = new Round.RoundResult();
        private Random _generator = new Random();
        private ConsoleKeyInfo _key;
        private List<Round.RoundResult> _roundList = new List<Round.RoundResult>();
        private bool ok = true, fightersOK = true;
        private string _name;
        public void NewBattle()
        {
            this._name = GetName();
            Round.ResetNumberOfRounds();
            if (this._name == "") this._name = "Stoned Dummy";
            userFighter = new Character(this._name);
            while (ok)
            {
                computerFighter = new Character();
                PrintInitialFighterData();
                Round.LogWriter.WriteLog("\t\tBattle Start\r");
                while (fightersOK)
                {
                    this._result = Round.RollTheDice();
                    this._roundList.Add(this._result);
                    PrintResultToScreen(this._result);

                    if(!(userFighter.Health > 0 && computerFighter.Health > 0))
                    {
                        this.fightersOK = false;
                    }
                    else
                    {
                        Console.WriteLine("\n\nEnter any key to continue.");
                        Console.ReadKey(false);
                    }
                }

                if (userFighter.Health > 0)
                {
                    userFighter.AddBattleWon();
                    Console.Write("New Game Y/N? ");
                    this._key = Console.ReadKey();
                    if (this._key.KeyChar.ToString().ToLower() == "n")
                    {
                        this.ok = false;
                    }
                    else
                    {
                        fightersOK = true;
                    }
                }
                else
                {
                    this.ok = false;
                    Round.ResetNumberOfRounds();
                }
                PrintBattle();
                Round.LogWriter.WriteRoundLog();
                PrintUserScore();
                Round.LogWriter.WriteLog("\t\tBattle End\n\r");
            }


        }

        private void PrintUserScore()
        {
            int score = 0;
            if (this.userFighter.Health > 0)
            {
                score = 3 * (this.userFighter.BattlesWon + this.userFighter.Health + this.userFighter.Strength);
            }
            else
            {
                score = this.userFighter.BattlesWon + this.userFighter.Health + this.userFighter.Strength;
            }

            if (score < 0) score = 0;
            Console.WriteLine("\n\n-------------\n" + this.userFighter.Name + "'s score is: " + score.ToString());
            Round.LogWriter.WriteLog("\rThe score for " + this.userFighter.Name + " is: " + score.ToString() + "\r");

        }

        public string GetName()
        {
            Console.Write("\n\nEnter your fighter's name: ");
            return Console.ReadLine();
        }

        public void PrintInitialFighterData()
        {
            Console.WriteLine("\n\nUser:\t\t" + this.userFighter.Name + "\nHealth:\t\t" + this.userFighter.Health + "\nStrength:\t" + this.userFighter.Strength);
            Console.WriteLine("\n\nComputer:\t" + this.computerFighter.Name + "\nHealth:\t\t" + this.computerFighter.Health + "\nStrength:\t" + this.computerFighter.Strength);
        }
        public void PrintResultToScreen(Round.RoundResult data)
        {
            string tmp;
            double multiplier;

            if (data.Winner == FightWinnerResult.User)
            {
                // Possible selections of how to inflict damage with what weapon and what to do with the amount won.
                Console.Write("\n\nHow many percent of your Strength do you want to use? ");
                tmp = Console.ReadLine();
                if (tmp == "" || tmp == "100")
                {
                    multiplier = 1;
                }
                else
                {
                    multiplier = Convert.ToDouble(tmp) / 100;
                }
                this.computerFighter.RecievedDamage(Convert.ToInt32(Math.Round(Convert.ToDouble(data.DamageToInflict) * multiplier * this.userFighter.Strength)));
                this.userFighter.UsedStrength(Convert.ToInt32(Math.Round(multiplier * Convert.ToDouble(userFighter.Strength) / 5)));
            }
            else if (data.Winner == FightWinnerResult.Computer)
            {
                // Possible selections of how to inflict damage with what weapon and what to do with the amount won.
                tmp = _generator.Next(1, 100).ToString();
                if (tmp == "" || tmp == "100")
                {
                    multiplier = 1.0;
                }
                else
                {
                    multiplier = Convert.ToDouble(tmp) / 100;
                }
                this.userFighter.RecievedDamage(Convert.ToInt32(Math.Round(Convert.ToDouble(this._result.DamageToInflict) * multiplier * this.computerFighter.Strength)));
                this.computerFighter.UsedStrength(Convert.ToInt32(Math.Round(multiplier * Convert.ToDouble(this.computerFighter.Strength) / 5)));

            }
            tmp = "\n\n--------------------\nThe Winner of round " + this._result.RoundNumber.ToString() + " is: ";
            if (data.Winner == FightWinnerResult.User)
            {
                tmp += this.userFighter.Name;
            }
            else
            {
                tmp += this.computerFighter.Name;
            }
            Console.WriteLine(tmp);
            Console.WriteLine("\n\nUser:\t\t" + this.userFighter.Name + "\nHealth:\t\t" + this.userFighter.Health + "\nStrength:\t" + this.userFighter.Strength);
            Console.WriteLine("\n\nComputer:\t" + this.computerFighter.Name + "\nHealth:\t\t" + this.computerFighter.Health + "\nStrength:\t" + this.computerFighter.Strength);
        }

        public void PrintBattle()
        {
            string winnerName;

            Console.WriteLine("\n\n------------------------------\n\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            

            if (userFighter.Health < 1)
            {
                Console.WriteLine("The winner is " + this.computerFighter.Name + "\n\n");
            }
            else
            {
                Console.WriteLine("The winner is " + userFighter.Name + "\n\n");
            }

            
            Console.WriteLine("Round\tWinner\t\t" + userFighter.Name + "\t\t" + this.computerFighter.Name);
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (Round.RoundResult round in this._roundList)
            {
                if (round.Winner == FightWinnerResult.User)
                {
                    winnerName = userFighter.Name;
                }
                else
                {
                    winnerName = this.computerFighter.Name;
                }
                Console.WriteLine("\n" + round.RoundNumber + "\t" + winnerName + "\t\t" + round.UserScore + "\t\t" + round.ComputerScore);
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
