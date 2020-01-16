using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : CollectableItem
{
    [Header("Game Info")]
    public int coinValue = 1; //How many coins this coin is worth. Can be worth more or less
    //Ideally different values of coins would have different appearances but this is early debug mode

    [Header("Components")] //Unity component info
    public ParticleSystem collectPFX;

    public override void BeCollected()
    {
        Instantiate(collectPFX, transform.position, Quaternion.identity, transform); //Spawns collect particle system
        FindObjectOfType<AudioManager>().Play("Coin");
        GameManager.instance.UpdateCoinCountersInCoinManager(coinValue); //Adds this coin's value to both coin counters
    }
}
