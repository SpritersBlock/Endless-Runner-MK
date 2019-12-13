using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableItem : MonoBehaviour
{
    public abstract void BeCollected();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BeCollected();
            Destroy(gameObject);
        }
    }
}
