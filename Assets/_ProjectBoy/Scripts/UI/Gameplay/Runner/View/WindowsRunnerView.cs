using _ProjectBoy.Scripts.Core.RunnerGameplay;
using UnityEngine;

namespace _ProjectBoy.Scripts.UI.Gameplay.Runner.View
{
    public class WindowsRunnerView : MonoBehaviour, IView
    {
        public IVisibilityGame[] WindowsView;
        [field: SerializeField] public DefeatScreenView DefeatScreenView { get; private set; }
        [field: SerializeField] public WinScreenView WinScreenView { get; private set; }
        [field: SerializeField] public ScoreView ScoreView { get; private set; }

        private void Awake()
        {
            WindowsView = new IVisibilityGame[]
            {
                DefeatScreenView,
                WinScreenView,
                ScoreView
            };
        }
    }
}