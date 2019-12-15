using System;
using System.Collections;
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
    public GameObject[] gameplaySpawnableObjects; //The GAMEPLAY objects that can spawn

    private void Start()
    {
        groundLength = ground.GetComponent<SpriteRenderer>().size.x; //Get the length of the ground prefab so we can build in even units
        InitialGroundSpawn();
        StartCoroutine("SpawnBGElements"); //Make sure the background elements are spawning
    }

    public void SpawnObjectType(string name) //This way, we can type in a string of an object we want to spawn, and it will be instantiated
    {
        GameObject objectClone = Array.Find(gameplaySpawnableObjects, GameObject => GameObject.name == name);
        if (objectClone == null) //If there's no object with that name in the array...
        {
            Debug.LogWarning("Game Object: " + name + " not found!"); //...Let people know
            return;
        }
        Instantiate(objectClone, gameSpawnPositions[UnityEngine.Random.Range(0, gameSpawnPositions.Length)], Quaternion.identity); //Spawn that object we found, put it on a random y axis out of the ones we chose, and let the objects themselves do the rest
    }

    void InitialGroundSpawn()
    {
        GameObject initialGround = FindObjectOfType<GroundScroller>().gameObject; //The initial ground object that the rest of the ground builds from

        for (int i = 1; i < numberOfTilesSpawned; i++) //i starts at 1 because there's already a ground tile beneath the player
        {
            GameObject groundClone = Instantiate(ground, initialGround.transform.position + new Vector3(groundLength * i, 0), Quaternion.identity, gameObject.transform); //Builds another ground tile ahead as many times as we set in numberOfTilesSpawned
        }
    }

    public void SpawnGround(Transform groundTransform) //Whenever a ground tile passes the player X coord, another one is added to the track
    {
        if (GameManager.instance.gameActive) //Only load ground tiles when game is active
        {
            GameObject groundClone = Instantiate(ground, groundTransform.position + new Vector3(groundLength * numberOfTilesSpawned, 0), Quaternion.identity, gameObject.transform);
        }
    }

    IEnumerator SpawnBGElements()
    {
        while (GameManager.instance.gameActive)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(GameManager.instance.gameSpeed / 5, GameManager.instance.gameSpeed / 2)); //Wait for a fraction of the game speed
            Instantiate(cloud, bgSpawnPositions[UnityEngine.Random.Range(0, bgSpawnPositions.Length)], Quaternion.identity, gameObject.transform); //Spawn a cloud at one of the BG spawn positions
        }

        yield return null;
    }
}
