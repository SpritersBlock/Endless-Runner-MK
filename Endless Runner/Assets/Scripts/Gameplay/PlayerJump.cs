using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Info")]
    public float jumpForce; //For debug purposes, this is just a singular force applied to the rigidbody
    public bool isAirborne; //Is the player jumping, or otherwise in the air?

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

    void CheckGrounded()
    {
        if (rb.velocity.y == 0) //Is the player's vertical velocity still?
        {
            if (!isAirborne) //The player's vertical velocity is 0 at the apex of their jump so let's take that into account
            {
                isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundRayDist, groundLayer); //Because all of the ground pillar is part of the ground layer, being in front of the pillar (even midair) will count as being grounded, so we need to use isAirborne to check
            }
            else
            {
                isAirborne = false; //This turns the bool off after the apex of the player's jump, so isGrounded will turn back on upon landing
            }
        }
        else
        {
            isAirborne = true; //If the player's vertical isn't 0, they must be airborne...
            isGrounded = false; //...and therefore not grounded
        }
    }

    void DetectTouches()
    {
        if (GameManager.instance.gameActive) //Input is only stored if the game is active
        {
            if (Input.touchCount > 0) //If there's any touch on the screen...
            {
                Touch touch = Input.GetTouch(0); //Only one touch is needed because the only control is "tap."

                if (touch.phase == TouchPhase.Began)
                {
                    if (isGrounded)
                    { //Player can only jump when grounded
                        Jump();
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (!isGrounded && rb.velocity.y >= 2)
                    { //If player is in the air
                        StartFall();
                    }
                }
            }

            //Below is a way to test the game on a computer. This shouldn't be relevant to the final game but it does mean I don't have to keep plugging my phone in and booting up Unity Remote 5.
            if (Input.GetMouseButtonDown(0))
            {
                if (isGrounded)
                {
                    Jump();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (!isGrounded && rb.velocity.y >= 2)
                {
                    StartFall();
                }
            }
        }
    }

    void Jump()
    {
        isAirborne = true;
        rb.velocity = new Vector2(0, 0); //Resets velocity
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //Apply force for jump
    }

    void StartFall()
    { //This function provides variable jump height; when player releases tap before the apex of their full jump, they start to fall preemptively
        rb.velocity = new Vector2(0, 0); //Resets velocity
        rb.AddForce(new Vector2(0, 2), ForceMode2D.Impulse); //Adds a little bit of force upwards to soften the hard fall down
    }
}
