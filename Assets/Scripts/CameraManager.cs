using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera playerCamera;
    [SerializeField] CinemachineVirtualCamera paintingCamera;
    public static CameraManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    
    private void GameManagerOnGameStateChanged(GameState state)
    {
        playerCamera.gameObject.SetActive(state == GameState.Start);
        playerCamera.gameObject.SetActive(state == GameState.Racing);
        paintingCamera.gameObject.SetActive(state == GameState.Painting);
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
}
