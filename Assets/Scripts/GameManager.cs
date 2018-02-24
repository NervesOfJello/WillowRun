using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState { Start, Playing, Won, Navagiting, Draw}

public class GameManager : MonoBehaviour
{
    private GameState _gameState;
    private const int GameRounds = 3;

    //used to keep track of the players states
    public List<Ent> Players = new List<Ent>();

    private void Start()
    {
        _gameState = GameState.Playing;
    }

    private void FixedUpdate()
    {
        Debug.Log("Game State:" + _gameState);
        CheckPlayersStates();
        if (_gameState == GameState.Draw || _gameState == GameState.Won)
        {
            Debug.Log("Game State:" + _gameState);
        }
        
    }

    int count;
    private void CheckPlayersStates()
    {
        foreach (var player in Players)
        {
            if(player.PlayerState == PlayerState.Dead)
            {
                Players.Remove(player);
                count++;
            }
        }

        if (count == 3)
        {
            Players[0].RoundsWon++;
            Debug.Log("Player" + Players[0].name);
            _gameState = GameState.Won;
        }
        else if(count == 4)
        {
            _gameState = GameState.Draw;
        }
    }
}
