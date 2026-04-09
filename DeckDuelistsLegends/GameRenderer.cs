namespace TextBasedCardGame
{
    /// <summary>
    /// Responsible for rendering the game board and layout.
    /// </summary>
    public static class GameRenderer
    {
        /// <summary>
        /// Draws the main gameboard including hero stats and turn number.
        /// </summary>
        public static void DrawGameBoard(Player player, Player enemy, int turnNumber)
        {
            string enemyHeroAttack = string.Format(GameConstants.HERO_ATTACK_FORMAT, enemy.HeroAttack);
            string playerHeroAttack = string.Format(GameConstants.HERO_ATTACK_FORMAT, player.HeroAttack);

            // Turn number
            GameUtils.ClearConsoleLine(GameConstants.BOX_ONE_Y_POSITION);
            GameUtils.WriteAt(string.Format(GameConstants.TURN_INFO_FORMAT, turnNumber), 0, GameConstants.BOX_ONE_Y_POSITION);

            //-----------------------------------------------
            // Enemy hero stats
            //-----------------------------------------------

            GameUtils.ClearConsoleLine(GameConstants.BOX_TWO_Y_POSITION);
            GameUtils.WriteAt(
                GameConstants.ENEMY_STATS_NAME,
                (GameConstants.SPLITTER_TEXT.Length - GameConstants.ENEMY_STATS_NAME.Length) / 2,
                GameConstants.BOX_TWO_Y_POSITION
            );

            GameUtils.ClearConsoleLine(GameConstants.BOX_TWO_Y_POSITION + 1);
            GameUtils.WriteAt(string.Format(GameConstants.HERO_HEALTH_FORMAT, enemy.HeroHealth), 0, GameConstants.BOX_TWO_Y_POSITION + 1);
            GameUtils.WriteAt(
                enemyHeroAttack,
                GameConstants.SPLITTER_TEXT.Length - enemyHeroAttack.Length,
                GameConstants.BOX_TWO_Y_POSITION + 1
            );

            //-----------------------------------------------
            // Player hero stats
            //-----------------------------------------------

            GameUtils.ClearConsoleLine(GameConstants.BOX_TWO_Y_POSITION + GameConstants.BOX_TWO_WIDTH - 2);
            GameUtils.WriteAt(
                GameConstants.PLAYER_STATS_NAME,
                (GameConstants.SPLITTER_TEXT.Length - GameConstants.PLAYER_STATS_NAME.Length) / 2,
                GameConstants.BOX_TWO_Y_POSITION + GameConstants.BOX_TWO_WIDTH - 2
            );

            GameUtils.ClearConsoleLine(GameConstants.BOX_TWO_Y_POSITION + GameConstants.BOX_TWO_WIDTH - 1);
            GameUtils.WriteAt(string.Format(GameConstants.HERO_HEALTH_FORMAT, player.HeroHealth), 0, GameConstants.BOX_TWO_Y_POSITION + GameConstants.BOX_TWO_WIDTH - 1);
            GameUtils.WriteAt(
                playerHeroAttack,
                GameConstants.SPLITTER_TEXT.Length - playerHeroAttack.Length,
                GameConstants.BOX_TWO_Y_POSITION + GameConstants.BOX_TWO_WIDTH - 1
            );
        }
    }
}
