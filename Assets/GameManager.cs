using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     public static GameManager Instance { get; private set; }

    public enum GameState
    {
        Menu,
        Racing,
        Painting,
        GameOver
    }

    public GameState CurrentGameState { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SetGameState(GameState.Menu);
    }

    public void StartRace()
    {
        SetGameState(GameState.Racing);
    }

    public void StartPainting()
    {
        SetGameState(GameState.Painting);
    }

    public void EndGame()
    {
        SetGameState(GameState.GameOver);
    }

    void SetGameState(GameState state)
    {
        CurrentGameState = state;

        switch (CurrentGameState)
        {
            case GameState.Menu:
                // Menü işlemleri
                break;
            case GameState.Racing:
                // Yarış işlemleri
                break;
            case GameState.Painting:
                // Boyama işlemleri
                break;
            case GameState.GameOver:
                // Oyun bitiş işlemleri
                break;
        }
    }
}
