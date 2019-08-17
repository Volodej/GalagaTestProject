namespace LifeCycle.Level
{
    public class LevelContext
    {
        public bool HasWavesToIncome { get; set; }
        public bool LevelEndAchieved { get; set; }
        public int TimeToWait { get; set; }
        
        public override string ToString()
        {
            return $"{typeof(LevelContext).FullName}\n" +
                   $"\tHasWavesToIncome: {HasWavesToIncome}\n" +
                   $"\tLevelEndAchieved: {LevelEndAchieved}\n" +
                   $"\tTimeToWait: {TimeToWait}";
        }
    }
}