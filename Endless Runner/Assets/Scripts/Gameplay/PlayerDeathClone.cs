using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathClone : MonoBehaviour
{
    //When this object is instantiated, it'll fly towards the screen in a fun little death animation.

    Rigidbody2D rb;
    [SerializeField] float angularForce; //How intense the player spins as they fly off


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartDeathSequence();
    }

    void StartDeathSequence()
    {
        rb.angularVelocity = angularForce;
        FindObjectOfType<AudioManager>().Play("Hit");
    }

    public void HitTheScreen()
    {
        //Screenshake here probably. This is the screen hitting part.
        FindObjectOfType<AudioManager>().Play("Punch");

        rb.angularVelocity = 0; //Stop the player from spinning once they hit the screen

        Destroy(gameObject, 5f); //Make sure the object despawns after falling off the screen
    }
}
