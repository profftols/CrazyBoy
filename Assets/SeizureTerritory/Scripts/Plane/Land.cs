using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Land : MonoBehaviour
{
    public Renderer Texture { get; private set; }
    private Map _map;

    private void Start()
    {
        Texture = GetComponent<Renderer>();
        _map = GetComponentInParent<Map>();
    }

    public void SetMaterial(Material material)
    {
        Texture.material = material;
        Texture.material.color = material.color;
    }

    public bool IsNotValidMaterial(Material material)
    {
        if (Texture.material.color == material.color)
        {
            return false;
        }
        
        return true;
    }
}