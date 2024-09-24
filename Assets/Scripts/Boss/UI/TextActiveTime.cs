using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextActiveTime : MonoBehaviour
{
    [SerializeField] private float time = 0;

    public void SetTime(float time)
    {
        this.time = time;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
