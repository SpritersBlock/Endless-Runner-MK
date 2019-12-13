using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : CollectableItem
{
    public ParticleSystem collectPFX;

    public override void BeCollected()
    {
        Instantiate(collectPFX, transform.position, Quaternion.identity);
    }
}
