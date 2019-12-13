using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Coin Managenent")]
    public int localCoinCount; //How many coins the player has collected since the *level* has started
    public int globalCoinCount = 0; //How many coins the player has collected over *all* play sessions
    [Space]
    public CoinCountText localCoinCountText; //The text displaying the local coin count
    public CoinCountText globalCoinCountText; //The text displaying the global coin count

    [Header("Distance Management")]
    public int distanceTraveled; //How far the player has traveled

    [Header("Score Management")]
    public int score; //The player's current score
    public TextMeshProUGUI scoreText; //The text that shows the player's current score

    private void Start()
    {
        globalCoinCount = GameManager.instance.globalCoinCount;
        FindUITexts();
        Invoke("UpdateCoinCounterText", 0.0005f); //For some reason calling the function normally causes a NullRefException error but invoking it is fine. Will look into later
        StartCoroutine("UpdateDistance");
        localCoinCount = 0;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.gameActive) //Update the score when the game is being played
        {
            score = distanceTraveled + localCoinCount;
            UpdateScore();
        }
    }

    void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    IEnumerator UpdateDistance()
    {
        while (GameManager.instance.gameActive)
        {
            yield return new WaitForSeconds(0.5f);
            distanceTraveled += 1;
        }

        yield return null;
    }

    void FindUITexts() //If, by any chance, the CoinCountTexts are null, find them.
    {
        localCoinCountText = GameObject.Find("CoinCountTxt").GetComponent<CoinCountText>();
        globalCoinCountText = GameObject.Find("TotalCoinCountTxt").GetComponent<CoinCountText>();
        scoreText = GameObject.Find("ScoreTxt").GetComponent<TextMeshProUGUI>();
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
}
