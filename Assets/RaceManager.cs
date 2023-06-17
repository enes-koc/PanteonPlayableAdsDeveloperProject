using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance { get; private set; }
    [SerializeField] List<GameObject> racers;
    GameObject player;
    List<GameObject> higherRanks;

    public float playerCurrentRank { get; private set; }

    GameManager gameManager;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        racers = new List<GameObject>();
        higherRanks = new List<GameObject>();

    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void FixedUpdate()
    {
        if (gameManager.State == GameState.Racing) CalculateCurrentRank();
    }

    void EndRace()
    {
        gameManager.UpdateGameState(GameState.Painting);
        print("END RACE");
    }

    void CalculateCurrentRank()
    {
        higherRanks.Clear();
        float playerCurrentZpos = player.transform.position.z;
        racers.ForEach(opponentRacer =>
        {
            if (opponentRacer.transform.position.z > playerCurrentZpos)
            {
                higherRanks.Add(opponentRacer);
            }
        });

        playerCurrentRank = higherRanks.Count + 1;
    }

    public void AddRacer(GameObject racer)
    {
        if (racer.CompareTag("Player"))
        {
            player = racer;
            player.GetComponent<PlayerController>().OnFinishRace += EndRace;
        }
        racers.Add(racer);
    }



}
