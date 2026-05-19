using System.Text.Json;

namespace TextBasedCardGame
{
    /// <summary>
    /// Responsible for loading card data from external JSON files.
    /// 
    /// This converts raw JSON into CardDatabaseData objects
    /// that can later be transformed into playable cards
    /// by the CardFactory.
    /// </summary>
    public static class CardLoader
    {
        /// <summary>
        /// Loads and deserializes the card database JSON file.
        /// </summary>
        public static CardDatabaseData LoadCards(string path)
        {
            string json = File.ReadAllText(path);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                // Allows the JSON property names to ignore casing differences.
                // Example:
                // "cards" -> Cards
                PropertyNameCaseInsensitive = true
            };

            CardDatabaseData database =
                JsonSerializer.Deserialize<CardDatabaseData>(json, options);

            return database;
        }
    }
}
