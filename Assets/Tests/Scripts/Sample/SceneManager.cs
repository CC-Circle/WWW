// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement; // Unityのシーン管理名前空間をインポート

// public class MySceneManager : MonoBehaviour
// {
//     //シーンチェンジスクリプト
//     private string NowScene; // クラスレベルで変数を宣言
//     public bool flag;
//     KusakariControl KusakariControl;

//     // Startは最初のフレームで1回だけ呼び出される
//     void Start()
//     {
//         KusakariControl = FindObjectOfType<KusakariControl>();
//     }

//     // Updateは毎フレーム呼び出される
//     void Update()
//     {
//         NowScene = SceneManager.GetActiveScene().name; // 現在のシーン名を変数に代入

//         if (NowScene == "start")
//         {
//             if (KusakariControl.ChangeMain == 1)
//             {
//                 flag = false;
//                 SceneManager.LoadScene("main"); // シーンを"main"に変更
//             }
//         }
//         else if (NowScene == "main")
//         {
//             if (flag)
//             {
//                 flag = false;
//                 SceneManager.LoadScene("end"); // シーンを"end"に変更
//             }
//         }
//         // else if(NowScene == "end")
//         // {
//         //     if(flag)
//         //     {
//         //         flag=false;
//         //         SceneManager.LoadScene("start"); // シーンを"start"に変更
//         //     }
//         // }
//     }
// }
