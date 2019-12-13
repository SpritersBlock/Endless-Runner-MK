using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] ParticleSystem dustPFX;
    public PlayerJump playerJump;

    public void Footstep()
    {
        if (playerJump.isGrounded)
        {
            //Play Sound Here
            dustPFX.Play();
        }
    }
}
