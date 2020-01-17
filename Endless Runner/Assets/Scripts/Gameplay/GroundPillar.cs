using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPillar : MonoBehaviour
{
    [Header("Components")] //Unity components
    SpriteRenderer sr;
    BoxCollider2D boxC;

    [Header("Ground Size Values")] //How tall/wide the ground is. Set in start to avoid complications with the spawn manager
    [SerializeField] float platformMinWidth = 1; //These are all default values and probably
    [SerializeField] float platformMaxWidth = 3; //aren't very accurate to gameplay
    [SerializeField] float platformMinHeight = 1;
    [SerializeField] float platformMaxHeight = 3;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.size = new Vector2(Random.Range(platformMinWidth, platformMaxWidth), Random.Range(platformMinHeight, platformMaxHeight));
        boxC = GetComponent<BoxCollider2D>();
        boxC.size = sr.size; //Because we're using the Platform Effector 2D, the box collider can take up the whole space of the platform and only affect the player if they're above it
        SpawnObjectsOnTop();
    }

    void SpawnObjectsOnTop()
    {
        bool spawnObjectsOnTop = (Random.value > 0.5f); //Randomly chooses whether to spawn objects on top of the pillar
        if (spawnObjectsOnTop)
        {
            bool spawnCoins = (Random.value > 0.5f); //Randomly chooses whether to spawn coins on top of the pillar
            if (spawnCoins)
            {
                SpawnManager.instance.SpawnObjectType("CoinNull", transform.position + new Vector3(Random.Range(sr.size.x / -2, sr.size.x / 2), sr.size.y / 2));
            }
            bool spawnCactus = (Random.value > 0.5f); //Randomly chooses whether to spawn cacti on top of the pillar
            if (spawnCactus) //We'll spawn a small cactus up here so it's not too overwhelming
            {
                SpawnManager.instance.SpawnObjectType("CactusSmall", transform.position + new Vector3(Random.Range(sr.size.x / -2, sr.size.x / 2), sr.size.y / 2) + new Vector3(0, SpawnManager.instance.smallCactus.GetComponent<SpriteRenderer>().sprite.bounds.extents.y)); //Spawns a cactus anywhere on the platform top, making sure to bump the cactus up by its own height since its pivot is in the center
            }
        }
    }
}
