using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーキャラクターの動作を管理するスクリプト。
/// プレイヤーが敵と衝突した際に処理を行い、タイマーが0になるとコライダーを無効にします。
/// </summary>
public class Hatake : MonoBehaviour
{
    public static bool isCollide = false; // 衝突状態を示すフラグ

    /// <summary>
    /// Hatakeが他のオブジェクトと衝突したときに呼ばれる。
    /// 衝突したオブジェクトが「CloneEnemy」タグを持っている場合、衝突フラグを立て、サウンドを再生し、対象オブジェクトを削除します。
    /// </summary>
    /// <param name="other">衝突したオブジェクトのコライダー</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CloneEnemy"))
        {
            isCollide = true;
            PlaySound(0, 0.5f);
            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// 毎フレーム実行され、タイマーが0以下になるとプレイヤーのコライダーを無効にします。
    /// </summary>
    void Update()
    {
        Timer timerScript = GameObject.Find("UIManager").GetComponent<Timer>();
        if (timerScript.countTime <= 0)
        {
            // "DynamicObjects"という名前のオブジェクトを探す
            GameObject dynamicObjects = GameObject.Find("DynamicObjects");

            if (dynamicObjects != null)
            {
                // 子オブジェクトをすべて削除
                foreach (Transform child in dynamicObjects.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    /// <summary>
    /// 指定されたサウンドインデックスのサウンドを再生します。
    /// </summary>
    /// <param name="soundIndex">再生するサウンドのインデックス</param>
    /// <param name="volume">サウンドのボリューム</param>
    private void PlaySound(int soundIndex, float volume)
    {
        SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager.SetVolume(volume);
        soundManager.PlaySound(soundIndex);
    }
}
