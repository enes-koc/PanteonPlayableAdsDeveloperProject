using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShiningController : MonoBehaviour
{
    [SerializeField] float xAxisMoveAmount;
    [SerializeField] float rotateTime;
    [SerializeField] GameObject obstacleModel;
    new ParticleSystem particleSystem;
    Rigidbody rb;
    Vector3 lastPosition;

    bool isGoingLeft;

    [SerializeField] List<GameObject> leftPenaltyList, rightPenaltyList;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        particleSystem = GetComponent<ParticleSystem>();
        HandleMovement();
    }

    private void FixedUpdate()
    {
        isGoingLeft = transform.position.x - lastPosition.x < 0 ? true : false;
        HandlePenalty();
        lastPosition = transform.position;
    }

    void HandleMovement()
    {
        transform.DOMove(transform.position + new Vector3(xAxisMoveAmount, 0, 0), 2).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        obstacleModel.transform.DORotate(new Vector3(0, 360, 0), rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    void HandlePenalty()
    {
        if (isGoingLeft)
        {
            for (int i = 0; i < leftPenaltyList.Count; i++)
            {
                leftPenaltyList[i].gameObject.SetActive(true);
                rightPenaltyList[i].gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < leftPenaltyList.Count; i++)
            {
                leftPenaltyList[i].gameObject.SetActive(false);
                rightPenaltyList[i].gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var mainModule = particleSystem.main;
            mainModule.startColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            other.GetComponent<PlayerController>().GoSpawnPoint();
        }
        else if (other.CompareTag("Opponent"))
        {
            var mainModule = particleSystem.main;
            mainModule.startColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            other.GetComponent<OpponentCharacterControllerAstar>().GoSpawnPoint();
        }
    }
}
