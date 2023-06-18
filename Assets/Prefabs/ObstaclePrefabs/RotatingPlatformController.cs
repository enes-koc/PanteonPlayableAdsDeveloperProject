using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotatingPlatformController : MonoBehaviour
{

    [SerializeField] float rotateTime;
    [SerializeField] GameObject platformModel;
    [SerializeField] RotationDirection rotationDirection;

    public Vector3 rotatingPlatformDestination;
    void Start()
    {
        rotatingPlatform();
    }

    void rotatingPlatform()
    {

        platformModel.transform.DORotate(new Vector3(0, 0, (float)rotationDirection), rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    private void OnDrawGizmos() {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(rotatingPlatformDestination,0.6f);
    }

    enum RotationDirection{
        Left = 360,
        Right=-360
    }

}


