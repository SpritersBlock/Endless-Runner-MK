using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Numbers")]
    public float jumpForce; //For debug purposes, this is just a singular force applied to the rigidbody

    [Header("Collision Info")]
    public bool isGrounded; //Is the player on the ground?
    public float groundRayDist = 1.15f; //How long the raycast determining groundedness is from the center of the player

    [Header("Components")] //Unity-specific components, most of which are set in Start()
    Rigidbody2D rb;
    Animator anim;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() //Input detection
    {
        DetectTouches(); //Detects touch controls
    }

    private void FixedUpdate() //Physics detection
    {
        CheckGrounded(); //Constantly check if player is grounded
    }

    void CheckGrounded(){
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundRayDist, groundLayer);
    }

    void DetectTouches(){
        if (Input.touchCount > 0) //If there's any touch on the screen...
        {
            Touch touch = Input.GetTouch(0); //Only one touch is needed because the only control is "tap."

            if (touch.phase == TouchPhase.Began)
            {
                if (isGrounded){ //Player can only jump when grounded
                    Jump();
                }
            } else if (touch.phase == TouchPhase.Ended)
            {
                if (!isGrounded && rb.velocity.y >= 2){ //If player is in the air
                    StartFall();
                }
            }
        }
    }

    void Jump(){
        rb.velocity = new Vector2(0, 0); //Resets velocity
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //Apply force for jump
    }

    void StartFall(){ //This function provides variable jump height; when player releases tap before the apex of their full jump, they start to fall preemptively
        rb.velocity = new Vector2(0, 0); //Resets velocity
        rb.AddForce(new Vector2(0, 2), ForceMode2D.Impulse); //Adds a little bit of force upwards to soften the hard fall down
    }
}
