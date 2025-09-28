using UnityEngine;

namespace _ProjectBoy.Scripts.Core.CharacterSc
{
    public class Skin : MonoBehaviour
    {
        [SerializeField] public SkinnedMeshRenderer skinned;

        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public float Price { get; private set; }

        public Mesh Mesh => skinned.sharedMesh;
        public Material[] Materials => skinned.sharedMaterials;
    }
}