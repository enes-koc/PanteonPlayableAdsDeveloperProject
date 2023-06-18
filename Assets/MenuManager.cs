using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject startPanel,paintingPanel,racingPanel,pauseGamePanel;
    [SerializeField] TextMeshProUGUI paintPercentageText,playerRankText,playerFailText,playerCurrencyText;

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
        racingPanel.SetActive(state==GameState.Racing);
        paintingPanel.SetActive(state==GameState.Painting);
    }

    public void TapToStart(){
        GameManager.Instance.UpdateGameState(GameState.Racing);
        startPanel.SetActive(false);
    }

    public void SetPercentage(float percentage){
        paintPercentageText.text=percentage.ToString("0")+"%";
    }

    public void SetPlayerRank(int playerRank, int racersCount){
        playerRankText.text="RANK \n"+playerRank.ToString("0")+"/"+racersCount;
    }

    public void SetFailAttempt(int failAttempt){
        playerFailText.text=failAttempt.ToString("0")+" Failed";
    }

    public void SetCurrency(int currencyAmount){
        playerCurrencyText.text=currencyAmount.ToString("0");
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged-=GameManagerOnGameStateChanged;
    }
}
