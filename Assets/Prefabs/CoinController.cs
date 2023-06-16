using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] float rotateTime;
    [SerializeField] GameObject coinObj;
    [SerializeField] List<GameObject> tempcoinObjList;
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        coinRotate();
    }

    void coinRotate()
    {
        transform.DORotate(new Vector3(0, 360, 0), rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider collider)
    {
        GameObject tempCoin;
        if (collider.CompareTag("Player"))
        {
            for (int i = 0; i < 5; i++)
            {
                tempCoin = Instantiate(coinObj,transform.position+new Vector3(Random.Range(1f,-1f),Random.Range(1f,-1f),Random.Range(1f,-1f)),Quaternion.Euler(90,0,0));
                tempCoin.transform.SetParent(mainCamera.transform);
                tempcoinObjList.Add(tempCoin);
            }
            
            foreach (var coin in tempcoinObjList)
            {
                coin.transform.DOLocalMove(new Vector3(6, 18.8f, 21),1f).SetEase(Ease.InFlash);
                Destroy(coin, 2f);
            }
        }
        transform.GetChild(0).gameObject.SetActive(false);
        Destroy(this.gameObject,5f);
    }
}
