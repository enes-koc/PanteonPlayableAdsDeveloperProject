using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotatorController : MonoBehaviour
{
    [SerializeField] float rotateTime;
    [SerializeField] float bounceForce;
    [SerializeField] RotationDirection rotationDirection = RotationDirection.Clockwise;
    [SerializeField] List<GameObject> CounterClockwisePenaltyList, ClockwisePenaltyList;

    void Start()
    {
        foreach (var penaltyNode in CounterClockwisePenaltyList)
        {
            penaltyNode.SetActive(rotationDirection == RotationDirection.CounterClockwise);
        }
        foreach (var penaltyNode in ClockwisePenaltyList)
        {
            penaltyNode.SetActive(rotationDirection == RotationDirection.Clockwise);
        }
        HandleMovement();
    }

    void HandleMovement()
    {
        transform.DORotate(new Vector3(0, (float)rotationDirection * 360, 0), rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
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
            ContactPoint firstContact = collision.contacts[0];
            Vector3 contactPoint = firstContact.point;
            var rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.AddForce((collision.transform.position - contactPoint) * bounceForce, ForceMode.Impulse);
        }
    }

    enum RotationDirection
    {
        Clockwise = 1,
        CounterClockwise = -1
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }

}
