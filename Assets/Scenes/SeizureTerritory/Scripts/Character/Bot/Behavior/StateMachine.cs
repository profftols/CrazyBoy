namespace SeizureTerritory.Scripts.Behavior
{
    public class StateMachine
    {
        public StateBot CurrentStateBot { get; private set; }
        
        public void Initialize(StateBot initialStateBot)
        {
            CurrentStateBot = initialStateBot;
            CurrentStateBot.Enter();
        }
        
        public void ChangeState(StateBot newStateBot)
        {
            CurrentStateBot.Exit();
            CurrentStateBot = newStateBot;
            CurrentStateBot.Enter();
        }
    }
}