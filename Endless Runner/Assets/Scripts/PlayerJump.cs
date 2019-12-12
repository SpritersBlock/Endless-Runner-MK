using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Numbers")]
    public float jumpForce; //For debug purposes, this is just a singular force applied to the rigidbody.

    [Header("Components")] //Unity-specific components, most of which are set in Start().
    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.touchCount > 0) //If there's any touch on the screen...
        {
            Touch touch = Input.GetTouch(0); //Only one touch is needed because the only control is "tap."

            if (touch.phase == TouchPhase.Start)
            {
                rb.AddForce(jumpForce, ForceMode.Impulse);
            }
        }
    }
}
