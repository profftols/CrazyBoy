namespace _ProjectBoy.Scripts.Core.FighterGameplay
{
    public interface IFighterMode
    {
        public Hero Player { get; }
        public Boss Enemy { get; }
        void InitGame();
        void StopFight();
        void StartFight();
        void SubscribeEvent();
        void UnsubscribeEvent();
    }
}