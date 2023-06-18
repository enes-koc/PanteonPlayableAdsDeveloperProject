using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    MenuManager menuManager;
    int coinAmount=0;

    private void Start() {
        menuManager = MenuManager.Instance;
    }

    public void increaseCoin(int increaseAmount){
        coinAmount+=increaseAmount;
        menuManager.SetCurrency(coinAmount);
    }
}
