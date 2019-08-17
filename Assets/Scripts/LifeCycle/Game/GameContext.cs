namespace LifeCycle.Game
{
    public class GameContext
    {
        public bool IsPlayerAlive { get; set; }
        public bool NewLevelsAvailable { get; set; }
        public int PlayerScore { get; private set; }
        public int LevelNumber { get; set; }
        public int PlayerLives { get; private set; }
        public int PlayerPositionInRating { get; private set; }

        public void SetPlayerPositionInRating(int ratingPosition)
        {
            PlayerPositionInRating = ratingPosition;
        }

        public void TakeOneLife()
        {
            PlayerLives--;
            IsPlayerAlive = true;
        }

        public int AddScore(int points)
        {
            return PlayerScore += points;
        }

        public void Reset()
        {
            PlayerLives = 2;
            PlayerScore = 0;
            LevelNumber = 0;
            NewLevelsAvailable = true;
            PlayerPositionInRating = -1;
        }

        public override string ToString()
        {
            return $"{typeof(GameContext).FullName}\n" +
                   $"\tIsPlayerAlive: {IsPlayerAlive}\n" +
                   $"\tNewLevelsAvailable: {NewLevelsAvailable}\n" +
                   $"\tPlayerScore: {PlayerScore}\n" +
                   $"\tLevelNumber: {LevelNumber}\n" +
                   $"\tPlayerLives: {PlayerLives}\n" +
                   $"\tPlayerPositionInRating: {PlayerPositionInRating}";
        }
    }
}