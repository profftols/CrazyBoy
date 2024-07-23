using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(MeshRenderer))]
public class Land : MonoBehaviour
{
    public Color RenderColor { get; private set; }
    
    private Renderer _texture;
    private Map _map;

    private void Start()
    {
        _texture = GetComponent<Renderer>();
        _map = GetComponentInParent<Map>();
    }

    public void SetMaterial(Material material)
    {
        _texture.material = material;
        RenderColor = material.color;
    }

    public bool IsNotValidColor(Color color)
    {
        if (_texture.material.color == color)
        {
            return false;
        }
        
        return true;
    }
}