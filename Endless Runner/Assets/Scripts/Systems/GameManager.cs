using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Management")] //Controls thing like the game being active/paused
    public static GameManager instance; //This is the only scene in this project so this may not be strictly necessary, but this keeps the possibility of other scenes in mind.
    public bool gameActive = true; //Can the player directly control the player character? Turned off for things like pause menus and end states

    [Header("Score Variables")]
    int localCoinCount = 0; //How many coins the player has collected since the *level* has started
    int globalCoinCount = 0; //How many coins the player has collected over *all* play sessions

    public CoinCountText localCoinCountText; //The text displaying the local coin count
    public CoinCountText globalCoinCountText; //The text displaying the global coin count

    [Header("Game Variables")]
    public float gameSpeed = 1; //The speed at which objects will travel to the left. A factor of the game's difficulty

    private void Awake()
    {
        // V Singleton
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
        // ^ Singleton
    }

    private void Start()
    {
        BeginGamePrep(); //Move all the preparation stuff into its own function
    }

    void BeginGamePrep() 
    {
        UpdateCoinCounters(); //Make sure the counters know how many coins the player has at the start of the level
    }

    public void AddCoinToCounters(int coinValue) //Adds coin value to both local and global coin count
    {
        localCoinCount += coinValue; //Adds coin value to local coin count
        globalCoinCount += coinValue; //Adds coin value to global coin count
        UpdateCoinCounters();
    }

    public void UpdateCoinCounters()
    {
        localCoinCountText.UpdateInfo(localCoinCount); //Updates local coin count text
        globalCoinCountText.UpdateInfo(globalCoinCount); //Updates global coin count text
    }
}
