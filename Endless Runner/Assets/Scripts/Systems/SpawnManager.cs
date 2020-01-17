using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Components")]
    public static SpawnManager instance;
    ObjectPooler objectPooler;
    [SerializeField] GameObject playerObj;

    [Header("Ground")]
    [SerializeField] GameObject ground;
    [SerializeField] float groundTileLength;

    [Header("Foreground Elements")]
    public GameObject smallCactus;

    [Header("Background Elements")]
    [SerializeField] GameObject cloud;
    [SerializeField] float timerCloud;
    [SerializeField] float timerCloudMin = 0.5f;
    [SerializeField] float timerCloudMax = 5;

    [Header("Spawn Variables")]
    [SerializeField] GameObject[] gameplaySpawnableObjects;
    [SerializeField] Vector3[] bgSpawnPositions;
    [SerializeField] int numberOfTilesSpawned;
    public float playerXPos;

    private void Awake()
    {
        // This isn't a TRUE singleton because we need the stage to be destroyed on load
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        groundTileLength = ground.GetComponent<SpriteRenderer>().size.x; //Get the length of the ground prefab so we can build in even units
        playerXPos = playerObj.transform.position.x;
        objectPooler = ObjectPooler.instance;
        InitialGroundSpawn();
        StartCoroutine("SpawnFGElements");
        //StartCoroutine("SpawnBGElements");
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.gameActive)
        {
            SpawnBGElementTimer();
        }
    }

    void SpawnBGElementTimer()
    {
        if (timerCloud > 0)
        {
            timerCloud -= Time.deltaTime;
        }
        else
        {
            objectPooler.SpawnFromPool("Cloud", bgSpawnPositions[UnityEngine.Random.Range(0, bgSpawnPositions.Length)], Quaternion.identity);
            timerCloud = UnityEngine.Random.Range(timerCloudMin, timerCloudMax);
        }
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

    void InitialGroundSpawn()
    {
        for (int i = 0; i < numberOfTilesSpawned; i++)
        {
            objectPooler.SpawnFromPool("Ground", ground.transform.position + new Vector3(groundTileLength * i, 0), Quaternion.identity); //Builds another ground tile ahead as many times as we set in numberOfTilesSpawned
        }
    }

    public void SpawnGround(Transform groundTransform) //Whenever a ground tile passes the player X coord, another one is added to the track. From GroundScroller.cs
    {
        if (GameManager.instance.gameActive) //Only load ground tiles when game is active
        {
            objectPooler.SpawnFromPool("Ground", groundTransform.position + new Vector3(groundTileLength * numberOfTilesSpawned, 0), Quaternion.identity);
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
}
