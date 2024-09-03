using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StartTimer : MonoBehaviour
{
    [SerializeField] private AudioSource StartSound;
    public static bool flag = false;
    [SerializeField] private TextMeshProUGUI ReadyText;

    void Start()
    {
        // AudioSourceコンポーネントを取得
        StartSound = GetComponent<AudioSource>();

        // シーンが読み込まれたらサウンドを再生
        PlaySound();
    }
    void Update()
    {
        //4秒後にflagをTrueにする
        if (Time.timeSinceLevelLoad >= 2)
        {
            flag = true;
            ReadyText.text = "Start!";
            if (Time.timeSinceLevelLoad > 3)
            {
                ReadyText.text = "";
            }
        }
        else
        {
            // カウントダウンを表示
            ReadyText.text = Mathf.FloorToInt(3 - Time.timeSinceLevelLoad).ToString("F0");
        }
    }

    void PlaySound()
    {
        StartSound.Play();
    }
}
