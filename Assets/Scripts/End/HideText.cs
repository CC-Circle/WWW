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
using System;

/*
Timer.csの処理の流れ
1. ShowStartText.csからフラグを受け取りにTimerの計測開始
2. ゲームスタート後に40秒経過すると，FinishTextを表示
3. FinishText表示後に3秒経過すると，MySceneManager.flagをtrueにしてEndシーンに遷移
*/

public class HideText : MonoBehaviour
{
    private float PauseCounter = 0;
    [SerializeField] private float PAUSE = 3;
    TextMeshProUGUI uiText;

    [SerializeField] private string NextScene;

    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        uiText.text = "";
    }

    void Update()
    {
        PauseCounter += Time.deltaTime; // 一時停止時間の計測開始
        if (PauseCounter >= PAUSE) uiText.text = NextScene;

    }
}
