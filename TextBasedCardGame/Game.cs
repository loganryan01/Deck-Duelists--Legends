using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public enum EGameTurnState
    {
        Player = 0,
        Enemy = 1
    }

    public struct Player
    {
        public Deck Deck { get; set; }
        public List<Card> Hand { get; set; }
        public int HeroHealth { get; set; }
        public int HeroAttack { get; set; }
        public int Wins { get; set; }
    }

    public class Game
    {
        private bool isRunning = true;
        
        private int turnNumber = GameConstants.STARTING_TURN_NUMBER;
        public int TurnNumber
        {
            get {  return turnNumber; }
        }

        // Player Info
        private Player player;
        public Player Player
        {
            get { return player; }
        }

        // Enemy Info
        private Player enemy;
        public Player Enemy
        {
            get { return enemy; }
        }
        
        public Game()
        {
            
        }

        public void StartGame()
        {
            player.Deck = new Deck();
            player.Hand = new List<Card>();
            player.HeroHealth = GameConstants.STARTING_HERO_HEALTH;
            player.HeroAttack = GameConstants.STARTING_HERO_ATTACK;
            player.Wins = 0;

            enemy.Deck = new Deck();
            enemy.Hand = new List<Card>();
            enemy.HeroHealth = GameConstants.STARTING_HERO_HEALTH;
            enemy.HeroAttack = GameConstants.STARTING_HERO_ATTACK;
            enemy.Wins = 0;

            Update();
        }

        public void StopGame()
        {
            isRunning = false;
        }

        private void Update()
        {
            EGameTurnState gameTurnState = DetermineFirstTurn();
            GameTurnState gameTurnStrategy = gameTurnState == EGameTurnState.Player ? new PrePlayerTurnState() : new PreEnemyTurnState();
            GameTurnStateManager gameTurnStateManager = new GameTurnStateManager(gameTurnStrategy);

            while (isRunning)
            {
                gameTurnStateManager.DoAction(this);
            }
        }

        private static EGameTurnState DetermineFirstTurn()
        {
            Random random = new Random();
            int playerNumber = random.Next(2);
            return (EGameTurnState)playerNumber;
        }

        public void IncrementPlayerHeroAttack()
        {
            player.HeroAttack++;
        }

        public void DecrementPlayerHeroAttack()
        {
            player.HeroAttack--;
        }

        public void IncrementPlayerHeroHealth()
        {
            player.HeroHealth++;
        }

        public void DecrementPlayerHeroHealth()
        {
            player.HeroHealth--;
        }

        public void DecreasePlayerHeroHealth(int amount)
        {
            player.HeroHealth -= amount;
        }

        public void IncrementEnemyHeroAttack()
        {
            enemy.HeroAttack++;
        }

        public void DecrementEnemyHeroAttack()
        {
            enemy.HeroAttack--;
        }

        public void IncrementEnemyHeroHealth()
        {
            enemy.HeroHealth++;
        }

        public void DecrementEnemyHeroHealth()
        {
            enemy.HeroHealth--;
        }

        public void DecreaseEnemyHeroHealth(int amount)
        {
            enemy.HeroHealth -= amount;
        }

        public void IncrementTurnNumber()
        {
            turnNumber++;
        }

        #region Simulation Logic
        //private int simGameNumber = 0;

        //public void StartGameSim()
        //{
        //    prePlayerTurnAction -= HandlePrePlayerTurnSim;
        //    prePlayerTurnAction += HandlePrePlayerTurnSim;

        //    playerTurnAction -= HandlePlayerTurnSim;
        //    playerTurnAction += HandlePlayerTurnSim;

        //    postPlayerTurnAction -= HandlePostPlayerTurnSim;
        //    postPlayerTurnAction += HandlePostPlayerTurnSim;

        //    preEnemyTurnAction -= HandlePreEnemyTurnSim;
        //    preEnemyTurnAction += HandlePreEnemyTurnSim;

        //    enemyTurnAction -= HandleEnemyTurnSim;
        //    enemyTurnAction += HandleEnemyTurnSim;

        //    postEnemyTurnAction -= HandlePostEnemyTurnSim;
        //    postEnemyTurnAction += HandlePostEnemyTurnSim;

        //    for (simGameNumber = 0; simGameNumber < 100; simGameNumber++)
        //    {
        //        Update();
        //        ResetGame();
        //    }

        //    // Print simulation information
        //    Console.WriteLine("\nPlayer has won " + player.wins.ToString() + " games");
        //    Console.WriteLine("Enemy has won " + enemy.wins.ToString() + " games");
        //}

        //private void ResetGame()
        //{
        //    turnNumber = GameConstants.STARTING_TURN_NUMBER;

        //    player.deck = new Deck();
        //    player.hand.Clear();

        //    player.heroAttack = GameConstants.STARTING_HERO_ATTACK;
        //    player.heroHealth = GameConstants.STARTING_HERO_HEALTH;

        //    enemy.deck = new Deck();
        //    enemy.hand.Clear();

        //    enemy.heroAttack = GameConstants.STARTING_HERO_ATTACK;
        //    enemy.heroHealth = GameConstants.STARTING_HERO_HEALTH;
        //}

        //private void HandlePrePlayerTurnSim()
        //{
        //    if (player.deck.Cards.Count == 0)
        //    {
        //        Console.WriteLine(string.Format(GameConstants.ENEMY_WINS_SIM_FORMAT, (simGameNumber + 1).ToString(), turnNumber.ToString()));
        //        enemy.wins++;
        //    }
        //    else
        //    {
        //        gameActions.Push(playerTurnAction);
        //    }
        //}

        //public void HandlePlayerTurnSim()
        //{
        //    // Draw Player hand
        //    while (player.hand.Count < 3)
        //    {
        //        Card card = player.deck.DrawCard();
        //        player.hand.Add(card);
        //    }

        //    // Get card from Player hand
        //    switch (player.hand[0].EffectIndex)
        //    {
        //        case 0:
        //            player.heroAttack++;
        //            break;
        //        case 1:
        //            player.heroHealth++;
        //            break;
        //        case 2:
        //            enemy.heroAttack--;
        //            break;
        //        case 3:
        //            enemy.heroHealth--;
        //            break;
        //    }
        //    player.hand.RemoveAt(0);

        //    // Attack the Player hero
        //    if (player.heroAttack > 0)
        //    {
        //        enemy.heroHealth -= player.heroAttack;
        //    }

        //    gameActions.Push(postPlayerTurnAction);
        //}

        //private void HandlePostPlayerTurnSim()
        //{
        //    if (enemy.heroHealth <= 0)
        //    {
        //        Console.WriteLine(string.Format(GameConstants.PLAYER_WINS_SIM_FORMAT, (simGameNumber + 1).ToString(), turnNumber.ToString()));
        //        player.wins++;
        //    }
        //    else
        //    {
        //        gameActions.Push(preEnemyTurnAction);
        //    }
        //}

        //private void HandlePreEnemyTurnSim()
        //{
        //    if (enemy.deck.Cards.Count == 0)
        //    {
        //        Console.WriteLine(string.Format(GameConstants.PLAYER_WINS_SIM_FORMAT, (simGameNumber + 1).ToString(), turnNumber.ToString()));
        //        player.wins++;
        //    }
        //    else
        //    {
        //        gameActions.Push(enemyTurnAction);
        //    }
        //}

        //private void HandleEnemyTurnSim()
        //{
        //    // Draw Enemy hand
        //    while (enemy.hand.Count < 3)
        //    {
        //        Card card = enemy.deck.DrawCard();
        //        enemy.hand.Add(card);
        //    }

        //    // Get card from enemy hand
        //    switch (enemy.hand[0].EffectIndex)
        //    {
        //        case 0:
        //            enemy.heroAttack++;
        //            break;
        //        case 1:
        //            enemy.heroHealth++;
        //            break;
        //        case 2:
        //            player.heroAttack--;
        //            break;
        //        case 3:
        //            player.heroHealth--;
        //            break;
        //    }
        //    enemy.hand.RemoveAt(0);

        //    // Attack the Player hero
        //    if (enemy.heroAttack > 0)
        //    {
        //        player.heroHealth -= enemy.heroAttack;
        //    }

        //    // End of turn
        //    turnNumber++;

        //    gameActions.Push(postEnemyTurnAction);
        //}

        //private void HandlePostEnemyTurnSim()
        //{
        //    if (player.heroHealth <= 0)
        //    {
        //        Console.WriteLine(string.Format(GameConstants.ENEMY_WINS_SIM_FORMAT, (simGameNumber + 1).ToString(), turnNumber.ToString()));
        //        enemy.wins++;
        //    }
        //    else
        //    {
        //        gameActions.Push(prePlayerTurnAction);
        //    }
        //}
        #endregion
    }
}
