using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState { Start, Playing, Won }

public class GameManager : MonoBehaviour
{
    private GameState _gameState;
    private const int GameRounds = 3;

    //used to keep track of the players states
    public GameObject[] Players;

    private void LateUpdate()
    {
        CheckPlayersStates();
    }

    private void CheckPlayersStates()
    {
        
    }
}
