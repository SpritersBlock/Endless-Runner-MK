﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPillar : MonoBehaviour, InterfacePooledObject
{
    [Header("Components")] //Unity components
    SpriteRenderer sr;
    BoxCollider2D boxC;
    ObjectPooler objectPooler;

    [Header("Ground Size Values")] //How tall/wide the ground is. Set in start to avoid complications with the spawn manager
    [SerializeField] float platformMinWidth = 1; //These are all default values and probably
    [SerializeField] float platformMaxWidth = 3; //aren't very accurate to gameplay
    [SerializeField] float platformMinHeight = 1;
    [SerializeField] float platformMaxHeight = 3;

    [Header("Gameplay Variables")]
    bool spawnObjectsOnTop;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.instance;
        OnObjectSpawn();
    }

    public void OnObjectSpawn()
    {
        sr = GetComponent<SpriteRenderer>();
        boxC = GetComponent<BoxCollider2D>();
        DestroyExistingChildren();
        sr.size = new Vector2(Random.Range(platformMinWidth, platformMaxWidth), Random.Range(platformMinHeight, platformMaxHeight));
        boxC.size = sr.size;
        SpawnObjectsOnTop();
    }

    void SpawnObjectsOnTop()
    {
        if (objectPooler == null)
        {
            objectPooler = ObjectPooler.instance;
        }

        spawnObjectsOnTop = (Random.value > 0.5f); //Randomly chooses whether to spawn objects on top of the pillar
        if (spawnObjectsOnTop)
        {
            bool spawnCoins = (Random.value > 0.5f); //Randomly chooses whether to spawn coins on top of the pillar
            if (spawnCoins)
            {
                objectPooler.SpawnFromPool("CoinNull", transform.position + new Vector3(Random.Range(sr.size.x / -2, sr.size.x / 2), sr.size.y / 2), Quaternion.identity, transform.parent);
            }
            bool spawnCactus = (Random.value > 0.5f); //Randomly chooses whether to spawn cacti on top of the pillar
            if (spawnCactus) //We'll spawn a small cactus up here so it's not too overwhelming
            {
                objectPooler.SpawnFromPool("CactusSmall", transform.position + new Vector3(Random.Range(sr.size.x / -2, sr.size.x / 2), sr.size.y / 2) + new Vector3(0, SpawnManager.instance.smallCactus.GetComponent<SpriteRenderer>().sprite.bounds.extents.y), Quaternion.identity, transform.parent); //Spawns a cactus anywhere on the platform top, making sure to bump the cactus up by its own height since its pivot is in the center
            }
        }
    }

    void DestroyExistingChildren()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}
