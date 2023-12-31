using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().GoSpawnPoint();
        }
        else if (other.CompareTag("Opponent"))
        {
            other.GetComponent<OpponentCharacterControllerAstar>().GoSpawnPoint();
        }
    }
}
