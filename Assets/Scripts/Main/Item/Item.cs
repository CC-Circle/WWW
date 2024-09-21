using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isHealing = false;

    void Update()
    {
    }


    void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Healing" && other.gameObject.tag == "Player")
        {
            isHealing = true;
        }
    }
}
