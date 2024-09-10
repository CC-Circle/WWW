using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering;
using UnityEditor.UIElements;
using System.Media;
using Unity.VisualScripting;

/*
Timer.csの処理の流れ
1. ShowStartText.csからフラグを受け取りにTimerの計測開始
2. ゲームスタート後に40秒経過すると，FinishTextを表示
3. FinishText表示後に3秒経過すると，MySceneManager.flagをtrueにしてEndシーンに遷移
*/

public class Pause : MonoBehaviour
{
    [SerializeField] private float CountTime = 3;

    void Start()
    {
        MySceneManager.flag = false;

    }

    void Update()
    {
        // 時間を減らす
        CountTime -= Time.deltaTime;

        if (CountTime <= 0)
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


}
