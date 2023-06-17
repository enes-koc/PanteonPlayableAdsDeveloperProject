using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState State;

    public static event Action<GameState> OnGameStateChanged;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        UpdateGameState(GameState.Start);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (State)
        {
            case GameState.Start:
                HandleStart();
                break;
            case GameState.Racing:
                HandleRacing();
                break;
            case GameState.Painting:
                // Boyama işlemleri
                break;
            case GameState.GameOver:
                // Oyun bitiş işlemleri
                break;
        }
        print(State);
        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleRacing()
    {
       
    }

    private void HandleStart()
    {
       
    }
}

public enum GameState
{
    Start,
    Racing,
    Painting,
    GameOver
}
