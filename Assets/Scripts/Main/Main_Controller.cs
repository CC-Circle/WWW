using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの動きと操作を制御するスクリプト。センサー入力またはマウス操作によってプレイヤーを制御します。
/// </summary>
public class Main_Controller : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 0.001f; // プレイヤーの移動速度
    [SerializeField] private GameObject rotationCenter; // プレイヤーが回転する中心オブジェクト
    [SerializeField] private ParticleSystem movementParticles; // 移動時に再生されるパーティクルシステム

    private Vector3 lastMousePosition; // 前回のマウス位置
    private float soundEffectInterval = 3f; // サウンドエフェクト再生間隔
    private SphereCollider playerCollider; // プレイヤーのコライダー

    private void Start()
    {
        // 初期化時にマウスの位置を保存
        lastMousePosition = Input.mousePosition;
        playerCollider = GetComponent<SphereCollider>();
        playerCollider.enabled = false;
        movementParticles.Stop();
    }

    private void Update()
    {
        // センサーまたはマウスによる操作を処理
        SerialHandler serialHandler;
        GameObject m5Stack = GameObject.Find("M5stack_Event"); // プレイヤーのオブジェクトを探す
        serialHandler = m5Stack.GetComponent<SerialHandler>(); // SerialHandlerスクリプトを取得

        SerialReceive serialReceive;
        serialReceive = m5Stack.GetComponent<SerialReceive>(); // SerialReceiveスクリプトを取得

        if (CountdownDisplay.flag)
        {
            if (serialHandler.Settingsflag)
            {
                HandleSensorInput(serialHandler, serialReceive);
            }
            else
            {
                HandleMouseInput();
            }
        }
    }

    /// <summary>
    /// センサー入力に基づいてプレイヤーの操作を処理します。
    /// </summary>
    /// <param name="serialHandler">SerialHandlerスクリプト</param>
    /// <param name="serialReceive">SerialReceiveスクリプト</param>
    private void HandleSensorInput(SerialHandler serialHandler, SerialReceive serialReceive)
    {
        if (serialReceive.Flag_view == 1)
        {
            // プレイヤーを左に回転
            transform.RotateAround(rotationCenter.transform.position, Vector3.up, -1.5f);
        }
        else if (serialReceive.Flag_view == 2)
        {
            // プレイヤーを右に回転
            transform.RotateAround(rotationCenter.transform.position, Vector3.up, 1.5f);
        }

        if (serialReceive.Flag_button == 1)
        {
            playerCollider.enabled = true;
            transform.position += transform.forward * movementSpeed * 1000;
            // y軸の高さの固定
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
            movementParticles.Play();
            PlaySoundEffect(1);
        }
        else
        {
            movementParticles.Stop();
        }
    }

    /// <summary>
    /// マウス入力に基づいてプレイヤーの操作を処理します。
    /// </summary>
    private void HandleMouseInput()
    {
        float rotationSpeed = 10f; // 回転速度

        if (Input.GetKey(KeyCode.Space))
        {
            playerCollider.enabled = true;
            transform.position += transform.forward * movementSpeed * 1000;
            // y軸の高さの固定
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
            movementParticles.Play();
            PlaySoundEffect(1);
        }
        else
        {
            movementParticles.Stop();
        }

        // 十字キーでの回転
        float horizontalInput = Input.GetAxis("Horizontal"); // 左右キーの取得
        transform.Rotate(0, rotationSpeed * horizontalInput * 0.1f, 0);

        // マウスのX方向の移動距離を計算
        Vector3 currentMousePosition = Input.mousePosition;
        float mouseDeltaX = currentMousePosition.x - lastMousePosition.x;

        rotationCenter.transform.Rotate(0, mouseDeltaX * rotationSpeed * 0.005f, 0);

        // 現在のマウス位置を次のフレーム用に保存
        lastMousePosition = currentMousePosition;
    }

    /// <summary>
    /// サウンドエフェクトを再生します。再生間隔に基づいて、連続再生を防ぎます。
    /// </summary>
    /// <param name="soundIndex">再生するサウンドのインデックス</param>
    private void PlaySoundEffect(int soundIndex)
    {
        if (Time.time > soundEffectInterval)
        {
            SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            soundManager.PlaySound(soundIndex);
            soundEffectInterval = Time.time + 2f; // 次の再生までの時間を設定
        }
    }
}
