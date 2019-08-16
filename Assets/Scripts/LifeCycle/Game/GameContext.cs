namespace LifeCycle.Game
{
    public class GameContext
    {
        public bool IsPlayerAlive { get; set; }
        public bool NewLevelsAvailable { get; set; }
        public int PlayerScore { get; private set; }
        public int LevelNumber { get; set; }
        public int PlayerLives { get; private set; }

        public void SetPlayerPosition(int ratingPosition)
        {
            throw new System.NotImplementedException();
        }

        public void TakeOneLife()
        {
            throw new System.NotImplementedException();
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
        }
    }
}