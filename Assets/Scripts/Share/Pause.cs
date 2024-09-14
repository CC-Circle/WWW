using UnityEngine;

/// <summary>
/// 指定した時間が経過した後、シーン遷移の条件をチェックするスクリプト。
/// </summary>
public class Pause : MonoBehaviour
{
    [SerializeField] private float countdownDuration = 3f; // カウントダウンの時間
    private float remainingTime; // 残り時間

    /// <summary>
    /// 初期化処理。フラグをリセットし、残り時間を設定します。
    /// </summary>
    void Start()
    {
        MySceneManager.flag = false;
        remainingTime = countdownDuration;
    }

    /// <summary>
    /// フレームごとの更新処理。カウントダウンを行い、時間が経過したらシーン遷移の条件をチェックします。
    /// </summary>
    void Update()
    {
        // 時間を減らす
        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0)
        {
            // M5Stackオブジェクトの取得
            GameObject m5Stack = GameObject.Find("M5stack_Event");

            // SerialHandlerとSerialReceiveコンポーネントの取得
            SerialHandler serialHandler = m5Stack.GetComponent<SerialHandler>();
            SerialReceive serialReceive = m5Stack.GetComponent<SerialReceive>();

            // シーン遷移の条件をチェック
            CheckSceneTransition(serialHandler, serialReceive);
        }
    }

    /// <summary>
    /// シーン遷移の条件をチェックし、フラグを設定します。
    /// </summary>
    /// <param name="serialHandler">SerialHandlerコンポーネント</param>
    /// <param name="serialReceive">SerialReceiveコンポーネント</param>
    private void CheckSceneTransition(SerialHandler serialHandler, SerialReceive serialReceive)
    {
        // ボタンが押されたときやスペースキーが押されたときにフラグをセット
        if (serialReceive.Flag_button == 1 || Input.GetKeyDown(KeyCode.Space))
        {
            MySceneManager.flag = true;
        }
    }
}
