using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Land : MonoBehaviour
{
    private Renderer _texture;
    private Outline _outline;

    private void Start()
    {
        _texture = GetComponent<Renderer>();
        _outline = GetComponent<Outline>();
    }

    public void SetMaterial(Material material)
    {
        _texture.material = material;
        _texture.material.color = material.color;
    }

    public bool IsNotDefaultMaterial(Material material)
    {
        if (_texture.material.color == material.color)
        {
            return false;
        }
        
        return true;
    }

    public void ActOutline() => _outline.enabled = true;
    
    public void DeactOutline() => _outline.enabled = false;
}