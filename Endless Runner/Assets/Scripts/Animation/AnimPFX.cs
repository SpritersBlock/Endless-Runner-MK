using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPFX : MonoBehaviour
{
    //This is an all-purpose script I like to use to trigger particle systems via animation events.

    [SerializeField] ParticleSystem PFXToTrigger;

    public void PlayParticleSystem()
    {
        PFXToTrigger.Play();
    }
}
