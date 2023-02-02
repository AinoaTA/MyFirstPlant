using System;
using System.Collections;
using System.Collections.Generic;
using Cutegame;
using UnityEditor;
using UnityEngine;

public class GameTest : MonoBehaviour
{
    public Minigame game;
    public bool winGame = false;
    public void Start()
    {
        game.Setup(StartGame, EndGame);
        game.StartMinigame();
    }

    public void StartGame()
    {
        Debug.Log($"Starting {game.name}");
    }

    private void EndGame(int points)
    {
        Debug.Log($"Ending game with {points} points.");
    }
    

    private void OnValidate()
    {
        if (winGame)
        {
            winGame = false;
            game.WinGame();
        }
    }
}
