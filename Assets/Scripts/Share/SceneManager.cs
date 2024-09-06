using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        ForceChangeScene();

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
    private void ForceChangeScene()
    {
        // キーボードでシーン遷移
        // 1: Start
        // 2: Main
        // 3: End
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Start");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Main");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("End");
        }
    }
}
