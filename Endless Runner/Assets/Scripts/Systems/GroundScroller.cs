using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    public Transform playerTrans; //We'll use this to detect when to spawn new tiles
    bool hasSpawnedNewTile; //Each ground tile only needs to spawn one more, this controls that

    // Start is called before the first frame update
    void Start()
    {
        if (playerTrans == null) //If nobody assigns the player transform...
        {
            playerTrans = FindObjectOfType<PlayerJump>().transform; //...find it
        }
    }

    private void LateUpdate()
    {
        if (GameManager.instance.gameActive) //We only need to spawn new tiles while the game is active
        {
            if (transform.position.x < playerTrans.position.x && !hasSpawnedNewTile) //If the ground tile passes the player and hasn't spawned a tile yet
            {
                FindObjectOfType<SpawnManager>().SpawnGround(transform);
                hasSpawnedNewTile = true; //Makes sure only one ground tile can be spawned
            }
        }
    }
}
