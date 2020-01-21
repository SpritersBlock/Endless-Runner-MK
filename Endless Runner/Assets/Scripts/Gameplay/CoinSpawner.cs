using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour, InterfacePooledObject
{
    [Header("Components")]
    [SerializeField] GameObject coinPrefab;
    ObjectPooler objectPooler;

    [Header("Coin Group Information")]
    [SerializeField] float horizontalDistanceBetweenCoins = 1.5f; //How much horizontal space is between coins in a group?
    [SerializeField] int[] coinsPerRow; //The number of coins that can spawn at once, all should be odd
    [SerializeField] float heightUnit = 2; //Coins can spawn higher than normal by a random amount, this is that value
    [SerializeField] float maxNumberOfLanesUp = 2; //How many lanes coins can be spawned up; should be kept somewhat low in case they spawn on platforms

    private void Start()
    {
        objectPooler = ObjectPooler.instance;
    }

    public void OnObjectSpawn()
    {
        SpawnGroupOfCoins();
        //StartCoroutine("CheckForChildren");
    }

    void SpawnGroupOfCoins()
    {
        DestroyExistingChildren();

        if (objectPooler == null)
        {
            objectPooler = ObjectPooler.instance;
        }

        Vector3 coinPosition = transform.position + new Vector3(0, heightUnit * Mathf.FloorToInt(Random.Range(0, maxNumberOfLanesUp)));


        objectPooler.SpawnFromPool("Coin", coinPosition, Quaternion.identity, transform); //Spawns the centermost coin

        int numberOfCoinsInGroup = coinsPerRow[Random.Range(0, coinsPerRow.Length)]; //Randomly select how many coins show up in a group
        int numberOfCoinsOnSide = Mathf.FloorToInt(numberOfCoinsInGroup / 2);

        bool doubleCoins = (Random.value > 0.7f); //Adds a random chance for coin groups to be doubled

        if (doubleCoins)
        {
            objectPooler.SpawnFromPool("Coin", coinPosition + new Vector3(0, heightUnit), Quaternion.identity, transform); //Spawns a coin above the center coin
        }

        for (int o = -1; o < 2; o += 2) //Spawns coins on both left and right sides of center coin(s)
        {
            for (int i = 1; i < numberOfCoinsOnSide + 1; i++)
            {
                objectPooler.SpawnFromPool("Coin", coinPosition + new Vector3(horizontalDistanceBetweenCoins * i * o, 0), Quaternion.identity, transform);
                if (doubleCoins)
                {
                    objectPooler.SpawnFromPool("Coin", coinPosition + new Vector3(horizontalDistanceBetweenCoins * i * o, heightUnit), Quaternion.identity, transform);
                }
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
