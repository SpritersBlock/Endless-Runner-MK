﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPillar : MonoBehaviour
{
    [Header("Components")] //Unity components
    SpriteRenderer sr;
    BoxCollider2D boxC;
    SpawnManager spawnManager; //Grab this to spawn items on top of the platforms

    [Header("Ground Size Values")] //How tall/wide the ground is. Set in start to avoid complications with the spawn manager
    public float platformMinWidth = 1; //These are all default values and probably
    public float platformMaxWidth = 3; //aren't very accurate to gameplay
    public float platformMinHeight = 1;
    public float platformMaxHeight = 3;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        sr = GetComponent<SpriteRenderer>();
        sr.size = new Vector2(Random.Range(platformMinWidth, platformMaxWidth), Random.Range(platformMinHeight, platformMaxHeight));
        boxC = GetComponent<BoxCollider2D>();
        boxC.size = sr.size; //Because we're using the Platform Effector 2D, the box collider can take up the whole space of the platform and only affect the player if they're above it
        SpawnObjectsOnTop();
    }

    void SpawnObjectsOnTop()
    {
        bool spawnObjectsOnTop = (Random.value > 0.5f); //Randomly chooses whether to spawn objects on top of the pillar
        if (spawnObjectsOnTop)
        {
            bool spawnCoins = (Random.value > 0.5f); //Randomly chooses whether to spawn coins on top of the pillar
            if (spawnCoins)
            {
                spawnManager.SpawnObjectType("CoinNull", transform.position + new Vector3(Random.Range(sr.size.x / -2, sr.size.x / 2), sr.size.y / 2));
            }
            bool spawnCactus = (Random.value > 0.5f); //Randomly chooses whether to spawn cacti on top of the pillar
            spawnCactus = true;
            if (spawnCactus) //We'll spawn a small cactus up here so it's not too overwhelming
            {
                spawnManager.SpawnObjectType("CactusSmall", transform.position + new Vector3(Random.Range(sr.size.x / -2, sr.size.x / 2), sr.size.y / 2) + new Vector3(0, spawnManager.smallCactus.GetComponent<SpriteRenderer>().sprite.bounds.extents.y)); //Spawns a cactus anywhere on the platform top, making sure to bump the cactus up by its own height since its pivot is in the center
            }
        }
    }
}
