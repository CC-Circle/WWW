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

public class CalibrationTimer : MonoBehaviour
{
    public float CountTime = 40;
    [SerializeField] private Image uiFill;
    private float PauseCounter = 0;
    [SerializeField] private GameObject FinishText;
    private GameObject[] UiElements;

    void Start()
    {
        FinishText.SetActive(false);
        MySceneManager.flag = false;
        UiElements = GameObject.FindGameObjectsWithTag("UI");
    }

    void Update()
    {
        // 時間を減らす
        CountTime -= Time.deltaTime;
        // FillのFillAmountを時間に応じて変化
        uiFill.fillAmount = Mathf.InverseLerp(0, 10, CountTime);


        // CountTimeのみでも可能だが，可読性向上のために，PauseTimeを使って条件分岐
        // Endシーンに遷移するための条件分岐
        if (CountTime <= 0)
        {

            PauseCounter += Time.deltaTime; // 一時停止時間の計測開始

            FinishText.SetActive(true);

            foreach (GameObject UiElement in UiElements)
            {
                CanvasGroup canvasGroup = UiElement.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    // CanvasGroupがアタッチされていない場合、追加する
                    canvasGroup = UiElement.AddComponent<CanvasGroup>();
                }
                // UIの透明度を0にして、インタラクティブとレイキャストを無効化
                // setActive(false)で実装すると，うまくいかなかったので，CanvasGroupを使って透明度を変更する方法を採用
                canvasGroup.alpha = 0f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }

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
