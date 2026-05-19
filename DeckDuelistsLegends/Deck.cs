using System;
using System.Collections.Generic;

namespace TextBasedCardGame
{
    /// <summary>
    /// Represents a deck of cards.
    /// Handles deck creation, drawing cards, and deck state.
    /// </summary>
    public class Deck
    {
        //-------------------------------------------------
        // FIELDS
        //-------------------------------------------------
        
        private readonly List<Card> cards = new List<Card>();

        /// <summary>
        /// Gets the list of cards in the deck.
        /// </summary>
        public List<Card> Cards => cards;

        //-------------------------------------------------
        // CONSTRUCTOR
        //-------------------------------------------------

        public Deck() 
        {
            CreateDeck();
        }

        //-------------------------------------------------
        // DECK CREATION
        //-------------------------------------------------

        /// <summary>
        /// Creates a standard 52-card deck with equal distribution of effects.
        /// </summary>
        private void CreateDeck()
        {
            CardDatabaseData database = CardLoader.LoadCards(Path.Combine("Data", "cards.json"));

            while (cards.Count < GameConstants.MAX_DECK_SIZE)
            {
                foreach (var data in database.Cards)
                {
                    for (int i = 0; i < GameConstants.MAX_DECK_SIZE / 4; i++)
                    {
                        cards.Add(CardFactory.CreateCard(data));
                    }
                }
            }
        }

        //-------------------------------------------------
        // DEBUG / DISPLAY
        //-------------------------------------------------

        /// <summary>
        /// Prints all cards in the deck to the console.
        /// Mainly used for debugging.
        /// </summary>
        public void PrintDeck()
        {
            int index = 0;

            foreach (var card in cards)
            {
                Console.WriteLine(string.Format(GameConstants.CARD_PRINT_FORMAT, index + 1, card.Name));
                index++;
            }
        }

        //-------------------------------------------------
        // GAMEPLAY METHODS
        //-------------------------------------------------

        /// <summary>
        /// Draws a random card from the deck and remove it.
        /// </summary>
        public Card DrawCard()
        {
            int randomIndex = GameRandom.Instance.Next(cards.Count);

            Card chosenCard = cards[randomIndex];
            cards.Remove(chosenCard);

            return chosenCard;
        }

        /// <summary>
        /// Returns true if the deck has no remaining cards.
        /// </summary>
        public bool IsEmpty()
        {
            return cards.Count == 0;
        }
    }
}
