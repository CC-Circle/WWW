using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchImage : MonoBehaviour
{
    [SerializeField] private GameObject[] images;
    private float time = 0;

    void Start()
    {
        images[0].SetActive(true);
        images[1].SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 3)
        {
            images[0].SetActive(false);
            images[1].SetActive(true);
        }

    }
}
