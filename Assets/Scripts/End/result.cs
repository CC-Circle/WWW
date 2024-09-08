using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class result : MonoBehaviour
{
    //resultのテキスト表示用変数
    public TextMeshProUGUI resulttext;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager.PlaySound(0);

        //確認用
        HatakeHP.currentRate = 0.15f;
        // Debug.Log(HatakeHP.currentRate);

        // currentRateを調べて、畑と表示テキストを変更
        if (HatakeHP.currentRate >= 0.9f)
        {
            // Debug.Log("通過1");
            // currentRateを調べて、条件にあう畑以外を非表示に
            GameObject.Find("Hatake 2").SetActive(false);
            GameObject.Find("Hatake 3").SetActive(false);
            GameObject.Find("Hatake 4").SetActive(false);
            GameObject.Find("Hatake 5").SetActive(false);
            //テキストの埋め込み
            resulttext.text = "かんぺき!!";
        }
        else if (HatakeHP.currentRate >= 0.75f)
        {
            // Debug.Log("通過2");
            GameObject.Find("Hatake 1").SetActive(false);
            GameObject.Find("Hatake 3").SetActive(false);
            GameObject.Find("Hatake 4").SetActive(false);
            GameObject.Find("Hatake 5").SetActive(false);
            //テキストの埋め込み
            resulttext.text = "いいかんじ!!";
        }
        else if (HatakeHP.currentRate >= 0.5f)
        {
            // Debug.Log("通過3");
            GameObject.Find("Hatake 1").SetActive(false);
            GameObject.Find("Hatake 2").SetActive(false);
            GameObject.Find("Hatake 4").SetActive(false);
            GameObject.Find("Hatake 5").SetActive(false);
            //テキストの埋め込み
            resulttext.text = "がんばった!!";
        }
        else if (HatakeHP.currentRate >= 0.25f)
        {
            // Debug.Log("通過4");
            GameObject.Find("Hatake 1").SetActive(false);
            GameObject.Find("Hatake 2").SetActive(false);
            GameObject.Find("Hatake 3").SetActive(false);
            GameObject.Find("Hatake 5").SetActive(false);
            //テキストの埋め込み
            resulttext.text = "もうすこし!!";
        }
        else if (HatakeHP.currentRate < 0.25f)
        {
            // Debug.Log("通過5");
            GameObject.Find("Hatake 1").SetActive(false);
            GameObject.Find("Hatake 2").SetActive(false);
            GameObject.Find("Hatake 3").SetActive(false);
            GameObject.Find("Hatake 4").SetActive(false);
            //テキストの埋め込み
            resulttext.text = "ぼどぼどや!!";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
