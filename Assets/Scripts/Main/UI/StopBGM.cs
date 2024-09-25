using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBGM : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        Timer timer = GameObject.Find("UIManager").GetComponent<Timer>();
        if (timer.countTime <= 0)
        {
            Destroy(GameObject.Find("BGM"));
        }
    }
}
