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
    float timer;
    [SerializeField] TextMeshProUGUI scoreText; //The text that shows the player's current score

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        GameSetUp();
    }

    public void GameSetUp()
    {
        timer = howSmallIsAMile;
        globalCoinCount = GameManager.instance.globalCoinCount;
        localCoinCount = 0;
        distanceTraveled = 0;
        score = 0;
        FindUITexts();
        UpdateCoinCounterText();
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.gameActive) //Update the score when the game is being played
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                distanceTraveled += 1;
                timer = howSmallIsAMile;
                UpdateScore();
            }
            
        }
    }

    void UpdateScore()
    {
        FindUITexts();
        score = distanceTraveled + localCoinCount;
        scoreText.text = score.ToString();
    }

    public void FindUITexts() //If, by any chance, the CoinCountTexts are null, find them.
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

    public void AddCoinToCounters(int coinValue) //Adds coin value to both local and global coin count vars
    {
        localCoinCount += coinValue;
        globalCoinCount += coinValue;
        UpdateCoinCounterText();
    }

    public void UpdateCoinCounterText() //Gives the player the visual feedback of collecting coins and adding to score
    {
        localCoinCountText.UpdateInfo(localCoinCount);
        globalCoinCountText.UpdateInfo(globalCoinCount);
        UpdateScore();
    }
}
