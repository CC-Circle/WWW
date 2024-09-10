using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public static bool flag = false;
    public SerialReceive serialReceive;
    GameObject M5Stack;
    // Start is called before the first frame update
    void Start()
    {
        MySceneManager.flag = false;
        M5Stack = GameObject.Find("M5stack_Event"); //Playerっていうオブジェクトを探す
    }
    // Update is called once per frame
    void Update()
    {
        // Flagを入手するためのコード
        serialReceive = M5Stack.GetComponent<SerialReceive>(); //付いているスクリプトを取得

        if (serialReceive.Flag_button == 1 || Input.GetKey(KeyCode.Space))
        {
            MySceneManager.flag = true;
        }
    }
}
