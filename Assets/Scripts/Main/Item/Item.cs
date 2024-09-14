using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isHealing = false;

    void Update()
    {
        TimeUpDestroy();
    }


    void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Healing" && other.gameObject.tag == "Player")
        {
            isHealing = true;
        }
    }

    void TimeUpDestroy()
    {
        Timer TimerScript = GameObject.Find("UIManager").GetComponent<Timer>();
        if (TimerScript.countTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
