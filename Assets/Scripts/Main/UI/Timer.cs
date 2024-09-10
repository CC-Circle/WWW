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

public class Timer : MonoBehaviour
{
    public float CountTime = 40;
    [SerializeField] private Image uiFill;
    [SerializeField] private TextMeshProUGUI uiText;
    private GameObject[] UiElements;
    private float PauseCounter = 0;
    [SerializeField] private float PAUSE = 3;
    [SerializeField] private GameObject FinishText;

    private bool isShotSE = true;

    void Start()
    {
        UiElements = GameObject.FindGameObjectsWithTag("UI");
        FinishText.SetActive(false);
    }

    void Update()
    {
        if (ShowStartText.flag)
        {
            // 時間を減らす
            CountTime -= Time.deltaTime;
            uiText.text = Mathf.FloorToInt(CountTime).ToString("F0");
            // FillのFillAmountを時間に応じて変化
            uiFill.fillAmount = Mathf.InverseLerp(0, 40, CountTime);
        }

        // CountTimeのみでも可能だが，可読性向上のために，PauseTimeを使って条件分岐
        // Endシーンに遷移するための条件分岐
        if (PauseCounter >= PAUSE) MySceneManager.flag = true;

        // 処理2
        //  40秒経過後にFinishTextを表示と一定時間（3秒）停止
        if (CountTime <= 0)
        {
            PauseCounter += Time.deltaTime; // 一時停止時間の計測開始

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
            FinishText.SetActive(true);

            // isShotSEがfalseの場合にOneShotSE()を実行
            isShotSE = isShotSE ? true : false;
            if (isShotSE) OneShotSE();
        }
    }

    private void OneShotSE()
    {
        SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager.PlaySound(5);
        isShotSE = false;
    }
}
