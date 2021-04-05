using System;
using System.Collections.Generic;
using System.Text;

namespace ArenaFighter3
{
	public class Character
	{
		private string _name = "Seymour Butt";
		private int _health, _strength, _initialHealth, _initialStrength, _battlesWon;
		private Random _randomGenerator = new Random();
		public Character()
		{
			this._health = this._randomGenerator.Next(10, 100);
			this._strength = this._randomGenerator.Next(5, 100);
			this._initialHealth = this._health;
			this._initialStrength = this._strength;
		}
		public Character(string userName) : this()
		{
			this._name = userName;
		}
		public string Name
		{
			get { return this._name; }
		}
		public int Health
		{
			get { return this._health; }
			set { this._health = value; }
		}
		public int InitialHealth
		{
			get { return this._initialHealth; }
		}
		public int Strength
		{
			get { return this._strength; }
			set { this._strength = value; }
		}
		public int InitialStrength
		{
			get { return this._initialStrength; }
		}
		public int BattlesWon
		{
			get { return this._battlesWon; }
		}

		public void RecievedDamage(int damage)
		{
			this._health = this._health - damage;
		}
		public void AddBattleWon()
		{
			this._battlesWon++;
		}

		public void UsedStrength(int used)
		{
			double tmp;
			this._strength = this._strength - used;
			if (this._strength < 1)
			{
				tmp = Math.Round(this._health * 0.2);
				this._strength = Convert.ToInt32(tmp);
				this._health = Convert.ToInt32(Math.Round(0.8 * this._health));
			}
		}
	}
}
