using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] ParticleSystem dustPFX;
    [SerializeField] PlayerJump playerJump;

    public void Footstep()
    {
        if (playerJump.isGrounded)
        {
            FindObjectOfType<AudioManager>().Play("Step");
            dustPFX.Play();
        }
    }
}
