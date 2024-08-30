using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Land : MonoBehaviour
{
    private Renderer _texture;

    private void Start()
    {
        _texture = GetComponent<Renderer>();
    }

    public void SetMaterial(Material material)
    {
        _texture.material = material;
        _texture.material.color = material.color;
    }

    public bool IsNotValidMaterial(Material material)
    {
        if (_texture.material.color == material.color)
        {
            return false;
        }
        
        return true;
    }
}