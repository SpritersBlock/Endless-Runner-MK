using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    bool hasSpawnedNewTile;

    void Start()
    {
        if (playerTransform == null) //This should be set in the inspector, but this is a failsafe.
        {
            playerTransform = FindObjectOfType<PlayerJump>().transform;
        }
    }

    private void LateUpdate()
    {
        if (GameManager.instance.gameActive)
        {
            if (transform.position.x < playerTransform.position.x && !hasSpawnedNewTile) //If the ground tile passes the player and hasn't spawned a tile yet
            {
                FindObjectOfType<SpawnManager>().SpawnGround(transform);
                hasSpawnedNewTile = true; //Makes sure only one ground tile can be spawned
            }
        }
    }
}
