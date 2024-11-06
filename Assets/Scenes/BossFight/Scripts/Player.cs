using UnityEngine;
using UnityEngine.UI;

namespace Scenes.BossFight.Scripts
{
    public class Player : Fighters
    {
        public Button attack;
        public Button defense;
        
        private Button Attack => attack;
        private Button Defense => defense;
        
        private void OnEnable()
        {
            Attack.onClick.AddListener(Attacks);
            Defense.onClick.AddListener(Defences);
        }
        
        private void OnDisable()
        {
            Attack.onClick.RemoveListener(Attacks);
            Defense.onClick.RemoveListener(Defences);
        }
    }
}
