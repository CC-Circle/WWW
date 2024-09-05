using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public static bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        MySceneManager.flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MySceneManager.flag = true;
        }
    }
}
