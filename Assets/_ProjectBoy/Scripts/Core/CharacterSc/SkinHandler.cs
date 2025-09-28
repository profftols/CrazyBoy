using UnityEngine;

namespace _ProjectBoy.Scripts.Core.CharacterSc
{
    public class SkinHandler : MonoBehaviour
    {
        [field: SerializeField] public Skin Skin { get; private set; }

        public void SetSkin(Skin skinObject)
        {
            if (Skin != null)
                Destroy(Skin);

            Skin = Instantiate(skinObject, transform);
        }
    }
}