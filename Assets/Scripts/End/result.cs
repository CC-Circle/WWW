using UnityEngine;
using TMPro;

/// <summary>
/// ゲーム結果を表示し、指定された条件に基づいて画像を非表示にするスクリプト。
/// </summary>
public class Result : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    private readonly float[] score = { 0.25f, 0.5f, 0.75f, 0.9f };
    private string[] hatakeNames = { "Hatake1", "Hatake2", "Hatake3", "Hatake4", "Hatake5" };
    private int[][] hideCombinations = {
        new int[] {1, 2, 3, 4},
        new int[] {0, 2, 3, 4},
        new int[] {0, 1, 3, 4},
        new int[] {0, 1, 2, 4},
        new int[] {0, 1, 2, 3}
    };
    private string[] resultMessages = {
        "かんぺき!!", "いいかんじ!!", "がんばった!!", "もうすこし!!", "ぼどぼどや!!"
    };


    private int resultIndex = 0;

    /// <summary>
    /// ゲーム開始時に結果を評価し、適切な処理を実行します。
    /// </summary>
    void Start()
    {
        PlayShotSound(0);
        JudgeResult();
        SetTextAndObject(resultIndex);

    }
    /// <summary>
    /// 指定されたサウンドインデックスのサウンドを再生します。
    /// </summary>
    /// <param name="soundIndex">再生するサウンドのインデックス</param>
    private void PlayShotSound(int soundIndex)
    {
        SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager.PlaySound(soundIndex);
    }

    private void JudgeResult()
    {
        if (HatakeHP.currentHealthRate >= score[3])
        {
            resultIndex = 0;
        }
        else if (HatakeHP.currentHealthRate >= score[2])
        {
            resultIndex = 1;
        }
        else if (HatakeHP.currentHealthRate >= score[1])
        {
            resultIndex = 2;
        }
        else if (HatakeHP.currentHealthRate >= score[0])
        {
            resultIndex = 3;
        }
        else
        {
            resultIndex = 4;
        }
    }

    private void SetTextAndObject(int resultIndex)
    {
        // 対応するオブジェクトを非表示にする
        foreach (int index in hideCombinations[resultIndex])
        {
            GameObject.Find(hatakeNames[index]).SetActive(false);
        }

        // テキストの埋め込み
        resultText.text = resultMessages[resultIndex];
    }
}
