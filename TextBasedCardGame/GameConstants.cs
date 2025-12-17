using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedCardGame
{
    public enum CardEffect
    {
        IncreaseHeroAttack,
        IncreaseHeroHealth,
        DecreaseEnemyAttack,
        DecreaseEnemyHealth,
    }
    
    public static class GameConstants
    {
        public static readonly string[] TITLE = { " _____        _       ___                  _      ___             _      ___                ",
                                                  "|_   _|____ _| |_ ___| _ ) __ _ ___ ___ __| |___ / __|__ _ _ _ __| |___ / __|__ _ _ __  ___ ",
                                                  "  | |/ -_) \\ /  _|___| _ \\/ _` (_-</ -_) _` |___| (__/ _` | '_/ _` |___| (_ / _` | '  \\/ -_)",
                                                  "  |_|\\___/_\\_\\\\__|   |___/\\__,_/__/\\___\\__,_|    \\___\\__,_|_| \\__,_|    \\___\\__,_|_|_|_\\___|"};

        public static readonly IImmutableList<int> AI_OFFENSIVE_PRIORITY_LIST = ImmutableList.Create((int)CardEffect.IncreaseHeroAttack, (int)CardEffect.DecreaseEnemyHealth, (int)CardEffect.DecreaseEnemyAttack, (int)CardEffect.IncreaseHeroHealth);
        public static readonly IImmutableList<int> AI_DEFENSIVE_PRIORITY_LIST = ImmutableList.Create((int)CardEffect.IncreaseHeroHealth, (int)CardEffect.DecreaseEnemyAttack, (int)CardEffect.IncreaseHeroAttack, (int)CardEffect.DecreaseEnemyHealth);

        public const int STARTING_TURN_NUMBER = 1;
        public const int STARTING_HERO_HEALTH = 20;
        public const int STARTING_HERO_ATTACK = 1;

        public const string SPLITTER_TEXT = "=======================================";
        public const string CARD_PRINT_FORMAT = "[{0}] {1}";
        public const string HERO_INFO_FORMAT = "Health = {0}\t\t   Attack = {1}";
        public const string TURN_INFO_FORMAT = "Turn: {0}";
        public const string ENEMY_ACTION_FORMAT = "Enemy has played '{0}'";
        public const string LOG_FORMAT = "Turn {0}: {1} used \"{2}\"";
    }
}
