using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCountText : MonoBehaviour
{
    //This script shows the player the number of coins collected, either per run or globally. This modifies the text only.

    TextMeshProUGUI coinCountText; //The on-screen text for coin count.
    public string prefix = "Count: "; //So that we can easily edit the prefix from the editor instead of it being hard-coded in

    void Start()
    {
        coinCountText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateInfo(int newCoinTotal)
    {
        coinCountText.text = prefix + newCoinTotal;
    }
}
