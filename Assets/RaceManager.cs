using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance { get; private set; }
    [SerializeField] List<GameObject> racers;
    [SerializeField] List<Transform> raceStartLocations;
    [SerializeField] List<Transform> raceEndLocations;
    GameObject player;
    List<GameObject> higherRanks;
    MenuManager menuManager;

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
        menuManager = MenuManager.Instance;
    }

    private void FixedUpdate()
    {
        if (gameManager.State == GameState.Racing) CalculateCurrentRank();
    }

    void EndRace()
    {
        gameManager.UpdateGameState(GameState.Painting);
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

        menuManager.SetPlayerRank(higherRanks.Count + 1, racers.Count);
    }

    public void AddRacer(GameObject racer)
    {
        Transform randomStartPos = raceStartLocations[Random.Range(0, raceStartLocations.Count)];

        if (racer.CompareTag("Player"))
        {
            player = racer;
            player.GetComponent<PlayerController>().raceStartPosition = randomStartPos;
            player.GetComponent<PlayerController>().OnFinishRace += EndRace;
        }
        else if (racer.CompareTag("Opponent"))
        {

            racer.GetComponent<OpponentCharacterControllerAstar>().raceStartPosition = randomStartPos;
            Transform randomEndPos = raceEndLocations[Random.Range(0, raceEndLocations.Count)];
            racer.GetComponent<OpponentCharacterControllerAstar>().raceEndPosition = randomEndPos;
            raceEndLocations.Remove(randomEndPos);
        }
        racers.Add(racer);
        raceEndLocations.Remove(randomStartPos);

    }



}
