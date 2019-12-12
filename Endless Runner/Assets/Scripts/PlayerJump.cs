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

    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundRayDist, groundLayer);

        if (Input.touchCount > 0) //If there's any touch on the screen...
        {
            Touch touch = Input.GetTouch(0); //Only one touch is needed because the only control is "tap."

            if (touch.phase == TouchPhase.Began) //
            {
                if (isGrounded){ //Player can only jump when grounded
                    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); //Apply force for the jump.
                }
                
            }
        }
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundRayDist);
    }
}
