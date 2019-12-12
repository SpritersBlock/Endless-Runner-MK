using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Numbers")]
    public float jumpForce; //For debug purposes, this is just a singular force applied to the rigidbody

    [Header("Collision Info")]
    public bool isGrounded; //Is the player on the ground?

    [Header("Components")] //Unity-specific components, most of which are set in Start()
    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.touchCount > 0) //If there's any touch on the screen...
        {
            Touch touch = Input.GetTouch(0); //Only one touch is needed because the only control is "tap."

            if (touch.phase == TouchPhase.Began) //
            {
                //[If Player is grounded]
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); //Apply force for the jump
            }
        }
    }
}
