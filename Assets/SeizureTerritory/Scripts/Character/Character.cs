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
    private const float Speed = 5f;
    public float _ds;

    [SerializeField]
    private MonoBehaviour _inputSourceBehaviour;
    private ICharacterInputSource _inputSource;

    private CharacterController _characterController;
    private Colouring _colouring;

    private void OnEnable()
    {
        _colouring = new Colouring(GetComponent<Renderer>());
    }

    private void Start()
    {
        _inputSource = (ICharacterInputSource)_inputSourceBehaviour;
        _characterController = GetComponent<CharacterController>();
        _colouring.Spawn(transform);
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
        if (_colouring != null)
        {
            _colouring.SetMap(map);
        }
    }

    private void Die()
    {
        _colouring.Clean();
        gameObject.SetActive(false);
    }

    private void CheckLand(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Land land))
        {
            if (_colouring.ChangeLandMaterial(land))
            {
                _colouring?.AddBuffer(land);
            }
            else if (_colouring.IsConquerLands())
            {
                if (_colouring?.PaintInside() == false)
                {
                    Die();
                }
            }
        }
    }

    /*
    private bool ChangeLandMaterial(Land land)
    {
        if (_lands.Contains(land) == false)
        {
            land.SetMaterial(_textureMaterial.material);
        }
        else
        {
            return false;
        }

        return true;
    }

    private bool IsConquerLands() => _buffer != null && _buffer.Count >= 1;

    private bool IsColorNotCorrect()
    {
        foreach (var land in _buffer)
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

        if (_buffer.Count > _minNumberFields)
        {
            List<Land> lands = new List<Land>();
            Vector3[] positions = new Vector3[_buffer.Count];

            for (int i = 0; i < _buffer.Count; i++)
            {
                positions[i] = _buffer[i].transform.position;
            }

            var minPoint = Calculate.FindMinPoint(positions);
            var maxPoint = Calculate.FindMaxPoint(positions);
            var centerPoint = Calculate.FindCenter(minPoint, maxPoint);

            _buffer.AddRange(_map.TakeLands(ref lands, _textureMaterial.material, centerPoint));

            foreach (var variaLand in lands)
            {
                variaLand.SetMaterial(_textureMaterial.material);
            }

            positions = null;
            lands = null;
        }

        _lands.AddRange(_buffer);
        _buffer.Clear();
    }

    private void Spawn()
    {
        _lands = new List<Land>();
        _buffer = new List<Land>();
        _textureMaterial = GetComponent<Renderer>();

        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        RaycastHit[] hits = Physics.SphereCastAll(origin, _radius, direction, _distance);

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent(out Land land))
            {
                land.SetMaterial(_textureMaterial.material);
                _lands.Add(land);
            }
        }
    }
    */
}