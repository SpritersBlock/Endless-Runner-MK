using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Info")]
    public float jumpForce; //For debug purposes, this is just a singular force applied to the rigidbody
    public bool isAirborne; //Is the player jumping, or otherwise in the air?
    public float fallMultiplier = 2.5f; //How much we're multiplying gravity by when player is falling down
    public float lowJumpMultiplier = 2f; //How much we're multiplying gravity by when player releases the jump input early
    bool isHoldingJump; //A bool to check if the player is holding the jump input

    [Header("Collision Info")]
    public bool isGrounded; //Is the player on the ground?
    public float groundRayDist = 1.15f; //How long the raycast determining groundedness is from the center of the player

    [Header("Components")] //Unity-specific components, most of which are set in Start()
    public LayerMask groundLayer;
    public ParticleSystem jumpPFX;
    Rigidbody2D rb;
    Animator anim;

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
        JumpPhysics();
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

    void JumpPhysics() //Default Unity physics aren't great, so let's make our own jump physics!
    {
        if (rb.velocity.y < 0) //If the player is falling...
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; //...Multiply vertical velocity
        }
        else if (rb.velocity.y > 0 && !isHoldingJump) //If the player is ascending but not holding the jump input (basically, has the player released the jump input early?)...
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime; //...Multiply vertical velocity but with a different var unique to letting go of the jump button early
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
                    isHoldingJump = true; //The player is holding the jump input
                    if (isGrounded) //Player can only jump when grounded
                    { 
                        Jump();
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    isHoldingJump = false; //The player is not holding the jump input
                }
            }

            //Below is a way to test the game on a computer. This shouldn't be relevant to the final game but it does mean I don't have to keep plugging my phone in and booting up Unity Remote 5.
            if (Input.GetMouseButtonDown(0))
            {
                isHoldingJump = true; //The player is holding the jump input
                if (isGrounded)
                {
                    Jump();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isHoldingJump = false; //The player is not holding the jump input
            }
        }
    }

    void Jump()
    {
        isAirborne = true;
        Instantiate(jumpPFX, transform.position + new Vector3(0, -groundRayDist), jumpPFX.gameObject.transform.rotation); //Spawns a puff of air for a little feedback

        rb.velocity = Vector2.up * jumpForce;//Apply force for jump

    }
}
