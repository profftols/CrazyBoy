using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(CharacterController))]
public class Character : MonoBehaviour
{
    protected const float Speed = 4f;

    public float BonusSpeed;
    public bool IsInvulnerable;
    
    private Colouring _colouring;
    private Map _map;

    private List<Land> _lands;
    private List<Land> _buffer;

    protected virtual void Start()
    {
        _lands = new List<Land>();
        _buffer = new List<Land>();
        _colouring = new Colouring(GetComponent<Renderer>());
        _lands.AddRange(_colouring.Spawn(transform));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Land land))
        {
            if (_colouring != null)
            {
                CheckLand(land);
            }
        }

        if (other.gameObject.TryGetComponent(out Item item))
        {
            item.OnPickUp(this);
        }
    }

    public void SetMap(Map map)
    {
        _map = map;
    }

    private void Die()
    {
        if (IsInvulnerable)
        {
            return;
        }
        
        _lands.AddRange(_buffer);

        foreach (var land in _lands)
        {
            _map.SetDefaultMaterial(land);
        }

        _lands = null;
        _buffer = null;
        gameObject.SetActive(false);
    }

    private void CheckLand(Land land)
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
                if (IsInvulnerable)
                {
                    return;
                }

                Die();
                return;
            }

            CompleteLand();
        }
    }

    private void CompleteLand()
    {
        _lands.AddRange(_buffer);
        var lands = _map.TakeLands(_lands);
        _colouring.PaintInside(lands);
        _lands.AddRange(lands);
        _buffer.Clear();
    }
}