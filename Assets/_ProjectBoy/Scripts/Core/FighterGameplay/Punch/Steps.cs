using System.Collections.Generic;

namespace _ProjectBoy.Scripts.Core.FighterGameplay.Punch
{
    public class Steps
    {
        private readonly Queue<Actions> _actions = new();
        public int Count => _actions.Count;

        public void AddAction(Actions action)
        {
            _actions.Enqueue(action);
        }

        public Actions GetAction()
        {
            return _actions.Dequeue();
        }
    }
}