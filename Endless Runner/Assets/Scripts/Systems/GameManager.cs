using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Management")] //Controls thing like the game being active/paused
    public static GameManager instance; //This is the only scene in this project, but this keeps the possibility of other scenes in mind.
    public bool gameActive = true; //Can the player directly control the player character? Turned off for things like pause menus and end states

    [Header("Score Variables")]
    public int globalCoinCount = 0; //How many coins the player has collected over *all* play sessions, updated from CoinManager.cs

    [Header("Game Variables")]
    public float gameSpeed = 1; //The speed at which objects will travel to the left. A factor of the game's difficulty

    [Header("Components")]
    ScoreManager scoreManager; //CoinManagers are per run (they aren't DDOL), but eventually save to globalCoinCount


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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BeginGamePrep();
    }

    void BeginGamePrep() //Move all the preparation stuff into its own function
    {
        gameActive = true;
    }

    public void UpdateCoinCountersInCoinManager(int coinValue)
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.AddCoinToCounters(coinValue);
        globalCoinCount = scoreManager.globalCoinCount;
    }

    public void StartGameOverSequence()
    {
        GameOverManager goMan = FindObjectOfType<GameOverManager>(); //GameOverManager is per level because it contains references to objects in their specific scene
        scoreManager = FindObjectOfType<ScoreManager>();
        goMan.UpdateGameOverText(scoreManager.distanceTraveled, scoreManager.localCoinCount, scoreManager.globalCoinCount, scoreManager.score);
        goMan.StartCoroutine("GameOverSequence");
    }
}
