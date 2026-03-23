using System.Collections.Immutable;

namespace TextBasedCardGame
{
    /// <summary>
    /// Represents all possible card effects in the game.
    /// </summary>
    public enum CardEffect
    {
        IncreaseHeroAttack,
        IncreaseHeroHealth,
        DecreaseEnemyAttack,
        DecreaseEnemyHealth,
    }
    
    /// <summary>
    /// Stores all constant values used throughout the game.
    /// Includes UI text, gameplay values, and AI behavior.
    /// </summary>
    public static class GameConstants
    {
        //------------------------------------------------
        // UI - TITLE & MODES
        //------------------------------------------------

        public static readonly string[] TITLE = 
        { 
            " _____        _       ___                  _      ___             _      ___                ",
            "|_   _|____ _| |_ ___| _ ) __ _ ___ ___ __| |___ / __|__ _ _ _ __| |___ / __|__ _ _ __  ___ ",
            "  | |/ -_) \\ /  _|___| _ \\/ _` (_-</ -_) _` |___| (__/ _` | '_/ _` |___| (_ / _` | '  \\/ -_)",
            "  |_|\\___/_\\_\\\\__|   |___/\\__,_/__/\\___\\__,_|    \\___\\__,_|_| \\__,_|    \\___\\__,_|_|_|_\\___|"
        };

        public static readonly string[] GAME_FORMATS = 
        { 
            "Rounds", 
            "Points" 
        };

        public static readonly string[] GAME_MENU_OPTIONS =
        {
            "Play",
            "Settings",
            "Exit"
        };

        //------------------------------------------------
        // AI CONFIGURATION
        //------------------------------------------------

        /// <summary>
        /// Priority list used when the enemy is playing offensively.
        /// </summary>
        public static readonly IImmutableList<CardEffect> AI_OFFENSIVE_PRIORITY_LIST = 
            ImmutableList.Create(
                CardEffect.IncreaseHeroAttack, 
                CardEffect.DecreaseEnemyHealth, 
                CardEffect.DecreaseEnemyAttack, 
                CardEffect.IncreaseHeroHealth
            );

        /// <summary>
        /// Priority list used when the enemy is playing defensively.
        /// </summary>
        public static readonly IImmutableList<CardEffect> AI_DEFENSIVE_PRIORITY_LIST = 
            ImmutableList.Create(
                CardEffect.IncreaseHeroHealth, 
                CardEffect.DecreaseEnemyAttack, 
                CardEffect.IncreaseHeroAttack, 
                CardEffect.DecreaseEnemyHealth
            );

        //------------------------------------------------
        // GAMEPLAY VALUES
        //------------------------------------------------

        public const int STARTING_TURN_NUMBER = 1;
        public const int STARTING_HERO_HEALTH = 20;
        public const int STARTING_HERO_ATTACK = 1;
        public const int STARTING_HERO_WINS = 0;

        public const int MAX_LOG_SIZE = 13;
        public const int MAX_HAND_SIZE = 3;
        public const int MAX_DECK_SIZE = 52;

        public const int DEFAULT_NUMBER_OF_ROUNDS = 3;
        public const int MIN_NUMBER_OF_ROUNDS = 1;

        // Y position of box 1
        public const int BOX_ONE_Y_POSITION = 1;
        public const int BOX_ONE_WIDTH = 1;

        // Y position of box 2
        public const int BOX_TWO_Y_POSITION = 3;
        public const int BOX_TWO_WIDTH = 8;

        // Y position of box 3
        public const int BOX_THREE_Y_POSITION = 12;
        public const int BOX_THREE_WIDTH = 3;

        // Y position of box 4
        public const int BOX_FOUR_Y_POSITION = 16;
        public const int BOX_FOUR_WIDTH = 2;

        // Y position of log box 1
        public const int LOG_BOX_ONE_Y_POSITION = 1;
        public const int LOG_BOX_ONE_WIDTH = 1;

        // Y position of log box 2
        public const int LOG_BOX_TWO_Y_POSITION = 3;

        // Width of the log section
        public const int LOG_WIDTH = 18;

        public const int MINIMUM_ATTACK_NUMBER = 0;

        // Minimum health the enemy needs to play offensively
        public const int MINIMUM_ENEMY_OFFENSIVE_HEALTH = 5;

        public const int THREE_SECOND_DELAY = 3000;
        public const int HALF_SECOND_DELAY = 500;

        public const int NUMBER_OF_FLASHES = 7;

        //------------------------------------------------
        // UI FORMATTING
        //------------------------------------------------

        public const string SPLITTER_TEXT = "=======================================";

        public const string INDEX_PRINT_FORMAT = "[{0}] ";
        public const string CARD_PRINT_FORMAT = "[{0}] {1}";
        public const string HERO_HEALTH_FORMAT = "Health = {0}";
        public const string HERO_ATTACK_FORMAT = "Attack = {0}";
        public const string TURN_INFO_FORMAT = "Turn: {0}";
        public const string ENEMY_ACTION_FORMAT = "Enemy has played '{0}'";

        public const string LOG_FORMAT = "Turn {0}: {1} used \"{2}\"";
        public const string LOG_SETTING_FORMAT = "Log = {0}";

        public const string ROUND_FORMAT = "Round {0}";
        public const string POINT_FORMAT = "{0} = {1}/{2}";
        public const string WINNER_FORMAT = "{0} = Winner!";
        public const string GAMES_FORMAT = "How many {0} do you want to play?";

        public const string READY_MESSAGE = "Ready";
        public const string FIGHT_MESSAGE = "FIGHT!";
        public const string INVALID_INPUT_MESSAGE = "Need a valid number";
        public const string CONTINUE_MESSAGE = "Press any key to continue...";
        public const string KO_MESSAGE = "KO!";
        public const string PLAYER_CHOOSE_MESSAGE = "What card do you want to play?";
        public const string PLAYER_LOSE_MESSAGE = "Sorry... You Lose...";
        public const string PLAYER_WIN_MESSAGE = "Congratulations! You Win!";

        public const string BACK_OPTION_TEXT = "Back";

        public const string DEFAULT_PLAYER_NAME = "Player";
        public const string DEFAULT_ENEMY_NAME = "Enemy";

        public const string PLAYER_STATS_NAME = "Player Hero:";
        public const string ENEMY_STATS_NAME = "Enemy Hero:";

        public const string FORMAT_DIVIDER = " / ";
        public const string LOG_VERTICAL_BORDER = "|";

        public const string INCREASE_HERO_ATTACK_CARD_NAME = "+1 Hero Attack";
        public const string INCREASE_HERO_HEALTH_CARD_NAME = "+1 Hero Health";
        public const string DECREASE_ENEMY_ATTACK_CARD_NAME = "-1 Enemy Attack";
        public const string DECREASE_ENEMY_HEALTH_CARD_NAME = "-1 Enemy Health";

        public const string POINTS_TITLE = "Points";
        public const string LOG_TITLE = "Log";
    }
}
