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
    public TextMeshProUGUI Blinking;
    public Image image;
    private int Flag;
    [SerializeField] private TextMeshProUGUI warning;
    
    // Start is called before the first frame update
    void Start()
    {
        //ゲーム起動時に空文字で初期化
        warning.text = "";
        GetComponent<warningText>().enabled = false;
        Flag = 0;
    }

    void OnEnable()
    {
        //有効化時に書き込み
        warning.text = "WARNING";
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Flag == 0)
        {
            Blinking.alpha += 0.02f;
            if(Blinking.alpha >= 1)
            {
                Flag = 1;
            }
        }else
        {
            Blinking.alpha -= 0.02f;
            if(Blinking.alpha < 0)
            {
                Flag = 0;
            }
        }
    }
}
