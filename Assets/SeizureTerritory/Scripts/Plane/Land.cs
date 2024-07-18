using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(MeshRenderer))]
public class Land : MonoBehaviour
{
    public Renderer TextureRender{ get; private set; }
    public Renderer DefaultRender { get; private set; }
    private Map _map;

    private void Start()
    {
        DefaultRender = GetComponent<Renderer>();
        _map = GetComponentInParent<Map>();
        TextureRender = DefaultRender;
    }

    public void SetMaterial(Material material) => TextureRender.material = material;
    
    public bool IsValidColor(Color color)
    {
        if (TextureRender.material.color == color || DefaultRender.material.color == color)
        {
            return true;
        }
        else
        {
            return false; //если этот ретерн то реализовать смерть
        }
    }
}