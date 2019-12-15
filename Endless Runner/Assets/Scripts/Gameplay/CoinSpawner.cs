using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public float distanceBetweenCoins = 1.5f; //How much space is between coins in a group?
    public int[] coinsPerGroup; //The number of coins that can spawn at once, all should be odd
    public float heightVar = 2; //Coins can spawn higher than normal by a random amount, this is that value
    public float maxNumberOfLanesUp = 2; //How many lanes coins can be spawned up; should be kept somewhat low in case they spawn on platforms

    private void Start()
    {
        InstantiateGroupOfCoins();
        StartCoroutine("CheckForChildren");
    }

    void InstantiateGroupOfCoins()
    {
        transform.position += new Vector3(0, heightVar * Random.Range(0,maxNumberOfLanesUp));


        int numberOfCoinsInGroup = coinsPerGroup[Random.Range(0, coinsPerGroup.Length)]; //Randomly select how many coins show up in a group
        int numberOfCoinsOnSide = Mathf.FloorToInt(numberOfCoinsInGroup / 2); //Calculates how many coins will spawn on either side of the center coin, which is our base. We use FloorToInt because we're dividing odd numbers
        GameObject coinClone = transform.GetChild(0).gameObject; //Getting a coin clone from child


        bool doubleCoins = (Random.value > 0.5f); //Adds a random chance for coin groups to be doubled

        if (doubleCoins)
        {
            Instantiate(coinClone, transform.position + new Vector3(0, heightVar), Quaternion.identity, transform); //Spawns a coin above the center coin
        }
        
        for (int i = 1; i < numberOfCoinsOnSide; i++)
        {
            Instantiate(coinClone, transform.position + new Vector3(distanceBetweenCoins * i, 0), Quaternion.identity, transform); //Spawn half of the coin group on the right side of the center coin. i = 1 because we don't want coins spawning in the center
            if (doubleCoins)
            {
                Instantiate(coinClone, transform.position + new Vector3(distanceBetweenCoins * i, heightVar), Quaternion.identity, transform); //Spawn a double of this coin up above
            }
        }
        for (int i = 1; i < numberOfCoinsOnSide; i++)
        {
            Instantiate(coinClone, transform.position + new Vector3(-distanceBetweenCoins * i, 0), Quaternion.identity, transform); //Spawn half of the coin group on the left side of the center coin. i = 1 because we don't want coins spawning in the center
            if (doubleCoins)
            {
                Instantiate(coinClone, transform.position + new Vector3(-distanceBetweenCoins * i, heightVar), Quaternion.identity, transform); //Spawn a double of this coin up above
            }
        }
    }

    IEnumerator CheckForChildren() //If this object has no more children, destroy it to free up memory
    {
        while (true)
        {
            yield return new WaitForSeconds(2); //Checks every two seconds if it has children or not. Doing it in update seems a bit excessive
            if (transform.childCount == 0) //If there's no child...
            {
                Destroy(gameObject); //...destroy the parent
            }
            yield return null;
        }
    }
}
