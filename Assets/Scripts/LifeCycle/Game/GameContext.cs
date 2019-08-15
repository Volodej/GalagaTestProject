namespace LifeCycle.Game
{
    public class GameContext
    {
        public bool IsPlayerAlive { get; set; }
        public bool NewLevelsAvailable { get; set; }
        public int PlayerScore { get; set; }
        public int LevelNumber { get; set; }
        public int PlayerLives { get; set; }

        public void SetPlayerPosition(int ratingPosition)
        {
            throw new System.NotImplementedException();
        }

        public void TakeOneLife()
        {
            throw new System.NotImplementedException();
        }
    }
}