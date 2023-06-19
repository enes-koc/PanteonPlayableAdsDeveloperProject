using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PendulumController : MonoBehaviour
{
    [SerializeField] float rotateTime;
    [SerializeField] float swingAngle;
    [SerializeField] GameObject pendulumModel;
    [SerializeField] StartPosition startPosition = StartPosition.Left;
    [SerializeField] List<GameObject> leftPenaltyList, rightPenaltyList;
    void Start()
    {
        if (startPosition == StartPosition.Left)
        {
            pendulumModel.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -swingAngle));
        }
        else
        {
            pendulumModel.transform.rotation = Quaternion.Euler(new Vector3(0, 0, swingAngle));
        }
        HandleMovement();
    }

    void HandleMovement()
    {
        pendulumModel.transform.DOLocalRotate(new Vector3(0, 0, (float)startPosition * swingAngle), rotateTime,RotateMode.Fast).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic);
    }

     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().GoSpawnPoint();
        }
        else if (other.CompareTag("Opponent"))
        {
            other.GetComponent<OpponentCharacterControllerAstar>().GoSpawnPoint();
        }
    }

    enum StartPosition
    {
        Left = 1,
        Right = -1
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
}
