using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Renderer))]
public class BotAI : Character
{
    private Renderer _renderMaterial;

    private void Awake()
    {
        _renderMaterial = GetComponent<Renderer>();
    }

    private void OnDestroy()
    {
        Debug.Log("Ты умир");
    }

    public void SetMaterial(Material material) => _renderMaterial.material = material;
}
