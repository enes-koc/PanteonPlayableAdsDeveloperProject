using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotatorController : MonoBehaviour
{
    [SerializeField] float rotateTime;
    [SerializeField] float bounceForce;

    void Start()
    {
        rotator();
    }

    void rotator()
    {
        transform.DORotate(new Vector3(0, 360, 0), rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<PlayerController>().GoSpawnPoint();
        }
        else if (collider.CompareTag("Opponent"))
        {
            collider.GetComponent<OpponentCharacterControllerAstar>().GoSpawnPoint();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Opponent"))
        {
            print(collision.gameObject.name);
            ContactPoint firstContact = collision.contacts[0];
            Vector3 contactPoint = firstContact.point;
            var rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.AddForce((collision.transform.position - contactPoint) * bounceForce, ForceMode.Impulse);
        }
    }

}
