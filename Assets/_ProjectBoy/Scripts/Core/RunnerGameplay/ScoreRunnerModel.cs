namespace _ProjectBoy.Scripts.Core.RunnerGameplay
{
    public class ScoreRunnerModel
    {
        public float Score { get; private set; }

        public void AddScore(float value)
        {
            Score += value;
        }
    }
}