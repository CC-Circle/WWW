using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 文字列を代入し、点滅させる
/// </summary>
public class warningText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Blinking;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI warning;
    [SerializeField] private GameObject flash; // flashオブジェクト用の変数を追加
    private int Flag;

    // Start is called before the first frame update
    void Start()
    {
        // 子オブジェクトのテキストコンポーネントにアクセスして初期化
        warning = transform.Find("warningText").GetComponent<TextMeshProUGUI>();
        // Blinking = transform.Find("BlinkingText").GetComponent<TextMeshProUGUI>(); // もしBlinkingTextという名前の子オブジェクトがある場合
        // image = transform.Find("Image").GetComponent<Image>();
        flash = transform.Find("flash").gameObject; // flashオブジェクトを取得

        // ゲーム起動時に初期化
        warning.text = "";
        flash.SetActive(false); // 起動時にflashオブジェクトを無効化
        GetComponent<warningText>().enabled = false;
        Flag = 0;
    }

    void OnEnable()
    {
        // 有効化時に書き込み
        warning.text = "WARNING";
        flash.SetActive(true); // 点滅が始まると同時にflashを有効化
    }

    // Update is called once per frame
    void Update()
    {
        if (Flag == 0)
        {
            Blinking.alpha += 0.02f;
            if (Blinking.alpha >= 1)
            {
                Flag = 1;
            }
        }
        else
        {
            Blinking.alpha -= 0.02f;
            if (Blinking.alpha < 0)
            {
                Flag = 0;
            }
        }
    }
}