using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotatingPlatformController : MonoBehaviour
{

    [SerializeField] float rotateTime;
    [SerializeField] float rotationPower;
    [SerializeField] GameObject platformModel;
    [SerializeField] Rigidbody rb;
    void Start()
    {
        rotatingPlatform();
    }

    void rotatingPlatform()
    {
        platformModel.transform.DORotate(new Vector3(0, 0, 360), rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.transform.RotateAround(transform.position,Vector3.forward,0.1f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.transform.SetParent(transform, true);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.transform.SetParent(null, true);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(transform.position,1f);
    }

}


