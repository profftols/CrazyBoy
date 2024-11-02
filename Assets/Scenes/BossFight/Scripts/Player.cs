using UnityEngine;
using UnityEngine.UI;

namespace Scenes.BossFight.Scripts
{
    public class Player : Fighters
    {
        [SerializeField] private Button _attack;
        [SerializeField] private Button _defense;
        
        private void OnEnable()
        {
            _attack.onClick.AddListener(Attack);
            _defense.onClick.AddListener(Defence);
        }
        
        private void OnDisable()
        {
            _attack.onClick.RemoveListener(Attack);
            _defense.onClick.RemoveListener(Defence);
        }
    }
}
