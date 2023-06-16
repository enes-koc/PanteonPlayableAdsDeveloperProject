using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShiningController : MonoBehaviour
{
    [SerializeField] float xAxisMoveAmount;
    [SerializeField] float rotateTime;
    new ParticleSystem particleSystem;
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        shining();
    }

    void shining()
    {
        transform.DOMove(transform.position + new Vector3(xAxisMoveAmount, 0, 0), 2).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(new Vector3(0, 360, 0), rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var mainModule = particleSystem.main;
            mainModule.startColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }
}
