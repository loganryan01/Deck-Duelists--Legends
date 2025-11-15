using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public class Player
    {
        private Deck deck;
        public Deck Deck 
        { 
            get { return deck; } 
        }

        private readonly List<Card> hand;
        public List<Card> Hand 
        { 
            get { return hand; } 
        }

        private int heroHealth;
        public int HeroHealth 
        {
            get { return heroHealth; }
        }

        private int heroAttack;
        public int HeroAttack 
        { 
            get { return heroAttack; }
        }

        private int wins;
        public int Wins 
        { 
            get { return wins; } 
        }

        public Player()
        {
            deck = new Deck();
            hand = new List<Card>();
            heroHealth = GameConstants.STARTING_HERO_HEALTH;
            heroAttack = GameConstants.STARTING_HERO_ATTACK;
            wins = 0;
        }

        public void Reset()
        {
            deck = new Deck();
            hand.Clear();
            heroHealth = GameConstants.STARTING_HERO_HEALTH;
            heroAttack = GameConstants.STARTING_HERO_ATTACK;
        }

        public void IncrementHeroAttack()
        {
            heroAttack++;
        }

        public void DecrementHeroAttack()
        {
            heroAttack--;
        }

        public void IncrementHeroHealth()
        {
            heroHealth++;
        }

        public void DecrementHeroHealth()
        {
            heroHealth--;
        }

        public void DecreaseHeroHealth(int amount)
        {
            heroHealth -= amount;
        }

        public void IncrementWinNumber()
        {
            wins++;
        }
    }
}
