using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
flagはTimer.csでtrueになる
*/

public class MySceneManager : MonoBehaviour
{
    private string NowScene;
    public static bool flag;

    void Update()
    {
        // 現在のシーン名を取得
        NowScene = SceneManager.GetActiveScene().name;

        if (NowScene == "Start" && flag)
        {
            Debug.Log("通過");
            flag = false;
            SceneManager.LoadScene("Main");

        }
        else if (NowScene == "Main" && flag)
        {
            flag = false;
            SceneManager.LoadScene("End");
        }
        else if (NowScene == "End" && flag)
        {
            flag = false;
            SceneManager.LoadScene("Start");
        }
    }
}
