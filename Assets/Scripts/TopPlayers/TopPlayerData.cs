using System;

namespace TopPlayers
{
    public class TopPlayerData
    {
        public string Name;
        public int Score;

        public static TopPlayerData Empty { get; } = new TopPlayerData
        {
            Name = string.Empty, Score = 0
        };
    }
}