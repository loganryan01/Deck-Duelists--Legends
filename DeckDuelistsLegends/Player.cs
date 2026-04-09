using System.Collections.Generic;

namespace TextBasedCardGame
{
    /// <summary>
    /// Represents a player (or enemy) in the game.
    /// 
    /// Stores deck, hand, hero stats, and win count.
    /// </summary>
    public class Player
    {
        //-----------------------------------------------
        // FIELDS & PROPERTIES
        //-----------------------------------------------

        private Deck deck;
        public Deck Deck => deck;

        private readonly List<Card> hand;
        public List<Card> Hand => hand;

        private int heroHealth;
        public int HeroHealth => heroHealth;

        private int heroAttack;
        public int HeroAttack => heroAttack;

        private int wins;
        public int Wins => wins;

        //-----------------------------------------------
        // CONSTRUCTOR
        //-----------------------------------------------

        public Player()
        {
            deck = new Deck();
            hand = new List<Card>();
            heroHealth = GameConstants.STARTING_HERO_HEALTH;
            heroAttack = GameConstants.STARTING_HERO_ATTACK;
            wins =GameConstants.STARTING_HERO_WINS;
        }

        //-----------------------------------------------
        // RESET
        //-----------------------------------------------

        /// <summary>
        /// Resets the player's state for a new game.
        /// </summary>
        public void Reset()
        {
            deck = new Deck();
            hand.Clear();
            heroHealth = GameConstants.STARTING_HERO_HEALTH;
            heroAttack = GameConstants.STARTING_HERO_ATTACK;
        }

        //-----------------------------------------------
        // HERO STAT MODIFIERS
        //-----------------------------------------------

        public void IncrementHeroAttack()
        {
            heroAttack++;
        }

        public void DecrementHeroAttack()
        {
            heroAttack--;
        }

        public void ModifyHeroHealth(int amount)
        {
            heroHealth += amount;
        }

        //-----------------------------------------------
        // WIN TRACKING
        //-----------------------------------------------

        /// <summary>
        /// Increments the player's win count.
        /// </summary>
        public void AddWin()
        {
            wins++;
        }

        /// <summary>
        /// Resets the player's win count.
        /// </summary>
        public void ResetWinCount()
        {
            wins = GameConstants.STARTING_HERO_WINS; 
        }
    }
}
