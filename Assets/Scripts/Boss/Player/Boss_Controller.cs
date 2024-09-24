using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの動きと操作を制御するスクリプト。センサー入力またはマウス操作によってプレイヤーを制御します。
/// </summary>
public class Boss_Controller : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 0.001f; // プレイヤーの移動速度
    [SerializeField] private GameObject rotationCenter; // プレイヤーが回転する中心オブジェクト

    private Vector3 lastMousePosition; // 前回のマウス位置
    private float soundEffectInterval = 3f; // サウンドエフェクト再生間隔
    private bool isMove = false;

    private void Start()
    {
        // 初期化時にマウスの位置を保存
        lastMousePosition = Input.mousePosition;
    }

    private void Update()
    {
        // センサーまたはマウスによる操作を処理
        SerialHandler serialHandler;
        GameObject m5Stack = GameObject.Find("M5stack_Event"); // プレイヤーのオブジェクトを探す
        serialHandler = m5Stack.GetComponent<SerialHandler>(); // SerialHandlerスクリプトを取得

        SerialReceive serialReceive;
        serialReceive = m5Stack.GetComponent<SerialReceive>(); // SerialReceiveスクリプトを取得

        if (serialHandler.Settingsflag)
        {
            HandleSensorInput(serialHandler, serialReceive);
        }
        else
        {
            HandleMouseInput();
        }
    }

    /// <summary>
    /// センサー入力に基づいてプレイヤーの操作を処理します。
    /// </summary>
    /// <param name="serialHandler">SerialHandlerスクリプト</param>
    /// <param name="serialReceive">SerialReceiveスクリプト</param>
    private void HandleSensorInput(SerialHandler serialHandler, SerialReceive serialReceive)
    {

    }

    /// <summary>
    /// マウス入力に基づいてプレイヤーの操作を処理します。
    /// </summary>
    private void HandleMouseInput()
    {
        float rotationSpeed = 10f; // 回転速度

        // マウスのX方向の移動距離を計算
        Vector3 currentMousePosition = Input.mousePosition;
        float mouseDeltaX = currentMousePosition.x - lastMousePosition.x;

        rotationCenter.transform.Rotate(0, mouseDeltaX * rotationSpeed * 0.005f, 0);

        BossHP BossHPScript = GameObject.Find("Boss").GetComponent<BossHP>();


        //  マウスが画面の右側にある場合
        if (currentMousePosition.x > Screen.width / 2 && isMove == false)
        {
            BossHPScript.TakeDamage(1);
            isMove = true;
        }

        // マウスが画面の左側にある場合
        if (currentMousePosition.x < Screen.width / 2 && isMove == true)
        {
            BossHPScript.TakeDamage(1);
            isMove = false;
        }

        // 現在のマウス位置を次のフレーム用に保存
        lastMousePosition = currentMousePosition;
    }

    // private void PlaySoundEffect(int soundIndex)
    // {
    //     if (Time.time > soundEffectInterval)
    //     {
    //         SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    //         soundManager.PlaySound(soundIndex);
    //         soundEffectInterval = Time.time + 2f; // 次の再生までの時間を設定
    //     }
    // }
}
