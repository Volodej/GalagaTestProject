namespace TopPlayers
{
    public class TopPlayersStorage
    {
        public int LowestScore { get; set; }
        public int HighestScore { get; set; }

        /// <summary>
        /// Add player's score to global rating
        /// </summary>
        /// <returns>Returns player position in the rating.</returns>
        public int AddPlayerScore(string userName, int gameContextPlayerScore)
        {
            throw new System.NotImplementedException();
        }
    }
}