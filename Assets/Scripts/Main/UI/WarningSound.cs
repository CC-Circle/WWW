using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSound : MonoBehaviour
{
    private AudioSource warningSound;
    private void Start()
    {
        warningSound = GetComponent<AudioSource>();
        warningSound.Stop();
    }

    public void ShotWarningSound()
    {
        warningSound.Play();
    }
}
