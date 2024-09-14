using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敵のHPを管理し、プレイヤーとの衝突に応じてHPを減少させるスクリプト。
/// </summary>
public class EnemyHP : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // 敵の最大HP
    [SerializeField] private int currentHealth; // 敵の現在のHP
    [SerializeField] private Slider healthSlider; // HPを表示するスライダー

    /// <summary>
    /// ゲーム開始時にHPスライダーを初期化します。
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    /// <summary>
    /// 毎フレーム実行され、HPスライダーの向きをカメラ方向に固定します。
    /// </summary>
    void Update()
    {
        // スライダーの向きをカメラ方向に固定
        healthSlider.transform.rotation = Camera.main.transform.rotation;
    }

    /// <summary>
    /// プレイヤーとの衝突時にHPを減少させ、HPがゼロになると敵を削除します。
    /// </summary>
    /// <param name="other">衝突したオブジェクトのコライダー</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HandleDamage();
        }
    }

    /// <summary>
    /// プレイヤーとの衝突終了時にHPを減少させ、HPがゼロになると敵を削除します。
    /// </summary>
    /// <param name="other">衝突が終了したオブジェクトのコライダー</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HandleDamage();
        }
    }

    /// <summary>
    /// 敵にダメージを与え、HPスライダーを更新します。
    /// HPがゼロになると敵を削除します。
    /// </summary>
    private void HandleDamage()
    {
        SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager.SetVolume(0.5f);
        soundManager.PlaySound(2);

        // HPを減少させる
        currentHealth -= 50;
        healthSlider.value = currentHealth;

        // HPがゼロまたはそれ以下になった場合、敵を削除
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
