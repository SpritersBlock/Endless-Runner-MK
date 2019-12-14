using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Ground")] //The ground the player runs on
    public GameObject ground;

    [Header("Collectables")] //Collectables the player can pick up
    public GameObject coin;

    [Header("Platforms")] //Platforms the player can run on
    public GameObject platform;

    [Header("Obstacles")] //If the player runs into these, the game ends
    public GameObject cactus;
}
