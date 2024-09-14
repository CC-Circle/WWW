using TMPro;
using UnityEngine;

/// <summary>
/// ゲームの開始時にカウントダウンとテキスト表示を管理するスクリプト。
/// 指定した時間が経過するまでカウントダウンを表示し、その後に指定されたテキストを表示します。
/// </summary>
public class CountdownDisplay : MonoBehaviour
{
    [SerializeField] private float countdownDuration = 4f; // カウントダウンの持続時間
    [SerializeField] private string startText = "Start!"; // カウントダウン後に表示するテキスト
    [SerializeField] private TextMeshProUGUI countdownText; // カウントダウンとテキストを表示するUIコンポーネント

    public static bool flag = false; // カウントダウンが完了したかどうかを示すフラグ

    private void Start()
    {
        PlayStartSound();
    }

    private void Update()
    {
        UpdateCountdown();
    }

    /// <summary>
    /// 指定されたサウンドを再生します。
    /// </summary>
    /// <param name="soundIndex">再生するサウンドのインデックス</param>
    private void PlayStartSound()
    {
        SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager.PlaySound(4);
    }

    /// <summary>
    /// カウントダウンを更新し、指定された時間が経過するとテキストを表示します。
    /// </summary>
    private void UpdateCountdown()
    {
        float elapsedTime = Time.timeSinceLevelLoad;

        if (elapsedTime >= countdownDuration - 1f)
        {
            flag = true;
            countdownText.text = startText;
            if (elapsedTime > countdownDuration)
            {
                countdownText.text = "";
            }
        }
        else
        {
            countdownText.text = Mathf.FloorToInt(countdownDuration - elapsedTime).ToString("F0");
        }
    }
}
