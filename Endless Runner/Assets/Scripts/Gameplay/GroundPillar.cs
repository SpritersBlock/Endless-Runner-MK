using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPillar : MonoBehaviour
{
    [Header("Components")] //Unity components
    SpriteRenderer sr;
    BoxCollider2D boxC;

    [Header("Ground Size Values")] //How tall/wide the ground is. Set in start to avoid complications with the spawn manager
    public float platformMinWidth = 1; //These are all default values and probably
    public float platformMaxWidth = 3; //aren't very accurate to gameplay
    public float platformMinHeight = 1;
    public float platformMaxHeight = 3;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.size = new Vector2(Random.Range(platformMinWidth, platformMaxWidth), Random.Range(platformMinHeight, platformMaxHeight));
        boxC = GetComponent<BoxCollider2D>();
        boxC.size = sr.size; //Because we're using the Platform Effector 2D, the box collider can take up the whole space of the platform and only affect the player if they're above it
    }

    void SpawnObjectsOnTop()
    {
        bool spawnObjectsOnTop = (Random.value > 0.5f); //Randomly chooses whether to spawn objects on top of the pillar
        if (!spawnObjectsOnTop)
        {
            return;
        }
        if (spawnObjectsOnTop)
        {
            //We might want to put items on top, so potentially spawn items here
        }
    }
}
