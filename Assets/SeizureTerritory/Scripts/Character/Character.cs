using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Object = UnityEngine.Object;

[RequireComponent(typeof(Renderer), typeof(CharacterController))]
public class Character : MonoBehaviour
{
    private const float Speed = 6f;

    [SerializeField]
    private MonoBehaviour _inputSourceBehaviour;
    private ICharacterInputSource _inputSource;

    private CharacterController _characterController;
    private Colouring _colouring;
    private Map _map;
    
    private List<Land> _lands;
    private List<Land> _buffer;

    private void Start()
    {
        _lands = new List<Land>();
        _buffer = new List<Land>();
        _colouring = new Colouring(GetComponent<Renderer>());
        _inputSource = (ICharacterInputSource)_inputSourceBehaviour;
        _characterController = GetComponent<CharacterController>();
        _lands.AddRange(_colouring.Spawn(transform));
    }

    private void Update()
    {
        var movement = new Vector3(_inputSource.MovementInput.x, 0f, _inputSource.MovementInput.y);
        movement *= Speed;
        _characterController.SimpleMove(movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_colouring != null)
        {
            CheckLand(other);
        }
    }
    
    private void OnValidate()
    {
        if (_inputSourceBehaviour && !(_inputSourceBehaviour is ICharacterInputSource))
        {
            Debug.LogError(nameof(_inputSourceBehaviour) + " needs to implement " + nameof(ICharacterInputSource));
            _inputSourceBehaviour = null;
        }
    }

    public void SetMap(Map map)
    {
        _map = map;
    }

    private void Die()
    {
        _lands.AddRange(_buffer);

        foreach (var land in _lands)
        {
            _map.SetDefaultMaterial(land);
        }

        _lands = null;
        _buffer = null;
        gameObject.SetActive(false);
    }

    private void CheckLand(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Land land))
        {
            if (_colouring.IsChangeLandMaterial(land, _lands))
            {
                if (_buffer.Contains(land))
                {
                    Die();
                    return;
                }

                _buffer.Add(land);
            }
            else if (Calculation.IsValidLands(_buffer))
            {
                if (_colouring.IsColorNotCorrect(_buffer))
                {
                    Die();
                    return;
                }
                
                _lands.AddRange(_buffer);
                var lands = _map.TakeLands(_lands);
                _colouring.PaintInside(lands);
                _lands.AddRange(lands);
                _buffer.Clear();
            }
        }
    }
}