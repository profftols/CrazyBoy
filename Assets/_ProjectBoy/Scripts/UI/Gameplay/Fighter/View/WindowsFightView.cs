using UnityEngine;

namespace _ProjectBoy.Scripts.UI.Gameplay.Fighter.View
{
    public class WindowsFightView : MonoBehaviour, IView
    {
        [field: SerializeField] public ButtonActionView ButtonActionView { get; private set; }
        [field: SerializeField] public EnemyHealthView EnemyView { get; private set; }
        [field: SerializeField] public PlayerHealthView PlayerView { get; private set; }
        [field: SerializeField] public DefeatScreenView DefeatScreenView { get; private set; }
        [field: SerializeField] public WinScreenView WinScreenView { get; private set; }
    }
}