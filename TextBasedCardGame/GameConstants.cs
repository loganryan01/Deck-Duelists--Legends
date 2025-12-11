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
        public static readonly IImmutableList<int> AI_OFFENSIVE_PRIORITY_LIST = ImmutableList.Create((int)CardEffect.IncreaseHeroAttack, (int)CardEffect.DecreaseEnemyHealth, (int)CardEffect.DecreaseEnemyAttack, (int)CardEffect.IncreaseHeroHealth);
        public static readonly IImmutableList<int> AI_DEFENSIVE_PRIORITY_LIST = ImmutableList.Create((int)CardEffect.IncreaseHeroHealth, (int)CardEffect.DecreaseEnemyAttack, (int)CardEffect.IncreaseHeroAttack, (int)CardEffect.DecreaseEnemyHealth);

        public const int STARTING_TURN_NUMBER = 1;
        public const int STARTING_HERO_HEALTH = 20;
        public const int STARTING_HERO_ATTACK = 1;

        public const string SPLITTER_TEXT = "==================================";
        public const string CARD_PRINT_FORMAT = "[{0}] {1}";
        public const string HERO_INFO_FORMAT = "Health = {0}\t    Attack = {1}";
        public const string TURN_INFO_FORMAT = "Turn: {0}";
        public const string ENEMY_ACTION_FORMAT = "Enemy has played '{0}'";
        public const string LOG_FORMAT = "{0} used \"{1}\"";
    }
}
