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
        // Flagを入手するためのコード
        SerialReceive serialReceive; //呼ぶスクリプトにあだなつける
        GameObject M5Stack = GameObject.Find("M5stack_Event"); //Playerっていうオブジェクトを探す
        serialReceive = M5Stack.GetComponent<SerialReceive>(); //付いているスクリプトを取得

        if (serialReceive.Flag_button == 1)
        {
            MySceneManager.flag = true;
        }
    }
}
