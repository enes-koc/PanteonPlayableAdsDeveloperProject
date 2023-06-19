using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] int coinAmount;
    [SerializeField] int popoutCoinCount;
    [SerializeField] float rotateTime;
    [SerializeField] float coinArrivalTime;
    [SerializeField] GameObject targetCoinImage;
    [SerializeField] GameObject coinImage;
    [SerializeField] RectTransform canvas;


    void Start()
    {
        coinRotate();
    }

    void coinRotate()
    {
        transform.DORotate(new Vector3(0, 360, 0), rotateTime, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<CurrencyManager>().increaseCoin(coinAmount);
            coinAnimation();
            this.gameObject.SetActive(false);
            Destroy(this.gameObject, coinArrivalTime);
        }
    }

    void coinAnimation()
    {
        GameObject tempCoin;
        for (int i = 0; i < popoutCoinCount; i++)
        {
            tempCoin = Instantiate(coinImage, transform.position, Quaternion.Euler(0, 0, 0));
            tempCoin.transform.parent = canvas.transform;
            tempCoin.GetComponent<RectTransform>().anchoredPosition = transform.position + new Vector3(Random.Range(90f, -90f), Random.Range(50f, -140f), Random.Range(90f, -90f));
            tempCoin.transform.DOMove(targetCoinImage.transform.position, coinArrivalTime).SetEase(Ease.InFlash);
            Destroy(tempCoin, coinArrivalTime);
        }
    }

    // if (collider.CompareTag("Player"))
    // {
    //     //StartCoroutine(test(collider.gameObject));
    //     Vector3 screenPoint = targetUIElement.transform.position + new Vector3(0, 0, 35);  //the "+ new Vector3(0,0,5)" ensures that the object is so close to the camera you dont see it
    //     Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);

    //     GameObject tempCoin;
    //     for (int i = 0; i < 5; i++)
    //     {
    //         tempCoin = Instantiate(coinObj, transform.position + new Vector3(Random.Range(1f, -1f), Random.Range(1f, -1f), Random.Range(1f, -1f)), Quaternion.Euler(90, 0, 0));
    //         tempCoin.transform.DOMove(worldPos, coinArrivalTime).SetEase(Ease.InFlash);
    //         tempCoin.transform.DOScale(0.6f, coinArrivalTime);
    //         Destroy(tempCoin, coinArrivalTime + 0.1f);
    //     }
    //     this.gameObject.SetActive(false);
    //     Destroy(this.gameObject, 5f);
    // }



    // IEnumerator test(GameObject player)
    // {

    //     List<GameObject> tempCoinList = new List<GameObject>();
    //     GameObject tempCoin;
    //     for (int i = 0; i < 5; i++)
    //     {
    //         tempCoin = Instantiate(coinObj, transform.position + new Vector3(Random.Range(1f, -1f), Random.Range(1f, -1f), Random.Range(1f, -1f)), Quaternion.Euler(90, 0, 0));
    //         tempCoinList.Add(tempCoin);
    //     }

    //     for (int i = 0; i < 5; i++)
    //     {
    //         Vector3 screenPoint = targetUIElement.transform.position + new Vector3(0, 0, 35);
    //         Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);
    //         foreach (var coin in tempCoinList)
    //         {
    //             coin.transform.DOMove(worldPos, coinArrivalTime);
    //             //coin.transform.DOScale(0.6f, coinArrivalTime);
    //         }
    //         yield return new WaitForSeconds(0.1f);
    //     }
    // }
}



