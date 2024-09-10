using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public static bool flag = false;

    void Start()
    {
        MySceneManager.flag = false;
    }

    void Update()
    {
        // Flagを入手するためのコード
        SerialHandler SerialHandler; //呼ぶスクリプトにあだなつける
        GameObject M5Stack = GameObject.Find("M5stack_Event"); //Playerっていうオブジェクトを探す
        SerialHandler = M5Stack.GetComponent<SerialHandler>(); //付いているスクリプトを取得

        SerialReceive serialReceive;
        SerialReceive SerialReceive; //呼ぶスクリプトにあだなつける
        SerialReceive = M5Stack.GetComponent<SerialReceive>(); //付いているスクリプトを取得

        if (SerialHandler.Settingsflag)
        {
            // Flagを入手するためのコード
            serialReceive = M5Stack.GetComponent<SerialReceive>(); //付いているスクリプトを取得

            if (serialReceive.Flag_button == 1 || Input.GetKey(KeyCode.Space))
            {
                MySceneManager.flag = true;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                MySceneManager.flag = true;
            }
        }
    }
}
