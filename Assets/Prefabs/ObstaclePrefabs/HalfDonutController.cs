using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HalfDonutController : MonoBehaviour
{
    [SerializeField] GameObject movingStick;
    [SerializeField] float delayTime;
    [SerializeField] bool randomTime;
    
    void Start()
    {
        halfDonut();
    }

    void halfDonut()
    {
        float time = 0;
        if (randomTime)
        {
            time = Random.Range(0.6f, 1.6f);
        }
        else
        {
            time=delayTime;
        }

        var halfDonutSequance = DOTween.Sequence();
        halfDonutSequance.SetDelay(time).Append(movingStick.transform.DOLocalMove(movingStick.transform.localPosition + new Vector3(-0.23f, 0, 0), 1));
        halfDonutSequance.SetLoops(-1, LoopType.Yoyo);
    }
}