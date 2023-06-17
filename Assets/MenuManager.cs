using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject startPanel,joystickPanel;

    public static MenuManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        GameManager.OnGameStateChanged+=GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        startPanel.SetActive(state==GameState.Start);
        joystickPanel.SetActive(state==GameState.Racing);
    }

    public void TapToStart(){
        GameManager.Instance.UpdateGameState(GameState.Racing);
        startPanel.SetActive(false);
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged-=GameManagerOnGameStateChanged;
    }
}
