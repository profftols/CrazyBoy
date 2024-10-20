namespace SeizureTerritory.Scripts.Behavior
{
    public abstract class StateBot 
    {
        protected Bot _bot;
        
        protected StateBot(Bot bot)
        {
            _bot = bot;
        }
        
        public virtual void Enter() {}
        public virtual void Exit() {}
        public virtual void Update() {}
    }
}