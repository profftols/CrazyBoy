using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private List<Character> _players;

    private void Start()
    {
        _players = new List<Character>();
    }

    public void AddPlayer(Character character)
    {
        _players.Add(character);
    }
}
