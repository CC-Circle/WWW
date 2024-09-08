using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatake : MonoBehaviour
{
    public static bool isCollide = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CloneEnemy"))
        {
            isCollide = true;

            SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            soundManager.SetVolume(0.5f);
            soundManager.PlaySound(0);

            Destroy(other.gameObject);
        }
    }
}
