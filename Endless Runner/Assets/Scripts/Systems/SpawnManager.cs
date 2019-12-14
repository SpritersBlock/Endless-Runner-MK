﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Ground")] //The ground the player runs on
    public GameObject ground;
    float groundLength; //How long the ground tile is

    [Header("Collectables")] //Collectables the player can pick up
    public GameObject coin;

    [Header("Platforms")] //Platforms the player can run on
    public GameObject platform;

    [Header("Obstacles")] //If the player runs into these, the game ends
    public GameObject cactus;

    [Header("Background Elements")] //These scroll in the background and are generally unimportant
    public GameObject cloud;

    [Header("Spawn Variables")]
    public Vector3[] gameSpawnPositions; //The different positions gameplay objects can spawn at
    public Vector3[] bgSpawnPositions;
    public int numberOfTilesSpawned; //How many instances of the ground tile do we want loaded?

    private void Start()
    {
        groundLength = ground.GetComponent<SpriteRenderer>().size.x; //Get the length of the ground prefab so we can build in even units
        InitialGroundSpawn();
    }

    public void SpawnObjectType(string nameOfObject)
    {

    }

    void InitialGroundSpawn()
    {
        GameObject initialGround = FindObjectOfType<GroundScroller>().gameObject; //The initial ground object that the rest of the ground builds from

        for (int i = 1; i < numberOfTilesSpawned; i++) //i starts at 1 because there's already a ground tile beneath the player
        {
            GameObject groundClone = Instantiate(ground, initialGround.transform.position + new Vector3(groundLength * i, 0), Quaternion.identity); //Builds another ground tile ahead as many times as we set in numberOfTilesSpawned
        }
    }

    public void SpawnGround(Transform groundTransform)
    {
        if (GameManager.instance.gameActive) //Only load ground tiles when game is active
        {
            GameObject groundClone = Instantiate(ground, groundTransform.position + new Vector3(groundLength * numberOfTilesSpawned, 0), Quaternion.identity);
        }
    }
}
