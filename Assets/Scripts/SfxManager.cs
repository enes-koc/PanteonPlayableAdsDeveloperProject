using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    [SerializeField] AudioClip boingSfx, coinSfx;
    [SerializeField] AudioSource audioSource;


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            audioSource.clip = boingSfx;
            audioSource.Play();
        }
        else if (other.gameObject.CompareTag("Coin"))
        {
            audioSource.clip = coinSfx;
            audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Obstacle"))
        {
            audioSource.clip = boingSfx;
            audioSource.Play();
        }
        else if (other.gameObject.CompareTag("Coin"))
        {
            audioSource.clip = coinSfx;
            audioSource.Play();
        }
    }
}
