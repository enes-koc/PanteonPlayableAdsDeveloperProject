using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    
    void Start()
    {
        TextImpulseEffect();
    }

    
    void TextImpulseEffect(){
        transform.DOScale(2,1).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
    }
}
