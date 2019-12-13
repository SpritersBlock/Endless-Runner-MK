using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    int localCoinCount; //How many coins the player has collected since the *level* has started
    public int globalCoinCount = 0; //How many coins the player has collected over *all* play sessions

    public CoinCountText localCoinCountText; //The text displaying the local coin count
    public CoinCountText globalCoinCountText; //The text displaying the global coin count

    private void Start()
    {
        globalCoinCount = GameManager.instance.globalCoinCount;
        FindCoinCountTexts();
        Invoke("UpdateCoinCounterText", 0.0005f); //For some reason calling the function normally causes a NullRefException error but invoking it is fine. Will look into later
    }

    void FindCoinCountTexts() //If, by any chance, the CoinCountTexts are null, find them.
    {
        localCoinCountText = GameObject.Find("CoinCountTxt").GetComponent<CoinCountText>();
        globalCoinCountText = GameObject.Find("TotalCoinCountTxt").GetComponent<CoinCountText>();
    }

    public void AddCoinToCounters(int coinValue) //Adds coin value to both local and global coin count
    {
        localCoinCount += coinValue; //Adds coin value to local coin count
        globalCoinCount += coinValue; //Adds coin value to global coin count
        UpdateCoinCounterText();
    }

    public void UpdateCoinCounterText()
    {
        localCoinCountText.UpdateInfo(localCoinCount);
        globalCoinCountText.UpdateInfo(globalCoinCount);
    }

    public void DeactivateCoinCounterTexts()
    {
        localCoinCountText.gameObject.SetActive(false);
        globalCoinCountText.gameObject.SetActive(false);
    }
}
