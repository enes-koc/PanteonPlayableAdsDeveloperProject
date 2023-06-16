using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotatingPlatformController : MonoBehaviour
{

    [SerializeField] float rotateTime;
    [SerializeField] GameObject platformModel;
    public Vector3 rotatinPlatformDestination;
    void Start()
    {
        rotatingPlatform();
    }

    void rotatingPlatform()
    {
        platformModel.transform.DORotate(new Vector3(0, 0, 360), rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    private void OnDrawGizmos() {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(rotatinPlatformDestination,2f);
    }

}


