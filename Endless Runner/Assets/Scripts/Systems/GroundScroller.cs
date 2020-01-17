using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    //Ground tiles get their own scrolling script due to the fact that they are the one element that spawns consistently and with no randomness factored in.

    float playerXPos;
    bool hasSpawnedNewTile;

    void Start()
    {
        playerXPos = SpawnManager.instance.playerXPos;
    }

    private void LateUpdate()
    {
        if (GameManager.instance.gameActive)
        {
            if (transform.position.x < playerXPos && !hasSpawnedNewTile) //If the ground tile passes the player and hasn't spawned a tile yet
            {
                SpawnManager.instance.SpawnGround(transform);
                hasSpawnedNewTile = true; //Makes sure only one ground tile can be spawned
            }
            if (transform.position.x > playerXPos && hasSpawnedNewTile)
            {
                hasSpawnedNewTile = false;
            }
        }
    }
}
