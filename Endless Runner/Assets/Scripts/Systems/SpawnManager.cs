using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Ground")]
    public GameObject ground;
    float groundTileLength;

    [Header("Foreground Elements")]
    public GameObject smallCactus;

    [Header("Background Elements")]
    public GameObject cloud;

    [Header("Spawn Variables")]
    public GameObject[] gameplaySpawnableObjects;
    public Vector3[] bgSpawnPositions;
    public int numberOfTilesSpawned;

    private void Start()
    {
        groundTileLength = ground.GetComponent<SpriteRenderer>().size.x; //Get the length of the ground prefab so we can build in even units
        InitialGroundSpawn();
        StartCoroutine("SpawnFGElements");
        StartCoroutine("SpawnBGElements");
    }

    public void SpawnObjectType(string name, Vector3 objPos) //This way, we can type in a string of an object we want to spawn, and it will be instantiated
    {
        GameObject objectClone = Array.Find(gameplaySpawnableObjects, GameObject => GameObject.name == name);
        if (objectClone == null)
        {
            Debug.LogWarning("Game Object: " + name + " not found!");
            return;
        }
        Instantiate(objectClone, objPos, Quaternion.identity, transform);
    }

    void InitialGroundSpawn() //We have CONTINUOUS ground spawning, but there's still a bunch of ground that needs to be spawned right away. That's this function
    {
        GameObject initialGround = FindObjectOfType<GroundScroller>().gameObject; //The initial ground object that the rest of the ground builds from

        for (int i = 1; i < numberOfTilesSpawned; i++) //i starts at 1 because there's already a ground tile beneath the player
        {
            GameObject groundClone = Instantiate(ground, initialGround.transform.position + new Vector3(groundTileLength * i, 0), Quaternion.identity, transform); //Builds another ground tile ahead as many times as we set in numberOfTilesSpawned
        }
    }

    public void SpawnGround(Transform groundTransform) //Whenever a ground tile passes the player X coord, another one is added to the track
    {
        if (GameManager.instance.gameActive) //Only load ground tiles when game is active
        {
            GameObject groundClone = Instantiate(ground, groundTransform.position + new Vector3(groundTileLength * numberOfTilesSpawned, 0), Quaternion.identity, transform);
        }
    }

    IEnumerator SpawnFGElements()
    {
        while (GameManager.instance.gameActive)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(GameManager.instance.gameSpeed / 5, GameManager.instance.gameSpeed / 2)); //Wait for a fraction of the game speed
            GameObject instantiatedObject = gameplaySpawnableObjects[UnityEngine.Random.Range(0, gameplaySpawnableObjects.Length)]; //This just makes the next line a lot shorter
            Instantiate(instantiatedObject, instantiatedObject.transform.position, Quaternion.identity, transform); //Spawn a gameplay object
        }

        yield return null;
    }

    IEnumerator SpawnBGElements()
    {
        while (GameManager.instance.gameActive)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(GameManager.instance.gameSpeed / 5, GameManager.instance.gameSpeed / 2)); //Wait for a fraction of the game speed
            Instantiate(cloud, bgSpawnPositions[UnityEngine.Random.Range(0, bgSpawnPositions.Length)], Quaternion.identity, transform); //Spawn a cloud at one of the BG spawn positions
        }

        yield return null;
    }
}
