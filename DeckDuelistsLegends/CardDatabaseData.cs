namespace TextBasedCardGame
{
    /// <summary>
    /// Represents the root object of the card database JSON file.
    /// 
    /// This class exists because the JSON structure contains
    /// a top-level "cards" array.
    /// 
    /// Example:
    /// 
    /// {
    ///     "cards":
    ///     [
    ///         ...
    ///     ]
    /// }
    /// </summary>
    public class CardDatabaseData
    {
        /// <summary>
        /// Collection of all card definitions loaded from JSON.
        /// </summary>
        public List<CardData> Cards { get; set; }
    }
}
