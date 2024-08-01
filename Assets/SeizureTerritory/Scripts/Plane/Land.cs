using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
        Debug.Log("до изменения " + Texture.material.color);
        Texture.material = material;
        Texture.material.color = material.color;
        Debug.Log("После " + Texture.material.color);
        Debug.Log("Мой цвет "  + material.color);
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