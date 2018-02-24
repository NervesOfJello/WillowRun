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
    public Ent[] Players;

    private void Start()
    {
        _gameState = GameState.Start;
    }

    private void LateUpdate()
    {
        _gameState = GameState.Playing;
        CheckPlayersStates();
        Debug.Log("Game State:" + _gameState);
    }

    int count;
    private void CheckPlayersStates()
    {
        foreach (var player in Players)
        {
            if(player.PlayerState == PlayerState.Dead)
            {
                count++;
            }
        }

        if (count == 3)
        {
            _gameState = GameState.Won;
        }
        else if(count == 4)
        {
            _gameState = GameState.Draw;
        }
    }
}
