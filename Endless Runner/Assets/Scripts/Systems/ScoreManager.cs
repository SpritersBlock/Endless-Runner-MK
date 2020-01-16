using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Game Management")] 
    public static ScoreManager instance;

    [Header("Coin Managenent")]
    public int localCoinCount; //How many coins the player has collected since the *level* has started
    public int globalCoinCount = 0; //How many coins the player has collected over *all* play sessions
    [Space]
    [SerializeField] CoinCountText localCoinCountText; //The text displaying the local coin count
    [SerializeField] CoinCountText globalCoinCountText; //The text displaying the global coin count

    [Header("Distance Management")]
    public int distanceTraveled; //How far the player has traveled
    [SerializeField] float howSmallIsAMile = 0.5f; //How often does the distance score update?

    [Header("Score Management")]
    public int score; //The player's current score
    [SerializeField] TextMeshProUGUI scoreText; //The text that shows the player's current score

    private void Awake()
    {
        // vvv Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        // ^^^ Singleton
    }

    private void Start()
    {
        globalCoinCount = GameManager.instance.globalCoinCount;
        FindUITexts();
        Invoke("UpdateCoinCounterText", 0.0005f); //For some reason calling the function normally causes a NullRefException error but invoking it is fine. Will look into later
        StartCoroutine(UpdateDistance());
        localCoinCount = 0;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.gameActive) //Update the score when the game is being played
        {
            score = distanceTraveled + localCoinCount; //Adds collected coins to distance score for final score
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
            yield return new WaitForSeconds(howSmallIsAMile);
            distanceTraveled += 1;
        }

        yield return null;
    }

    void FindUITexts() //If, by any chance, the CoinCountTexts are null, find them.
    {
        if (!localCoinCountText)
        {
            localCoinCountText = GameObject.Find("CoinCountTxt").GetComponent<CoinCountText>();
        }
        if (!globalCoinCountText)
        {
            globalCoinCountText = GameObject.Find("TotalCoinCountTxt").GetComponent<CoinCountText>();
        }
        if (!scoreText)
        {
            scoreText = GameObject.Find("ScoreTxt").GetComponent<TextMeshProUGUI>();
        }
    }

    public void AddCoinToCounters(int coinValue) //Adds coin value to both local and global coin count
    {
        localCoinCount += coinValue; //Adds coin value to local coin count
        globalCoinCount += coinValue; //Adds coin value to global coin count
        UpdateCoinCounterText(); //Gives the player the visual feedback of collecting coins and adding to score
    }

    public void UpdateCoinCounterText() //Gives the player the visual feedback of collecting coins and adding to score
    {
        localCoinCountText.UpdateInfo(localCoinCount);
        globalCoinCountText.UpdateInfo(globalCoinCount);
    }
}
