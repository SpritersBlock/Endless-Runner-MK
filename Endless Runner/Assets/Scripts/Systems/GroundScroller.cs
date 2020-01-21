using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroller : MonoBehaviour, InterfacePooledObject
{
    //Ground tiles get their own scrolling script due to the fact that they are the one element that spawns consistently and with no randomness factored in.

    float playerXPos;
    bool hasSpawnedNewTile;

    void Start()
    {
        playerXPos = SpawnManager.instance.playerXPos;
    }

    public void OnObjectSpawn()
    {
        DestroyExistingChildren();
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

    void DestroyExistingChildren()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
