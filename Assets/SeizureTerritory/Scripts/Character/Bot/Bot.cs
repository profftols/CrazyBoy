using UnityEngine;

public class Bot : Character
{
    private Renderer _renderMaterial;

    private void Awake()
    {
        _renderMaterial = GetComponent<Renderer>();
    }

    private void OnDestroy()
    {
        
    }

    public void SetMaterial(Material material) => _renderMaterial.material = material;
}
