﻿using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using Object = UnityEngine.Object;

[RequireComponent(typeof(Renderer))]
public class Character : MonoBehaviour
{
    protected List<Land> Lands;
    protected List<Land> Buffer;

    private Map _map;
    private Renderer _textureMaterial;
    private float _radius = 3f;
    private float _distance = 1f;
    private int _minNumberFields = 5;

    private void Start()
    {
        Lands = new List<Land>();
        Buffer = new List<Land>();
        _textureMaterial = GetComponent<Renderer>();
        
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        RaycastHit[] hits = Physics.SphereCastAll(origin, _radius, direction, _distance);

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent(out Land land))
            {
                land.SetMaterial(_textureMaterial.material);
                Lands.Add(land);
            }
        }
    }
    
    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Land land))
        {
            if (ReferenceEquals(_textureMaterial, null) == false)
            {
                if (ChangeLandMaterial(land))
                {
                    Buffer.Add(land);
                }
                else if (IsConquerLands())
                {
                    PaintInside();
                }
            }
        }
    }
    
    public void SetMap(Map map)
    {
        _map = map;
    }
    
    private bool ChangeLandMaterial(Land land)
    {
        if (Lands.Contains(land) == false)
        {
            land.SetMaterial(_textureMaterial.material);
        }
        else
        {
            return false;
        }

        return true;
    }

    private bool IsConquerLands() => Buffer != null && Buffer.Count >= 1;

    private bool IsColorNotCorrect()
    {
        foreach (var land in Buffer)
        {
            if (land.IsNotValidMaterial(_textureMaterial.material))
            {
                return true;
            }
        }

        return false;
    }

    private void PaintInside()
    {
        if (IsColorNotCorrect())
        {
            Die();
            return;
        }
        
        if (Buffer.Count > _minNumberFields)
        {
            List<Land> lands = new List<Land>();
            Vector3[] positions = new Vector3[Buffer.Count];

            for (int i = 0; i < Buffer.Count; i++)
            {
                positions[i] = Buffer[i].transform.position;
            }
        
            var minPoint = Calculate.FindMinPoint(positions);
            var maxPoint = Calculate.FindMaxPoint(positions);
            var iteration = Calculate.GetSquareArea(minPoint, maxPoint);
            var centerPoint = Calculate.FindCenter(minPoint, maxPoint);
            
            Buffer.AddRange(_map.TakeLands(ref lands, _textureMaterial.material, centerPoint));
        
            foreach (var variaLand in lands)
            {
                variaLand.SetMaterial(_textureMaterial.material);
            }
            
            positions = null;
            lands = null;
        }
        
        Lands.AddRange(Buffer);
        Buffer.Clear();
    }

    private void Die()
    {
        Lands.AddRange(Buffer);

        foreach (var land in Lands)
        {
            _map.SetDefaultMaterial(land);
        }

        Lands = null;
        Buffer = null;
        
        gameObject.SetActive(false);
    }
}