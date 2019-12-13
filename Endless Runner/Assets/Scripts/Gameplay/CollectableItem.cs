using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableItem : MonoBehaviour
{
    //Abstract class for items that the player can collect

    public abstract void BeCollected();//Initiate whatever behaviour the specific item has

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BeCollected();
            Destroy(gameObject);
        }
    }
}
