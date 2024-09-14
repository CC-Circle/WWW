using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ゲームのカウントダウンタイマーを管理するクラス.
/// </summary>
public class CalibrationTimer : MonoBehaviour
{
    /// <summary>
    /// タイマーの初期値（秒単位）.
    /// </summary>
    [SerializeField] private float CountTime = 5;

    /// <summary>
    /// UIの進行状況を示す画像.
    /// </summary>
    [SerializeField] private Image uiFill;

    /// <summary>
    /// タイマー終了時に表示されるテキストオブジェクト.
    /// </summary>
    [SerializeField] private GameObject FinishText;

    /// <summary>
    /// タイマー終了時に表示されるテキストコンポーネント.
    /// </summary>
    [SerializeField] private TextMeshProUGUI uiText;

    /// <summary>
    /// 一時停止カウンター.
    /// </summary>
    private float PauseCounter = 0;

    /// <summary>
    /// UI要素の配列.
    /// </summary>
    private GameObject[] UiElements;

    /// <summary>
    /// 初期化処理.
    /// </summary>
    void Start()
    {
        FinishText.SetActive(false);
        MySceneManager.flag = false;
        UiElements = GameObject.FindGameObjectsWithTag("UI");
    }

    /// <summary>
    /// フレームごとの更新処理.
    /// </summary>
    void Update()
    {
        // 時間を減らす
        CountTime -= Time.deltaTime;

        // FillのFillAmountを時間に応じて変化
        uiFill.fillAmount = Mathf.InverseLerp(0, 8, CountTime);

        // CountTimeのみでも可能だが、可読性向上のためにPauseCounterを使って条件分岐
        // Endシーンに遷移するための条件分岐
        if (CountTime <= 0)
        {
            PauseCounter += Time.deltaTime; // 一時停止時間の計測開始

            FinishText.SetActive(true);
            uiText.text = "ボタンを押してスタート!!";

            HideUIElements();

            // M5Stackオブジェクトの取得
            GameObject m5Stack = GameObject.Find("M5stack_Event");

            // SerialHandlerとSerialReceiveコンポーネントの取得
            SerialHandler serialHandler = m5Stack.GetComponent<SerialHandler>();
            SerialReceive serialReceive = m5Stack.GetComponent<SerialReceive>();

            // シーン遷移のチェック
            CheckSceneTransition(serialHandler, serialReceive);
        }
    }

    /// <summary>
    /// UI要素を隠す（透明度を0にしてインタラクティブとレイキャストを無効化）.
    /// </summary>
    private void HideUIElements()
    {
        foreach (GameObject UiElement in UiElements)
        {
            CanvasGroup canvasGroup = UiElement.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                // CanvasGroupがアタッチされていない場合、追加する
                canvasGroup = UiElement.AddComponent<CanvasGroup>();
            }
            // UIの透明度を0にして、インタラクティブとレイキャストを無効化
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    /// <summary>
    /// シーン遷移の条件をチェックする.
    /// </summary>
    /// <param name="serialHandler">SerialHandlerコンポーネント.</param>
    /// <param name="serialReceive">SerialReceiveコンポーネント.</param>
    private void CheckSceneTransition(SerialHandler serialHandler, SerialReceive serialReceive)
    {
        // ボタンが押されたときやスペースキーが押されたときにフラグをセット
        if (serialReceive.Flag_button == 1 || Input.GetKeyDown(KeyCode.Space))
        {
            MySceneManager.flag = true;
        }
    }
}
