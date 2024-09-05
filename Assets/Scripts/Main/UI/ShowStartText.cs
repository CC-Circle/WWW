using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
処理の流れ
1. ゲーム開始時にStartTextを表示
2. 3秒後にStartTextを非表示にして，ゲームスタート
3. ゲームスタート後にTimer.csの処理を開始

flagはTimer.csで使用
*/

public class ShowStartText : MonoBehaviour
{
    public static bool flag = false;
    [SerializeField] private float TIME = 5;
    [SerializeField] private TextMeshProUGUI StartText;

    void Update()
    {
        if (Time.timeSinceLevelLoad >= TIME - 1)
        {
            flag = true;
            StartText.text = "Start!";
            if (Time.timeSinceLevelLoad > TIME) StartText.text = "";
        }
        else
        {
            StartText.text = Mathf.FloorToInt(TIME - Time.timeSinceLevelLoad).ToString("F0");
        }
    }
}