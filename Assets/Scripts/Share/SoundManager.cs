using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] audioClips;

    public void PlaySound(int index)
    {
        if (audioClips.Length <= index)
        {
            Debug.LogError("指定されたインデックスが範囲外です。");
            return;
        }

        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioClips[index]);
    }
    public void SetVolume(float volume)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }
}
