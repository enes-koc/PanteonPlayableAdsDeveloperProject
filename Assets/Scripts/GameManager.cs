using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        Screen.SetResolution(1080, 1920, true);
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

                break;
            case GameState.Racing:

                break;
            case GameState.Painting:

                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame(){
        Application.Quit();
    }
}

public enum GameState
{
    Start,
    Racing,
    Painting
}
