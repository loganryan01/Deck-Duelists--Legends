# Deck Duelists: Legends

A turn-based card battler where you strengthen your hero and weaken your enemy before clashing in combat.

Play cards to boost attack, restore health, and cripple your opponent's power as you prepare for decisive battles.

## Gameplay Preview

The game is played through a split interface showing the current battle state and a running action log.

Players choose cards each turn to stengthen themselves or weaken their opponent before combat resolves.

```
=======================================|=======================================|
Turn: 3				                   |	              Log                  |
=======================================|=======================================|
              Enemy Hero:   	       |Turn 1: Enemy used "+1 Hero Attack"    |
Health = 18                  Attack = 2|Turn 2: Player used "-1 Enemy Health"  |
				                       |                                       |
				                       |                                       |
				                       |                                       |
				                       |                                       |
Health = 18     	         Attack = 1|                                       |
             Player Hero:              |                                       |
=======================================|                                       |
[1] +1 Hero Attack             	       |                                       |
[2] +1 Hero Health             	       |                                       |
[3] -1 Enemy Attack            	       |                                       |
=======================================|                                       |
Enemy has played "-1 Enemy Attack"     |                                       |
                                       |=======================================|
```

## How It Works

The game is a turn-based card battler where the player faces off against an AI-controlled enemy.

### Turn Flow

Each turn follows a simple sequence:

1. **Draw Phase**

   Draw cards until your hand contains 3 cards.

2. **Play Phase**

   Choose **one card** from your hand to play.
   Cards can:
   - Increase your hero's attack
   - Restore your health
   - Weaken then enemy's attack
   - Damage or drain the enemy's health

3. **Attack Phase**

   Your hero automatically attacks, dealing damage equal to their current attack value to the enemy.
   
4. **End Turn**

   Control passes to the enemy, which follows the same rules.

---

### Rules & Limitations

- Maximum hand size: **3 cards**
- Cards played per turn: **1**
- No mana or resource system
- Strategy is based on timing and card choice rather than resource management

---

### Winning the Game

Reduce the enemy hero's health to **0** to win.

Be careful-if your health reaches 0 first, you lose.

## Controls

### During Gameplay

- **1 / 2 / 3** - Select and play a card from your hand

---

### Menu / Settings

- **1 / 2 / 3 / 4** - Select an option
- **Number Input** - Set number of rounds or points

---

There are no additional controls. All gameplay actions are performed using simple numeric input.

## Installation & running

### Requirements

- .NET 8
- Windows OS

---

### Running the Game

1. Navigate to the game folder
2. Locate the `.exe` file
3. Double-click the `.exe` to launch the Game

---

### Notes

- This is a console-based application, so a terminal window will open when the game starts.
- No additional setup or installation is required.

## Features

- **Turn-Based Combat**

   Structured gameplay where each player takes actions in sequence.
  
- **AI Opponent**

   Battle against a computer-controlled enemy that follows the same rules as the player.
   
- **Card-Based Mechanics**

   Use cards to:
   - Increase your hero's attack
   - Heal your hero
   - Reduce enemy attack
   - Deal damage
   
- **Action Log System**

   View a running log of actions taken during the match.
   This can be enabled or disabled in the settings menu.
   
- **Custom Game Settings**

   Configure gameplay by adjusting:
   - Number of rounds
   - Points required to win
   
## Known Limitations

- **Basic AI**
   
   The enemy uses simple decision-making and does not perform advanced strategies.
   
- **No Animations**

   Gameplay is entirely text-based with no visual animations.
   
- **Console-Based Interface**

   The game runs in a terminal window and does not include a graphical user interface.
   
- **No Save System**

   Progress is not saved between sessions.
   
## Roadmap

Planned features and improvements for future development:

- **Save System**

   Allow players to save and continue their progress.
   
- **Replay System**

   Review past matches and decisions.
   
- **Improved AI**

   Smarter enemy behavior with better decision-making and strategy.
   
- **Expanded Card System**

   Introduce new card types and more varied effects.
   
- **Status Effects**

   Add mechanics such as buffs, debuffs, and ongoing effects over multiple turns.
   
- **Story Mode**

   A structured gameplay experience with narrative elements.
   
- **Story Creator**

   Tools for creating custom scenarios or campaigns.
   
- **Deck Builder**

   Allow players to create and customize their own decks.
   
- **Multiplayer**

   Play against other players instead of AI.
   
## Credits

**Developer:** Logan Ryan

This is a solo project developed using C# and .NET 8.

---

## About

This project was created as a text-based card game focused on simple mechanics, strategic decision-making, and clean console-based gameplay.

All systems, including gameplay logic, UI, and game flow, were built from scratch.