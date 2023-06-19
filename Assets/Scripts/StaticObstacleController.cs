using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacleController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().GoSpawnPoint();
        }
        else if (collision.gameObject.CompareTag("Opponent"))
        {
            collision.gameObject.GetComponent<OpponentCharacterControllerAstar>().GoSpawnPoint();
        }
    }
}
