using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// タイマーを管理し、カウントダウン、UIの更新、およびシーン遷移の処理を行うスクリプト。
/// </summary>
public class Timer : MonoBehaviour
{
    [SerializeField] private Image timerFillImage; // タイマーの進捗を表示するUIイメージ
    [SerializeField] private TextMeshProUGUI timerText; // タイマーの残り時間を表示するテキスト
    [SerializeField] private float pauseDuration = 3f; // シーン遷移前に停止する時間
    [SerializeField] private GameObject finishTextObject; // タイマー終了後に表示するテキストオブジェクト

    private GameObject[] uiElements; // シーン内のUI要素
    private float pauseCounter = 0f; // 一時停止の経過時間

    private bool hasPlayedFinishSound = true; // フィニッシュサウンドが再生済みかどうか
    public float countTime = 40f; // カウントダウンの初期時間

    private void Start()
    {
        uiElements = GameObject.FindGameObjectsWithTag("UI");
        finishTextObject.SetActive(false);
        MySceneManager.flag = false;
    }

    private void Update()
    {
        if (CountdownDisplay.flag)
        {
            UpdateTimer();
        }

        HandleSceneTransition();

        if (countTime <= 0)
        {
            pauseCounter += Time.deltaTime; // 一時停止時間の計測開始

            HideUIElements();

            finishTextObject.SetActive(true);

            PlayFinishSound();
        }
    }

    /// <summary>
    /// UI要素の透明度を変更し、インタラクティブ性とレイキャストを無効にします。
    /// </summary>
    private void HideUIElements()
    {
        foreach (GameObject uiElement in uiElements)
        {
            CanvasGroup canvasGroup = uiElement.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                // CanvasGroupがアタッチされていない場合、追加する
                canvasGroup = uiElement.AddComponent<CanvasGroup>();
            }
            // UIの透明度を0にして、インタラクティブとレイキャストを無効化
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    /// <summary>
    /// 一時停止の経過時間をチェックし、必要に応じてシーン遷移のフラグを更新します。
    /// </summary>
    private void HandleSceneTransition()
    {
        if (pauseCounter >= pauseDuration)
        {
            MySceneManager.flag = true;
        }
    }

    /// <summary>
    /// タイマーを更新し、残り時間を表示します。
    /// </summary>
    private void UpdateTimer()
    {
        countTime -= Time.deltaTime;
        timerText.text = Mathf.FloorToInt(countTime).ToString("F0");
        timerFillImage.fillAmount = Mathf.InverseLerp(0, 40, countTime);
    }

    /// <summary>
    /// タイマー終了時にフィニッシュサウンドを再生します。
    /// </summary>
    private void PlayFinishSound()
    {
        // フィニッシュサウンドが再生済みでない場合のみ実行
        if (hasPlayedFinishSound)
        {
            PlaySound(5);
        }
    }

    /// <summary>
    /// 指定されたサウンドを再生します。
    /// </summary>
    /// <param name="soundIndex">再生するサウンドのインデックス</param>
    private void PlaySound(int soundIndex)
    {
        SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager.PlaySound(soundIndex);
        hasPlayedFinishSound = false;
    }
}
